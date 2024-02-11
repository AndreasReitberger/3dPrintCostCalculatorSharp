using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("Calculations")]
    [Obsolete("Use Calculation3dEnhanced instead")]
    public partial class Calculation3d : ObservableObject, ICalculation3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        #region Basics
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        CalculationState state = CalculationState.Draft;

        [ObservableProperty]
        DateTimeOffset created = DateTime.Now;

        [ObservableProperty]
        Guid printerId;

        [ObservableProperty]
        [property: Ignore]
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
        [property: Ignore]
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
        Guid customerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CustomerId), CascadeOperations = CascadeOperation.All)]
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
        [property: ManyToMany(typeof(Printer3dCalculation), CascadeOperations = CascadeOperation.All)]
        List<Printer3d> printers = [];

        [ObservableProperty]
        [property: ManyToMany(typeof(Material3dCalculation), CascadeOperations = CascadeOperation.All)]
        List<Material3d> materials = [];

        [ObservableProperty]
        [property: ManyToMany(typeof(CustomAdditionCalculation), CascadeOperations = CascadeOperation.All)]
        List<CustomAddition> customAdditions = [];

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsages class instead")]
        [property: ManyToMany(typeof(WorkstepCalculation), CascadeOperations = CascadeOperation.All)]
        List<Workstep> workSteps = [];
        [ObservableProperty]
        [Obsolete("Use the WorkstepUsages class instead")]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<WorkstepDuration> workStepDurations = [];

        [ObservableProperty]
        [property: ManyToMany(typeof(WorkstepUsageCalculation), CascadeOperations = CascadeOperation.All)]
        List<WorkstepUsage> workstepUsages = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Item3dUsage> additionalItems = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> printTimes = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> materialUsage = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> overallMaterialCosts = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> overallPrinterCosts = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> costs = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<CalculationAttribute> rates = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<File3d> files = [];

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
        Material3dFamily procedure = Material3dFamily.Misc;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<CalculationProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<ProcedureAddition> procedureAdditions = [];

        #endregion

        #region Calculated
        [Ignore, XmlIgnore, JsonIgnore]
        public int TotalQuantity => GetTotalQuantity();

        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
        public double MachineCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Machine) + GetTotalCosts(CalculationAttributeType.Energy);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
        public double ItemsCost
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.AdditionalItem);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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
        [Ignore, XmlIgnore, JsonIgnore]
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

        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
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
