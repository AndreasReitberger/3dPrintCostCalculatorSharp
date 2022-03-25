using AndreasReitberger.Enums;
using AndreasReitberger.Models.CalculationAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AndreasReitberger.Utilities;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Models
{
    [Table("Calculations")]
    public class Calculation3d : BaseModel
    {

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        #region Basics
        /*
         * Otherwise cannot be deserialized with JsonConverter
         */
        [JsonProperty(nameof(Name))]
        string _name = string.Empty;

        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(nameof(Created))]
        DateTime _created = DateTime.Now;

        [JsonIgnore]
        public DateTime Created
        {
            get { return _created; }
            set { SetProperty(ref _created, value); }
        }

        [JsonIgnore, XmlIgnore]
        public Guid PrinterId { get; set; }

        [JsonProperty(nameof(Printer))]
        Printer3d _printer;
        
        [Ignore, JsonIgnore]
        //[ManyToOne(nameof(PrinterId))]
        public Printer3d Printer
        {
            get { return _printer; }
            set
            {
                SetProperty(ref _printer, value);
                // Recalculate on change
                if (IsCalculated)
                {
                    NeedsReCalculation = true;
                    IsCalculated = false;
                    //Calculate();
                }
                //OnPropertyChanged(nameof(TotalCosts));
            }
        }
        
        [JsonProperty(nameof(Material))]
        [JsonIgnore, XmlIgnore]
        public Guid MaterialId { get; set; }

        Material3d material;
        [Ignore, JsonIgnore]
        //[ManyToOne(nameof(MaterialId))]
        public Material3d Material
        {
            get { return material; }
            set
            {
                SetProperty(ref material, value);
                // Recalculate on change
                if (IsCalculated)
                {
                    NeedsReCalculation = true;
                    IsCalculated = false;
                    //Calculate();
                }
                //OnPropertyChanged(nameof(TotalCosts));
            }
        }

        [JsonIgnore, XmlIgnore]
        public Guid CustomerId { get; set; }

        [JsonProperty(nameof(Customer))]
        Customer3d _customer;
        [JsonIgnore]
        [ManyToOne(nameof(CustomerId))]
        public Customer3d Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }

        [JsonProperty(nameof(IsCalculated))]
        bool _isCalculated = false;
        [Ignore, JsonIgnore, XmlIgnore]
        public bool IsCalculated
        {
            get { return _isCalculated; }
            private set { SetProperty(ref _isCalculated, value); }
        }

        [JsonProperty(nameof(NeedsReCalculation))]
        bool _needsReCalculation = false;
        [Ignore, JsonIgnore, XmlIgnore]
        public bool NeedsReCalculation
        {
            get { return _needsReCalculation; }
            private set { SetProperty(ref _needsReCalculation, value); }
        }

        [JsonProperty(nameof(Quantity))]
        int _quantity = 1;
        [JsonIgnore]
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        [JsonProperty(nameof(PowerLevel))]
        double _powerLevel = 0;
        [JsonIgnore]
        public double PowerLevel
        {
            get { return _powerLevel; }
            set { SetProperty(ref _powerLevel, value); }
        }

        [JsonProperty(nameof(FailRate))]
        double _failRate = 0;
        [JsonIgnore]
        public double FailRate
        {
            get { return _failRate; }
            set { SetProperty(ref _failRate, value); }
        }

        [JsonProperty(nameof(EnergyCostsPerkWh))]
        double _energyCostsPerkWh = 0;
        [JsonIgnore]
        public double EnergyCostsPerkWh
        {
            get { return _energyCostsPerkWh; }
            set { SetProperty(ref _energyCostsPerkWh, value); }
        }

        [JsonProperty(nameof(ApplyenergyCost))]
        bool _applyEnergyCost = false;
        [JsonIgnore]
        public bool ApplyenergyCost
        {
            get { return _applyEnergyCost; }
            set { SetProperty(ref _applyEnergyCost, value); }
        }

        [JsonProperty(nameof(TotalCosts))]
        double _totalCosts = 0;
        [JsonIgnore]
        public double TotalCosts
        {
            get { return _totalCosts; }
            set { SetProperty(ref _totalCosts, value); }
        }
        /*
        [JsonProperty(nameof(TargetProcedure))]
        Printer3dType _targetProcedure;
        [JsonIgnore]
        public Printer3dType TargetProcedure
        {
            get { return _targetProcedure; }
            set { SetProperty(ref _targetProcedure, value); }
        }
        */
        #endregion

        #region Details
        // Do not store in database, this collections will be filled after
        // calling "Calculate()"
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //[Ignore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Printer3d> Printers
        { get; set; } = new ObservableCollection<Printer3d>();

        //[Ignore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Material3d> Materials
        { get; set; } = new ObservableCollection<Material3d>();

        //[Ignore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CustomAddition> CustomAdditions
        { get; set; } = new ObservableCollection<CustomAddition>();

        //[Ignore]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Workstep> WorkSteps
        { get; set; } = new ObservableCollection<Workstep>();

        [Ignore]
        public ObservableCollection<CalculationAttribute> PrintTimes
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        [Ignore]
        public ObservableCollection<CalculationAttribute> MaterialUsage
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        [Ignore]
        public ObservableCollection<CalculationAttribute> OverallMaterialCosts
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        [Ignore]
        public ObservableCollection<CalculationAttribute> OverallPrinterCosts
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        [Ignore]
        public ObservableCollection<CalculationAttribute> Costs
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        /*
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CalculationAttribute> FixCosts
        { get; set; } = new ObservableCollection<CalculationAttribute>();
        */

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CalculationAttribute> Rates
        { get; set; } = new ObservableCollection<CalculationAttribute>();

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<File3d> Files
        { get; set; } = new ObservableCollection<File3d>();
        #endregion

        #region AdditionalSettings
        [JsonProperty(nameof(ApplyEnhancedMarginSettings))]
        bool _applyEnhancedMarginSettings = false;
        [JsonIgnore]
        public bool ApplyEnhancedMarginSettings
        {
            get { return _applyEnhancedMarginSettings; }
            set { SetProperty(ref _applyEnhancedMarginSettings, value); }
        }

        [JsonProperty(nameof(ExcludePrinterCostsFromMarginCalculation))]
        bool _excludePrinterCostsFromMarginCalculation = false;
        [JsonIgnore]
        public bool ExcludePrinterCostsFromMarginCalculation
        {
            get { return _excludePrinterCostsFromMarginCalculation; }
            set { SetProperty(ref _excludePrinterCostsFromMarginCalculation, value); }
        }

        [JsonProperty(nameof(ExcludeMaterialCostsFromMarginCalculation))]
        bool _excludeMaterialCostsFromMarginCalculation = false;
        [JsonIgnore]
        public bool ExcludeMaterialCostsFromMarginCalculation
        {
            get { return _excludeMaterialCostsFromMarginCalculation; }
            set { SetProperty(ref _excludeMaterialCostsFromMarginCalculation, value); }
        }

        [JsonProperty(nameof(ExcludeWorkstepsFromMarginCalculation))]
        bool _excludeWorkstepsFromMarginCalculation = false;
        [JsonIgnore]
        public bool ExcludeWorkstepsFromMarginCalculation
        {
            get { return _excludeWorkstepsFromMarginCalculation; }
            set { SetProperty(ref _excludeWorkstepsFromMarginCalculation, value); }
        }
        #endregion

        #region ProcedureSpecific
        [JsonProperty(nameof(ApplyProcedureSpecificAdditions))]
        bool _applyProcedureSpecificAdditions = false;
        [JsonIgnore]
        public bool ApplyProcedureSpecificAdditions
        {
            get { return _applyProcedureSpecificAdditions; }
            set { SetProperty(ref _applyProcedureSpecificAdditions, value); }
        }

        [JsonProperty(nameof(Procedure))]
        Material3dFamily _procedure = Material3dFamily.Misc;
        [JsonIgnore]
        public Material3dFamily Procedure
        {
            get { return _procedure; }
            set { SetProperty(ref _procedure, value); }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CalculationProcedureAttribute> ProcedureAttributes
        { get; set; } = new ObservableCollection<CalculationProcedureAttribute>();
        #endregion

        #region Calculated
        [Ignore, JsonIgnore]
        public int TotalQuantity
        {
            get
            {
                return GetTotalQuantity();
            }
        }
        [Ignore, JsonIgnore]
        public double TotalPrintTime
        {
            get
            {
                if (IsCalculated)
                    return GetTotalPrintTime();
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double TotalVolume
        {
            get
            {
                if (IsCalculated)
                    return GetTotalVolume();
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double TotalMaterialUsed
        {
            get
            {
                if (IsCalculated)
                    return GetTotalMaterialUsed();
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double MachineCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Machine);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double MaterialCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Material);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double EnergyCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Energy);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double HandlingCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.FixCost);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double CustomAdditionCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.CustomAddition);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double WorkstepCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Workstep);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double CalculatedMargin
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Margin);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double CalculatedTax
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Tax);
                else
                    return 0;
            }
        }
        [Ignore, JsonIgnore]
        public double CostsPerPiece
        {
            get
            {
                if (IsCalculated)
                    return TotalCosts / TotalQuantity;
                else
                    return 0;
            }
        }

        #endregion

        #endregion

        #region Constructor
        public Calculation3d()
        {

        }
        #endregion

        #region Methods
        public void Calculate()
        {
            PrintTimes.Clear();
            MaterialUsage.Clear();
            OverallMaterialCosts.Clear();
            OverallPrinterCosts.Clear();
            Costs.Clear();

            int quantity = Files.Select(file => file.Quantity).ToList().Sum();
            // Handling fee
            CalculationAttribute HandlingsFee = Rates.FirstOrDefault(costs => costs.Attribute == "HandlingFee");
            if (HandlingsFee != null && HandlingsFee.Value > 0)
            {
                Costs.Add(new CalculationAttribute() { Attribute = "HandlingFee", Type = CalculationAttributeType.FixCost, Value = Convert.ToDouble(HandlingsFee.Value * quantity) });
            }
            foreach (File3d file in Files)
            {
                double printTime = file.PrintTime * (file.MultiplyPrintTimeWithQuantity ? (file.Quantity * file.PrintTimeQuantityFactor) : 1);
                PrintTimes.Add(new CalculationAttribute() { Attribute = file.FileName, Value = printTime });
                PrintTimes.Add(new CalculationAttribute() { Attribute = string.Format("FailRate_{0}", file.FileName), Value = printTime * FailRate / 100 });
                if (Materials.Count > 0)
                {
                    if (Material == null)
                        Material = Materials[0];
                    //((CONSUMED_MATERIAL[g] x PRICE[$/kg]) / 1000) x QUANTITY x (FAILRATE / 100)
                    double _weight = 0;
                    if (file.Volume > 0)
                    {
                        double _volume = file.Volume;
                        _weight = _volume * Convert.ToDouble(Material.Density);
                    }
                    else if (file.Weight != null)
                    {
                        _weight = file.Weight.Weight * Convert.ToDouble(UnitFactor.GetUnitFactor(file.Weight.Unit));
                    }
                    // Needed material in g
                    double _material = _weight * file.Quantity;
                    MaterialUsage.Add(new CalculationAttribute() { Attribute = Material.Name, Value = _material, Type = CalculationAttributeType.Material });
                    MaterialUsage.Add(new CalculationAttribute() { Attribute = string.Format("FailRate_{0}", Material.Name), Value = _material * FailRate / 100, Type = CalculationAttributeType.Material });
                }
            }
            // Material
            if (Materials.Count > 0)
            {
                // Check if selected material is still in the collection
                Material = Materials?.FirstOrDefault(material => material.Id == Material?.Id);
                foreach (Material3d material in Materials)
                {
                    // Set first materials as default
                    if (Material == null)
                    {
                        Material = material;
                    }

                    double refreshed = 0;
                    if (ApplyProcedureSpecificAdditions && material.MaterialFamily == Material3dFamily.Powder)
                    {
                        CalculationProcedureAttribute attribute = ProcedureAttributes.FirstOrDefault(
                            attr => attr.Attribute == ProcedureAttribute.MaterialRefreshingRatio && attr.Level == CalculationLevel.Material);
                        if (attribute != null)
                        {
                            CalculationProcedureParameter minPowderNeeded = attribute.Parameters.FirstOrDefault(para => para.Type == ProcedureParameter.MinPowderNeeded);
                            if (minPowderNeeded != null)
                            {
                                double powderInBuildArea = minPowderNeeded.Value;
                                MaterialAdditions.Material3dProcedureAttribute refreshRatio = material.ProcedureAttributes.FirstOrDefault(ratio => ratio.Attribute == ProcedureAttribute.MaterialRefreshingRatio);
                                if (refreshRatio != null)
                                {
                                    // this value is in liter
                                    CalculationAttribute materialPrintObject = MaterialUsage.FirstOrDefault(usage =>
                                        usage.Attribute == material.Name);
                                    if (materialPrintObject != null)
                                    {
                                        double refreshedMaterial = (powderInBuildArea -
                                            (materialPrintObject.Value * material.FactorLToKg / UnitFactor.GetUnitFactor(Unit.kg))) * refreshRatio.Value / 100f;
                                        refreshed = (refreshedMaterial / material.FactorLToKg * UnitFactor.GetUnitFactor(Unit.kg));
                                    }
                                    else
                                        refreshed = 0;
                                }
                            }
                        }
                    }

                    double totalMaterialUsed = GetTotalMaterialUsed();
                    //((Volume * Material.Density * Material.UnitPrice) / (Material.PackageSize * Convert.ToDecimal(UnitFactor.getUnitFactor(Material.Unit)))) * Quantity * (1m + FailRate / 100m);
                    double pricePerGramm = Convert.ToDouble(material.UnitPrice) /
                        Convert.ToDouble(Convert.ToDouble(material.PackageSize) * Convert.ToDouble(UnitFactor.GetUnitFactor(material.Unit)));
                    double totalCosts = Convert.ToDouble(
                        totalMaterialUsed * pricePerGramm);
                    OverallMaterialCosts.Add(new CalculationAttribute()
                    {
                        LinkedId = material.Id,
                        Attribute = material.Name,
                        Type = CalculationAttributeType.Material,
                        Value = totalCosts,
                    });
                    if (refreshed > 0)
                    {
                        double refreshCosts = Convert.ToDouble(
                        refreshed * pricePerGramm);
                        OverallMaterialCosts.Add(new CalculationAttribute()
                        {
                            LinkedId = material.Id,
                            Attribute = $"{material.Name} (Refreshed)",
                            Type = CalculationAttributeType.Material,
                            Value = refreshCosts,
                        });
                    }
                }
            }
            double totalPrintTime = GetTotalPrintTime();
            // Machine + Energy
            if (Printers.Count > 0)
            {
                // Check if selected material is still in the collection
                Printer = Printers?.FirstOrDefault(printer => printer.Id == Printer?.Id);
                foreach (Printer3d printer in Printers)
                {
                    if (Printer == null)
                    {
                        Printer = printer;
                    }

                    if (printer.HourlyMachineRate != null)
                    {
                        double machineHourRate = Convert.ToDouble(printer.HourlyMachineRate.CalcMachineHourRate) * totalPrintTime;
                        if (machineHourRate > 0)
                        {
                            OverallPrinterCosts.Add(new CalculationAttribute()
                            {
                                LinkedId = printer.Id,
                                Attribute = printer.Name,
                                Type = CalculationAttributeType.Machine,
                                Value = machineHourRate,
                            });
                        }
                    }

                    if (ApplyenergyCost)
                    {
                        double consumption = Convert.ToDouble(((totalPrintTime * Convert.ToDouble(printer.PowerConsumption)) / 1000.0)) / 100.0 * Convert.ToDouble(PowerLevel);
                        double totalEnergyCost = consumption * EnergyCostsPerkWh;
                        if (totalEnergyCost > 0)
                        {
                            OverallPrinterCosts.Add(new CalculationAttribute()
                            {
                                LinkedId = printer.Id,
                                Attribute = printer.Name,
                                Type = CalculationAttributeType.Energy,
                                Value = totalEnergyCost,
                            });
                        }
                    }

                    if (ApplyProcedureSpecificAdditions)
                    {
                        // Filter for the current printer procedure
                        List<CalculationProcedureAttribute> attributes = ProcedureAttributes.Where(
                            attr => attr.Family == printer.MaterialType && attr.Level == CalculationLevel.Printer).ToList();
                        foreach (CalculationProcedureAttribute attribute in attributes)
                        {
                            foreach (CalculationProcedureParameter parameter in attribute.Parameters)
                            {
                                OverallPrinterCosts.Add(new CalculationAttribute()
                                {
                                    LinkedId = printer.Id,
                                    Attribute = parameter.Type.ToString(),
                                    Type = CalculationAttributeType.ProcedureSpecificAddition,
                                    Value = parameter.Value,
                                });
                            }
                        }
                    }
                }
            }
            // Worksteps
            if (WorkSteps.Count > 0)
            {
                foreach (Workstep ws in WorkSteps)
                {
                    switch (ws.CalculationType)
                    {
                        case CalculationType.PerHour:
                            double totalPerHour = ws.TotalCosts;
                            //double totalPerHour = Convert.ToDouble(ws.Duration) * Convert.ToDouble(ws.Price);
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = ws.Id,
                                Attribute = ws.Name,
                                Type = CalculationAttributeType.Workstep,
                                Value = totalPerHour,
                            });
                            break;
                        case CalculationType.PerJob:
                            double totalPerJob = ws.TotalCosts;
                            //double totalPerJob = Convert.ToDouble(ws.Price);
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = ws.Id,
                                Attribute = ws.Name,
                                Type = CalculationAttributeType.Workstep,
                                Value = totalPerJob,
                            });
                            break;
                        case CalculationType.PerPiece:
                            double totalPerPiece = ws.TotalCosts * quantity;
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = ws.Id,
                                Attribute = ws.Name,
                                Type = CalculationAttributeType.Workstep,
                                Value = totalPerPiece,
                            });
                            break;
                    }
                }
            }
            if (ApplyProcedureSpecificAdditions && ProcedureAttributes.Count > 0)
            {
                IEnumerable<CalculationProcedureAttribute> attributes = ProcedureAttributes.Where(attr => attr.Family == Procedure);
                foreach (CalculationProcedureAttribute attribute in attributes)
                {

                }
            }
            // Custom additions before margin
            List<CustomAddition> customAdditionsBeforeMargin =
                CustomAdditions.Where(addition => addition.CalculationType == CustomAdditionCalculationType.BeforeApplingMargin).ToList();
            if (customAdditionsBeforeMargin.Count > 0)
            {
                SortedDictionary<int, double> additions = new();
                foreach (CustomAddition ca in customAdditionsBeforeMargin)
                {
                    if (additions.ContainsKey(ca.Order))
                        additions[ca.Order] += ca.Percentage;
                    else
                        additions.Add(ca.Order, ca.Percentage);
                }
                foreach (KeyValuePair<int, double> pairs in additions)
                {
                    double costsSoFar = GetTotalCosts();
                    Costs.Add(new CalculationAttribute()
                    {
                        Attribute = string.Format("CustomAdditionPreMargin_Order{0}", pairs.Key),
                        Type = CalculationAttributeType.CustomAddition,
                        Value = pairs.Value * costsSoFar / 100.0
                    });
                }
            }
            //Margin
            CalculationAttribute Margin = Rates.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Margin);
            if (Margin != null && !Margin.SkipForCalculation)
            {
                double costsSoFar = GetTotalCosts();
                if (ApplyEnhancedMarginSettings)
                {
                    double excludedCosts = 0;
                    if (ExcludePrinterCostsFromMarginCalculation)
                    {
                        excludedCosts += GetTotalCosts(CalculationAttributeType.Machine);
                    }
                    if (ExcludeMaterialCostsFromMarginCalculation)
                    {
                        excludedCosts += GetTotalCosts(CalculationAttributeType.Material);
                    }
                    if (ExcludeWorkstepsFromMarginCalculation)
                    {
                        excludedCosts += GetTotalCosts(CalculationAttributeType.Workstep);
                    }
                    // Subtract costs 
                    if (excludedCosts > 0)
                        costsSoFar -= excludedCosts;
                }
                double margin = costsSoFar * Margin.Value / (Margin.IsPercentageValue ? 100.0 : 1.0);
                Costs.Add(new CalculationAttribute() { Attribute = "Margin", Type = CalculationAttributeType.Margin, Value = margin });
            }
            // Custom additions before margin
            List<CustomAddition> customAdditionsAfterMargin =
                CustomAdditions.Where(addition => addition.CalculationType == CustomAdditionCalculationType.AfterApplingMargin).ToList();
            if (customAdditionsAfterMargin.Count > 0)
            {
                SortedDictionary<int, double> additions = new();
                foreach (CustomAddition ca in customAdditionsAfterMargin)
                {
                    if (additions.ContainsKey(ca.Order))
                        additions[ca.Order] += ca.Percentage;
                    else
                        additions.Add(ca.Order, ca.Percentage);
                }
                foreach (KeyValuePair<int, double> pairs in additions)
                {
                    double costsSoFar = GetTotalCosts();
                    Costs.Add(new CalculationAttribute()
                    {
                        Attribute = string.Format("CustomAdditionPostMargin_Order{0}", pairs.Key),
                        Type = CalculationAttributeType.CustomAddition,
                        Value = pairs.Value * costsSoFar / 100.0
                    });
                }
            }

            //Tax
            CalculationAttribute Tax = Rates.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Tax);
            if (Tax != null && !Tax.SkipForCalculation)
            {
                double costsSoFar = GetTotalCosts();
                double tax = costsSoFar * Tax.Value / (Tax.IsPercentageValue ? 100.0 : 1.0);
                Costs.Add(new CalculationAttribute() { Attribute = "Tax", Type = CalculationAttributeType.Tax, Value = tax });
            }

            TotalCosts = GetTotalCosts(CalculationAttributeType.All);
            IsCalculated = true;
            NeedsReCalculation = false;
        }
        public int GetTotalQuantity()
        {
            try
            {
                int quantity = Files.Select(file => file.Quantity).ToList().Sum();
                return quantity;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalPrintTime()
        {
            try
            {
                IEnumerable<double> times = PrintTimes.Select(value => Convert.ToDouble(value.Value));
                double total = 0;
                foreach (var time in times)
                    total += time;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalVolume()
        {
            try
            {
                IEnumerable<double> volumes = Files.Select(value => Convert.ToDouble(value.Volume));
                double total = 0;
                foreach (var vol in volumes)
                    total += vol;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalMaterialUsed()
        {
            try
            {
                IEnumerable<double> materials = MaterialUsage.Select(value => Convert.ToDouble(value.Value));
                double total = 0;
                foreach (var material in materials)
                    total += material;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalCosts(CalculationAttributeType calculationAttributeType = CalculationAttributeType.All)
        {
            try
            {
                IEnumerable<double> costs = calculationAttributeType == CalculationAttributeType.All ?
                    Costs.Select(value => Convert.ToDouble(value.Value)) :
                    Costs.Where(cost => cost.Type == calculationAttributeType).Select(value => Convert.ToDouble(value.Value))
                    ;
                // Get costs of currently selected printer
                /*
                IEnumerable<double> costsMachine = calculationAttributeType == CalculationAttributeType.All ?
                    OverallPrinterCosts.Where(cost =>
                        cost.Attribute == Printer.Name ||
                        cost.LinkedId == Printer.Id)
                    .Select(value => Convert.ToDouble(value.Value)) :

                    OverallPrinterCosts.Where(cost =>
                        (cost.Type == calculationAttributeType || cost.Type == CalculationAttributeType.ProcedureSpecificAddition) &&
                        (cost.Attribute == Printer.Name || cost.LinkedId == Printer.Id)
                        )
                    .Select(value => Convert.ToDouble(value.Value))
                    ;
                IEnumerable<double> costsMaterial = calculationAttributeType == CalculationAttributeType.All ?
                    OverallMaterialCosts.Where(cost =>
                        cost.Attribute == Material.Name ||
                        cost.LinkedId == Material.Id)
                    .Select(value => Convert.ToDouble(value.Value)) :

                    OverallMaterialCosts.Where(cost => cost.Type == calculationAttributeType &&
                        (cost.Attribute == Material.Name || cost.LinkedId == Material.Id)
                        ).Select(value => Convert.ToDouble(value.Value))
                    ;
                */
                IEnumerable<double> costsMachine = 
                    OverallPrinterCosts.Where(cost =>
                        cost.Attribute == Printer.Name ||
                        cost.LinkedId == Printer.Id)
                    .Select(value => Convert.ToDouble(value.Value))
                    ;
                IEnumerable<double> costsMaterial =
                    OverallMaterialCosts.Where(cost =>
                        cost.Attribute == Material.Name ||
                        cost.LinkedId == Material.Id)
                    .Select(value => Convert.ToDouble(value.Value));

                double total = 0;
                foreach (var cost in costs)
                    total += cost;
                if (calculationAttributeType == CalculationAttributeType.Machine || calculationAttributeType == CalculationAttributeType.All)
                {
                    foreach (var cost in costsMachine)
                        total += cost;
                }
                if (calculationAttributeType == CalculationAttributeType.Material || calculationAttributeType == CalculationAttributeType.All)
                {
                    foreach (var cost in costsMaterial)
                        total += cost;
                }
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            //return string.Format("{0} - {1:C2}", Name, TotalCosts);
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Calculation3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
