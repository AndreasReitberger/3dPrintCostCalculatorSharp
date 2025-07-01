using AndreasReitberger.Print3d.Core.Events;
using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Serialization;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Calculation3dEnhanced)}s")]
#else
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Calculation3dEnhanced : ObservableObject, ICalculation3dEnhanced, ICloneable
    {

        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        #region Basics

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        CalculationState state = CalculationState.Draft;

        [ObservableProperty]
        DateTimeOffset created = DateTime.Now;

#if SQL
        [ObservableProperty]
        //[property: ForeignKey(typeof(Customer3d))]
        Guid customerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CustomerId), CascadeOperations = CascadeOperation.All)]
        Customer3d? customer;
#else
        [ObservableProperty]
        ICustomer3d? customer;
#endif

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
#if SQL
        [property: Ignore]
#endif
        bool isCalculated = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
#if SQL
        [property: Ignore]
#endif
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
        bool differFileCosts = true;

        #endregion

        #region Details

#if SQL
        public List<Printer3d?> AvailablePrinters => PrintInfos.Select(pi => pi?.Printer).Distinct().ToList();

        public List<Material3d?> AvailableMaterials => PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu?.Material).Distinct().ToList();

        [ObservableProperty]
        [property: ManyToMany(typeof(CustomAdditionCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        List<CustomAddition> customAdditions = [];

        [ObservableProperty]
        [property: ManyToMany(typeof(WorkstepUsageCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        List<WorkstepUsage> workstepUsages = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Item3dUsage> additionalItems = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> printTimes = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> materialUsages = [];

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
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Print3dInfo> printInfos = [];
        partial void OnPrintInfosChanged(List<Print3dInfo> value)
        {

        }
#else
        public IList<IPrinter3d?> AvailablePrinters => PrintInfos.Select(pi => pi.Printer).Distinct().ToList();

        public IList<IMaterial3d?> AvailableMaterials => PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu.Material).Distinct().ToList();

        [ObservableProperty]
        IList<ICustomAddition> customAdditions = [];

        [ObservableProperty]
        IList<IWorkstepUsage> workstepUsages = [];

        [ObservableProperty]
        IList<IItem3dUsage> additionalItems = [];

        [ObservableProperty]
        IList<ICalculationAttribute> printTimes = [];

        [ObservableProperty]
        IList<ICalculationAttribute> materialUsages = [];

        [ObservableProperty]
        IList<ICalculationAttribute> overallMaterialCosts = [];

        [ObservableProperty]
        IList<ICalculationAttribute> overallPrinterCosts = [];

        [ObservableProperty]
        IList<ICalculationAttribute> costs = [];

        [ObservableProperty]
        IList<ICalculationAttribute> rates = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        IList<IPrint3dInfo> printInfos = [];
        partial void OnPrintInfosChanged(IList<IPrint3dInfo> value)
        {

        }
#endif
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


#if SQL
        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<CalculationProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<ProcedureAddition> procedureAdditions = [];
#else
        [ObservableProperty]
        IList<ICalculationProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        IList<IProcedureAddition> procedureAdditions = [];
#endif

        #endregion

        #region Calculated
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
        public int TotalQuantity => GetTotalQuantity();

        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        [XmlIgnore, JsonIgnore]
#if SQL
        [property: Ignore]
#endif
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
        public event EventHandler? Error;
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

        public event EventHandler<CalculatorEventArgs>? RecalculationNeeded;
        protected virtual void OnRecalculationNeeded(CalculatorEventArgs e)
        {
            RecalculationNeeded?.Invoke(this, e);
        }

        public event EventHandler<PrinterChangedEventArgs>? PrinterChanged;
        protected virtual void OnPrinterChangedEvent(PrinterChangedEventArgs e)
        {
            PrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialChangedEventArgs>? MaterialChanged;
        protected virtual void OnMaterialChangedEvent(MaterialChangedEventArgs e)
        {
            MaterialChanged?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        public Calculation3dEnhanced()
        {
            Id = Guid.NewGuid();
            PrintInfos = [];
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();
        
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Calculation3dEnhanced item)
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
