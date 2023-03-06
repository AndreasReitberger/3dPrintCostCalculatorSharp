using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Calculations")]
    public partial class Calculation3d : ObservableObject, ICalculation3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        #region Basics
        /*
         * Otherwise cannot be deserialized with JsonConverter
         */
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        DateTimeOffset created = DateTime.Now;

        [ObservableProperty]
        public Guid printerId;

        [ObservableProperty]
        Printer3d printer;
        partial void OnPrinterChanged(Printer3d value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnPrinterChangedEvent(new()
                {
                    CalculationId = Id,
                    Printer = value,
                });
            }
        }

        [ObservableProperty]
        public Guid materialId;

        [ObservableProperty]
        Material3d material;
        partial void OnMaterialChanged(Material3d value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnMaterialChangedEvent(new()
                {
                    CalculationId = Id,
                    Material = value,
                });
            }
        }

        [ObservableProperty]
        public Guid customerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CustomerId))]
        Customer3d customer;

        [ObservableProperty]
        [property: Ignore, JsonIgnore, XmlIgnore]
        bool isCalculated = false;

        [ObservableProperty]
        [property: Ignore, JsonIgnore, XmlIgnore]
        bool recalculationRequired = false;
        partial void OnRecalculationRequiredChanged(bool value)
        {
            if (value)
            {
                OnRecalculationNeeded(new()
                {
                    CalculationId = Id,
                });
            }
        }

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        double powerLevel = 0;

        [ObservableProperty]
        double failRate = 0;

        [ObservableProperty]
        double energyCostsPerkWh = 0;

        [ObservableProperty]
        bool applyEnergyCost = false;

        [ObservableProperty]
        double totalCosts = 0;

        [ObservableProperty]
        bool combineMaterialCosts = false;

        [ObservableProperty]
        bool differFileCosts = true;

        #endregion

        #region Details
        [ObservableProperty]
        [property: ManyToMany(typeof(Printer3dCalculation))]
        public List<Printer3d> printers = new();

        [ObservableProperty]
        [property: ManyToMany(typeof(Material3dCalculation))]
        public List<Material3d> materials = new();

        [ObservableProperty]
        [property: ManyToMany(typeof(CustomAdditionCalculation))]
        public List<CustomAddition> customAdditions = new();

        [ObservableProperty]
        [property: ManyToMany(typeof(WorkstepCalculation))]
        public List<Workstep> workSteps = new();

        [ObservableProperty]
        [property: OneToMany]
        public List<WorkstepDuration> workStepDurations = new();

        [ObservableProperty]
        [property: OneToMany]
        public List<Item3dUsage> additionalItems = new();

        [ObservableProperty]
        [property: Ignore]
        public ObservableCollection<CalculationAttribute> printTimes = new();

        [ObservableProperty]
        [property: Ignore]
        public ObservableCollection<CalculationAttribute> materialUsage = new();

        [ObservableProperty]
        [property: Ignore]
        public ObservableCollection<CalculationAttribute> overallMaterialCosts = new();

        [ObservableProperty]
        [property: Ignore]
        public ObservableCollection<CalculationAttribute> overallPrinterCosts = new();

        [ObservableProperty]
        [property: Ignore]
        public ObservableCollection<CalculationAttribute> costs = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CalculationAttribute> rates = new();

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<File3d> files = new();
        #endregion

        #region AdditionalSettings
        [ObservableProperty]
        bool applyEnhancedMarginSettings = false;

        [ObservableProperty]
        bool excludePrinterCostsFromMarginCalculation = false;

        [ObservableProperty]
        bool excludeMaterialCostsFromMarginCalculation = false;

        [ObservableProperty]
        bool excludeWorkstepsFromMarginCalculation = false;

        #endregion

        #region ProcedureSpecific
        [ObservableProperty]
        bool applyProcedureSpecificAdditions = false;

        [ObservableProperty]
        Material3dFamily _procedure = Material3dFamily.Misc;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<CalculationProcedureAttribute> procedureAttributes = new();
        #endregion

        #region Calculated
        [Ignore, JsonIgnore]
        public int TotalQuantity => GetTotalQuantity();

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

        #region EventHandlers
        public event EventHandler Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        public event EventHandler<CalculatorEventArgs> RecalculationNeeded;
        protected virtual void OnRecalculationNeeded(CalculatorEventArgs e)
        {
            RecalculationNeeded?.Invoke(this, e);
        }

        public event EventHandler<PrinterChangedEventArgs> PrinterChanged;
        protected virtual void OnPrinterChangedEvent(PrinterChangedEventArgs e)
        {
            PrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialChangedEventArgs> MaterialChanged;
        protected virtual void OnMaterialChangedEvent(MaterialChangedEventArgs e)
        {
            MaterialChanged?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        public Calculation3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        [Obsolete("This method is obsolete, please use `CalculateCosts()` instead.")]
        public void Calculate()
        {
            PrintTimes.Clear();
            MaterialUsage.Clear();
            OverallMaterialCosts.Clear();
            OverallPrinterCosts.Clear();
            Costs.Clear();
            CombineMaterialCosts = false;

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
                PrintTimes.Add(new CalculationAttribute()
                {
                    Attribute = file.FileName,
                    Value = printTime,
                    FileId = file.Id,
                    FileName = file.FileName,
                });
                if (FailRate > 0)
                {
                    PrintTimes.Add(new CalculationAttribute()
                    {
                        Attribute = $"{file.FileName}_FailRate",
                        Value = printTime * FailRate / 100,
                        FileId = file.Id,
                        FileName = file.FileName,
                    });
                }

                if (Materials.Count > 0)
                {
                    Material ??= Materials[0];
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
                    MaterialUsage.Add(new CalculationAttribute()
                    {
                        Attribute = Material.Name,
                        Value = _material,
                        Type = CalculationAttributeType.Material,
                        FileId = file.Id,
                        FileName = file.FileName,
                    });
                    if (FailRate > 0)
                    {
                        MaterialUsage.Add(new CalculationAttribute()
                        {
                            Attribute = $"{Material.Name}_FailRate",
                            Value = _material * FailRate / 100,
                            Type = CalculationAttributeType.Material,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
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
                    Material ??= material;

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

                    double pricePerGramm = Convert.ToDouble(material.UnitPrice) /
                        Convert.ToDouble(Convert.ToDouble(material.PackageSize) * Convert.ToDouble(UnitFactor.GetUnitFactor(material.Unit)));

                    if (DifferFileCosts)
                    {
                        foreach (CalculationAttribute materialUsage in MaterialUsage)
                        {
                            double totalCosts = Convert.ToDouble(
                            materialUsage.Value * pricePerGramm);
                            OverallMaterialCosts.Add(new CalculationAttribute()
                            {
                                LinkedId = material.Id,
                                Attribute = material.Name,
                                Type = CalculationAttributeType.Material,
                                Value = totalCosts,
                                FileId = materialUsage.FileId,
                                FileName = materialUsage.FileName,
                            });
                        }
                    }
                    else
                    {
                        double totalMaterialUsed = GetTotalMaterialUsed();
                        //((Volume * Material.Density * Material.UnitPrice) / (Material.PackageSize * Convert.ToDecimal(UnitFactor.getUnitFactor(Material.Unit)))) * Quantity * (1m + FailRate / 100m);

                        double totalCosts = Convert.ToDouble(
                            totalMaterialUsed * pricePerGramm);
                        OverallMaterialCosts.Add(new CalculationAttribute()
                        {
                            LinkedId = material.Id,
                            Attribute = material.Name,
                            Type = CalculationAttributeType.Material,
                            Value = totalCosts,
                        });
                    }

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
                    Printer ??= printer;
                    if (DifferFileCosts)
                    {
                        foreach (CalculationAttribute printTime in PrintTimes)
                        {
                            if (printer.HourlyMachineRate != null)
                            {
                                double machineHourRate = Convert.ToDouble(printer.HourlyMachineRate.CalcMachineHourRate) * printTime.Value;
                                if (machineHourRate > 0)
                                {
                                    OverallPrinterCosts.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Machine,
                                        Value = machineHourRate,
                                        FileId = printTime.FileId,
                                        FileName = printTime.FileName,
                                    });
                                }
                            }
                            if (ApplyEnergyCost)
                            {
                                double consumption = Convert.ToDouble(((printTime.Value * Convert.ToDouble(printer.PowerConsumption)) / 1000.0)) / 100.0 * Convert.ToDouble(PowerLevel);
                                double totalEnergyCost = consumption * EnergyCostsPerkWh;
                                if (totalEnergyCost > 0)
                                {
                                    OverallPrinterCosts.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Energy,
                                        Value = totalEnergyCost,
                                        FileId = printTime.FileId,
                                        FileName = printTime.FileName,
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
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
                        if (ApplyEnergyCost)
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
                            WorkstepDuration workstepDuration = WorkStepDurations?.FirstOrDefault(wsd => wsd.WorkstepId == ws.Id);
                            if (workstepDuration != null)
                            {
                                ws.Duration = workstepDuration.Duration;
                            }
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

            if (ApplyProcedureSpecificAdditions)
            {
                List<CalculationProcedureAttribute> multiMaterialAttributes = ProcedureAttributes
                    .Where(attr => attr.Family == Procedure && attr.Level == CalculationLevel.Calculation)?.ToList();
                for (int i = 0; i < multiMaterialAttributes?.Count; i++)
                {
                    CalculationProcedureAttribute attribute = multiMaterialAttributes[i];
                    for (int j = 0; j < attribute.Parameters.Count; j++)
                    {
                        CalculationProcedureParameter parameter = attribute.Parameters[j];
                        switch (parameter.Type)
                        {
                            case ProcedureParameter.MultiMaterialCalculation:
                                if (Printer?.MaterialType == Material3dFamily.Filament)
                                {
                                    CombineMaterialCosts = parameter.Value > 0;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            /*
            if (ApplyProcedureSpecificAdditions && ProcedureAttributes.Count > 0)
            {
                IEnumerable<CalculationProcedureAttribute> attributes = ProcedureAttributes.Where(attr => attr.Family == Procedure);
                foreach (CalculationProcedureAttribute attribute in attributes)
                {

                }
            }
            */
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
            // Custom additions after margin
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
            RecalculationRequired = false;
        }
        #endregion

        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
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
