using AndreasReitberger.Print3d.Core.Events;
using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
#if !SQL
using System.Xml.Serialization;
#endif

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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        #region Basics

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial CalculationState State { get; set; } = CalculationState.Draft;

        [ObservableProperty]
        public partial DateTimeOffset Created { get; set; } = DateTime.Now;

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Customer3d))]
        public partial Guid CustomerId { get; set; }

        [ManyToOne(nameof(CustomerId), CascadeOperations = CascadeOperation.All)]
        [ObservableProperty]
        public partial Customer3d? Customer { get; set; }
#else
        [ObservableProperty]
        public partial ICustomer3d? Customer { get; set; }
#endif

#if SQL
        [Ignore]
#endif
        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial bool IsCalculated { get; set; } = false;

#if SQL
        [Ignore]
#endif
        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial bool RecalculationRequired { get; set; } = false;
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
        public partial double PowerLevel { get; set; } = 0;

        [ObservableProperty]
        public partial double FailRate { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCostsPerkWh { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyEnergyCost { get; set; } = false;

        [ObservableProperty]
        public partial double TotalCosts { get; set; } = 0;

        [ObservableProperty]
        public partial bool DifferFileCosts { get; set; } = true;

        #endregion

        #region Details

#if SQL
        public List<Printer3d?> AvailablePrinters => [.. PrintInfos.Select(pi => pi?.Printer).Distinct()];

        public List<Material3d?> AvailableMaterials => [.. PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu?.Material).Distinct()];

        [ObservableProperty]
        [ManyToMany(typeof(CustomAdditionCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        public partial List<CustomAddition> CustomAdditions { get; set; } = [];

        [ObservableProperty]
        [ManyToMany(typeof(WorkstepUsageCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        public partial List<WorkstepUsage> WorkstepUsages { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Item3dUsage> AdditionalItems { get; set; } = [];

        [ObservableProperty]
        [Ignore]
        public partial ObservableCollection<CalculationAttribute> PrintTimes { get; set; } = [];

        [ObservableProperty]
        [Ignore]
        public partial ObservableCollection<CalculationAttribute> MaterialUsages { get; set; } = [];

        [ObservableProperty]
        [Ignore]
        public partial ObservableCollection<CalculationAttribute> OverallMaterialCosts { get; set; } = [];

        [ObservableProperty]
        [Ignore]
        public partial ObservableCollection<CalculationAttribute> OverallPrinterCosts { get; set; } = [];

        [ObservableProperty]
        [Ignore]
        public partial ObservableCollection<CalculationAttribute> Costs { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<CalculationAttribute> Rates { get; set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Print3dInfo> PrintInfos { get; set; } = [];
        partial void OnPrintInfosChanged(List<Print3dInfo> value)
        {

        }
#else
        public IList<IPrinter3d?> AvailablePrinters => [.. PrintInfos.Select(pi => pi.Printer).Distinct()];

        public IList<IMaterial3d?> AvailableMaterials => [.. PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu.Material).Distinct()];

        [ObservableProperty]
        public partial IList<ICustomAddition> CustomAdditions { get; set; } = [];

        [ObservableProperty]
        public partial IList<IWorkstepUsage> WorkstepUsages { get; set; } = [];

        [ObservableProperty]
        public partial IList<IItem3dUsage> AdditionalItems { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> PrintTimes { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> MaterialUsages { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> OverallMaterialCosts { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> OverallPrinterCosts { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> Costs { get; set; } = [];

        [ObservableProperty]
        public partial IList<ICalculationAttribute> Rates { get; set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        public partial IList<IPrint3dInfo> PrintInfos { get; set; } = [];

        partial void OnPrintInfosChanged(IList<IPrint3dInfo> value)
        {

        }
#endif
#endregion

        #region AdditionalSettings
        [ObservableProperty]
        public partial bool ApplyEnhancedMarginSettings { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludePrinterCostsFromMarginCalculation { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludeMaterialCostsFromMarginCalculation { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludeWorkstepsFromMarginCalculation { get; set; } = false;

        #endregion

        #region ProcedureSpecific
        [ObservableProperty]
        public partial bool ApplyProcedureSpecificAdditions { get; set; } = false;

        [ObservableProperty]
        public partial Material3dFamily Procedure { get; set; } = Material3dFamily.Misc;

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial ObservableCollection<CalculationProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial ObservableCollection<ProcedureAddition> ProcedureAdditions { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<ICalculationProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        public partial IList<IProcedureAddition> ProcedureAdditions { get; set; } = [];
#endif

        #endregion

        #region Calculated
        [XmlIgnore, JsonIgnore]
#if SQL
        [Ignore]
#endif
        public int TotalQuantity => GetTotalQuantity();

        [XmlIgnore, JsonIgnore]
#if SQL
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        [Ignore]
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
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
