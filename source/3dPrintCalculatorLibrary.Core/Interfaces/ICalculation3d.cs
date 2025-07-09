using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Events;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
{
#endif
    public interface ICalculation3d
    {
        #region Properties

        #region Basics
        Guid Id { get; set; }
        string Name { get; set; }
        CalculationState State { get; set; }
        DateTimeOffset Created { get; set; }
#if SQL
        public Guid CustomerId { get; set; }
        Customer3d? Customer { get; set; }

        public Guid PrinterId { get; set; }
        Printer3d? Printer { get; set; }

        public Guid MaterialId { get; set; }
        Material3d? Material { get; set; }
#else
        ICustomer3d? Customer { get; set; }
        IPrinter3d? Printer { get; set; }
        IMaterial3d? Material { get; set; }
#endif

        bool IsCalculated { get; }
        bool RecalculationRequired { get; }
        int Quantity { get; set; }
        double PowerLevel { get; set; }
        double FailRate { get; set; }
        double EnergyCostsPerkWh { get; set; }
        bool ApplyEnergyCost { get; set; }
        double TotalCosts { get; set; }
        bool CombineMaterialCosts { get; }
        bool DifferFileCosts { get; set; }

        #endregion

        #region Details
#if SQL
        public List<Printer3d?> Printers { get; set; }
        public List<Material3d?> Materials { get; set; }
        public List<CustomAddition> CustomAdditions { get; set; }
        public List<WorkstepUsage> WorkstepUsages { get; set; }
        public List<Item3dUsage> AdditionalItems { get; set; }

        public ObservableCollection<CalculationAttribute> PrintTimes { get; set; }
        public ObservableCollection<CalculationAttribute> MaterialUsages { get; set; }
        public ObservableCollection<CalculationAttribute> OverallMaterialCosts { get; set; }
        public ObservableCollection<CalculationAttribute> OverallPrinterCosts { get; set; }
        public ObservableCollection<CalculationAttribute> Costs { get; set; }

        public List<CalculationAttribute> Rates { get; set; }
        public List<File3d> Files { get; set; }
#else
        public IList<IPrinter3d?> Printers { get; set; }
        public IList<IMaterial3d?> Materials { get; set; }
        public IList<ICustomAddition> CustomAdditions { get; set; }
        public IList<IWorkstepUsage> WorkstepUsages { get; set; }
        public IList<IItem3dUsage> AdditionalItems { get; set; }
        public IList<ICalculationAttribute> PrintTimes { get; set; }
        public IList<ICalculationAttribute> MaterialUsages { get; set; }
        public IList<ICalculationAttribute> OverallMaterialCosts { get; set; }
        public IList<ICalculationAttribute> OverallPrinterCosts { get; set; }
        public IList<ICalculationAttribute> Costs { get; set; }
        public IList<ICalculationAttribute> Rates { get; set; }
        public IList<IFile3d> Files { get; set; }
#endif
        #endregion

        #region AdditionalSettings
        public bool ApplyEnhancedMarginSettings { get; set; }
        public bool ExcludePrinterCostsFromMarginCalculation { get; set; }
        public bool ExcludeMaterialCostsFromMarginCalculation { get; set; }
        public bool ExcludeWorkstepsFromMarginCalculation { get; set; }
        #endregion

        #region ProcedureSpecific
        public bool ApplyProcedureSpecificAdditions { get; set; }
        public Material3dFamily Procedure { get; set; }
#if SQL
        public ObservableCollection<CalculationProcedureAttribute> ProcedureAttributes { get; set; }
        public ObservableCollection<ProcedureAddition> ProcedureAdditions { get; set; }
#else
        public IList<ICalculationProcedureAttribute> ProcedureAttributes { get; set; }
        public IList<IProcedureAddition> ProcedureAdditions { get; set; }
#endif
        #endregion

        #region Calculated

        public int TotalQuantity { get; }
        public double TotalPrintTime { get; }
        public double TotalVolume { get; }
        public double TotalMaterialUsed { get; }
        public double MachineCosts { get; }
        public double MaterialCosts { get; }
        public double ItemsCost { get; }
        public double EnergyCosts { get; }
        public double HandlingCosts { get; }
        public double CustomAdditionCosts { get; }
        public double WorkstepCosts { get; }
        public double CalculatedMargin { get; }
        public double CalculatedTax { get; }
        public double CostsPerPiece { get; }

        #endregion

        #endregion

        #region Event Handlers

        public event EventHandler? Error;
        public event EventHandler<ICalculatorEventArgs>? RecalculationNeeded;
        public event EventHandler<IPrinterChangedEventArgs>? PrinterChanged;
        public event EventHandler<IMaterialChangedEventArgs>? MaterialChanged;

        #endregion

        #region Methods
        public object Clone();
        public void CalculateCosts();
        public Task CalculateCostsAsync();
        public double GetTotalCosts(CalculationAttributeType calculationAttributeType = CalculationAttributeType.All);
        public double GetTotalCosts(Guid fileId, CalculationAttributeType calculationAttributeType = CalculationAttributeType.All);
        public int GetTotalQuantity();
        public double GetTotalPrintTime();
        public double GetTotalVolume();
        public double GetTotalMaterialUsed();
        #endregion
    }
}
