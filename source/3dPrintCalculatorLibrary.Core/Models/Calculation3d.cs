using AndreasReitberger.Print3d.Core.Events;
using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
#if !SQL
using System.Xml.Serialization;
using AndreasReitberger.Print3d.Core.Interfaces;
#endif

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using System.Collections.ObjectModel;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.Core.Interfaces;
using IPrinterChangedEventArgs = AndreasReitberger.Print3d.SQLite.Interfaces.IPrinterChangedEventArgs;
using IMaterialChangedEventArgs = AndreasReitberger.Print3d.SQLite.Interfaces.IMaterialChangedEventArgs;
using ICalculation3d = AndreasReitberger.Print3d.SQLite.Interfaces.ICalculation3d;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Calculation3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    [Obsolete("Use the Calculation3dEnhanced class instead. This class is deprecated and will be removed later.")]
    public partial class Calculation3d : ObservableObject, ICalculation3d, ICloneable
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
      
        [ObservableProperty]
        [ForeignKey(typeof(Printer3d))]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        [Ignore]
        public partial Printer3d? Printer { get; set; }
        partial void OnPrinterChanged(Printer3d? value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnPrinterChangedEvent(new Events.PrinterChangedEventArgs()
                {
                    CalculationId = Id,
                    Printer = value,
                });
            }
        }
   
        [ObservableProperty]
        [ForeignKey(typeof(Material3d))]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        [Ignore]
        public partial Material3d? Material { get; set; }
        partial void OnMaterialChanged(Material3d? value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnMaterialChangedEvent(new Events.MaterialChangedEventArgs()
                {
                    CalculationId = Id,
                    Material = value,
                });
            }
        }
#else
        [ObservableProperty]
        public partial ICustomer3d? Customer { get; set; }

        [ObservableProperty]
        public partial IPrinter3d? Printer { get; set; }
        partial void OnPrinterChanged(IPrinter3d? value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnPrinterChangedEvent(new PrinterChangedEventArgs()
                {
                    CalculationId = Id,
                    Printer = value,
                });
            }
        }

        [ObservableProperty]
        public partial IMaterial3d? Material { get; set; }
        partial void OnMaterialChanged(IMaterial3d? value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnMaterialChangedEvent(new MaterialChangedEventArgs()
                {
                    CalculationId = Id,
                    Material = value,
                });
            }
        }
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
                OnRecalculationNeeded(new CalculatorEventArgs()
                {
                    CalculationId = Id,
                });
            }
        }

        [ObservableProperty]
        public partial double PowerLevel { get; set; } = 0;

        [ObservableProperty]
        public partial int Quantity { get; set; } = 0;

        [ObservableProperty]
        public partial double FailRate { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCostsPerkWh { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyEnergyCost { get; set; } = false;

        [ObservableProperty]
        public partial double TotalCosts { get; set; } = 0;

        [ObservableProperty]
        public partial bool CombineMaterialCosts { get; set; } = true;

        [ObservableProperty]
        public partial bool DifferFileCosts { get; set; } = true;

        #endregion

        #region Details

#if SQL

        [ObservableProperty]
        [ManyToMany(typeof(Printer3dCalculation3d), CascadeOperations = CascadeOperation.All)]
        public partial List<Printer3d> Printers { get; set; } = [];
        
        [ObservableProperty]
        [ManyToMany(typeof(Material3dCalculation3d), CascadeOperations = CascadeOperation.All)]
        public partial List<Material3d> Materials { get; set; } = [];

        [ObservableProperty]
        [ManyToMany(typeof(CustomAdditionCalculation3d), CascadeOperations = CascadeOperation.All)]
        public partial List<CustomAddition> CustomAdditions { get; set; } = [];

        [ObservableProperty]
        [ManyToMany(typeof(WorkstepUsageCalculation3d), CascadeOperations = CascadeOperation.All)]
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
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<File3d> Files { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IPrinter3d> Printers { get; set; } = [];

        [ObservableProperty]
        public partial IList<IMaterial3d> Materials { get; set; } = [];

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
        public partial IList<IFile3d> Files { get; set; } = [];

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

        public event EventHandler<ICalculatorEventArgs>? RecalculationNeeded;
        protected virtual void OnRecalculationNeeded(ICalculatorEventArgs e)
        {
            RecalculationNeeded?.Invoke(this, e);
        }

        public event EventHandler<IPrinterChangedEventArgs>? PrinterChanged;
        protected virtual void OnPrinterChangedEvent(IPrinterChangedEventArgs e)
        {
            PrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<IMaterialChangedEventArgs>? MaterialChanged;
        protected virtual void OnMaterialChangedEvent(IMaterialChangedEventArgs e)
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
        public object Clone() => MemberwiseClone();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Calculation3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
