using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Events;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ICalculation3dEnhanced
    {
        #region Properties

        Guid Id { get; set; }

        #region Basics
        string Name { get; set; }
        CalculationState State { get; set; }
        DateTimeOffset Created { get; set; }
        ICustomer3d Customer { get; set; }
        bool IsCalculated { get; }
        bool RecalculationRequired { get; }
        int Quantity { get; set; }
        double PowerLevel { get; set; }
        double FailRate { get; set; }
        double EnergyCostsPerkWh { get; set; }
        bool ApplyEnergyCost { get; set; }
        double TotalCosts { get; set; }
        bool DifferFileCosts { get; set; }

        #endregion

        #region Details
        public IList<IPrinter3d> AvailablePrinters { get; }
        public IList<IMaterial3d> AvailableMaterials { get; }
        public IList<ICustomAddition> CustomAdditions { get; set; }
        public IList<IWorkstepUsage> WorkstepUsages { get; set; }
        public IList<ICalculationAttribute> PrintTimes { get; set; }
        public IList<ICalculationAttribute> MaterialUsages { get; set; }
        public IList<ICalculationAttribute> OverallMaterialCosts { get; set; }
        public IList<ICalculationAttribute> OverallPrinterCosts { get; set; }
        public IList<ICalculationAttribute> Costs { get; set; }
        public IList<ICalculationAttribute> Rates { get; set; }
        public IList<IPrint3dInfo> PrintInfos { get; set; }
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
        public IList<ICalculationProcedureAttribute> ProcedureAttributes { get; set; }
        public IList<IProcedureAddition> ProcedureAdditions { get; set; }
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

        public event EventHandler Error;
        public event EventHandler<CalculatorEventArgs> RecalculationNeeded;
        public event EventHandler<PrinterChangedEventArgs> PrinterChanged;
        public event EventHandler<MaterialChangedEventArgs> MaterialChanged;

        #endregion

        #region Methods
        public object Clone();
        public void CalculateCosts();
        public double GetTotalCosts(CalculationAttributeType calculationAttributeType = CalculationAttributeType.All);
        public double GetTotalCosts(Guid fileId, CalculationAttributeType calculationAttributeType = CalculationAttributeType.All);
        public int GetTotalQuantity();
        public double GetTotalPrintTime();
        public double GetTotalVolume();
        public double GetTotalMaterialUsed();
        #endregion
    }
}
