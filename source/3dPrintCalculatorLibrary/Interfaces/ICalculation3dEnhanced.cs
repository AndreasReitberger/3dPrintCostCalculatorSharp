using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface ICalculation3dEnhanced
    {
        #region Properties

        Guid Id { get; set; }

        #region Basics
        string Name { get; set; }
        DateTimeOffset Created { get; set; }
        public Guid CustomerId { get; set; }
        //ICustomer3d Customer { get; set; }
        bool IsCalculated { get; }
        bool RecalculationRequired { get; }
        int Quantity { get; set; }
        double PowerLevel { get; set; }
        double FailRate { get; set; }
        double EnergyCostsPerkWh { get; set; }
        bool ApplyEnergyCost { get; set; }
        double TotalCosts { get; set; }
        bool CombineMaterialCosts { get; set; }
        bool DifferFileCosts { get; set; }

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
        //public ObservableCollection<ICalculationProcedureAttribute> ProcedureAttributes { get; set; }
        #endregion

        #region Calculated

        public int TotalQuantity { get; }
        public double TotalPrintTime { get; }
        public double TotalVolume { get; }
        public double TotalMaterialUsed { get; }
        public double MachineCosts { get; }
        public double MaterialCosts { get; }
        public double EnergyCosts { get; }
        public double HandlingCosts { get; }
        public double CustomAdditionCosts { get; }
        public double WorkstepCosts { get; }
        public double CalculatedMargin { get; }
        public double CalculatedTax { get; }
        public double CostsPerPiece { get; }

        #endregion

        #endregion

        #region Lists
        //public ObservableCollection<IProcedureAddition> ProcedureAdditions { get; set; }
        /*
        public List<IPrinter3d> Printers { get; set; }
        public List<IMaterial3d> Materials { get; set; }
        public List<ICustomAddition> CustomAdditions { get; set; }
        public List<IWorkstep> WorkSteps { get; set; }
        public List<IWorkstepDuration> WorkStepDurations { get; set; } 
        public ObservableCollection<ICalculationAttribute> PrintTimes { get; set; }
        public ObservableCollection<ICalculationAttribute> MaterialUsage { get; set; }
        public ObservableCollection<ICalculationAttribute> OverallMaterialCosts { get; set; }
        public ObservableCollection<ICalculationAttribute> OverallPrinterCosts { get; set; }
        public ObservableCollection<ICalculationAttribute> Costs { get; set; }
        public List<ICalculationAttribute> Rates { get; set; }
        public List<IFile3d> Files { get; set; }
        */
        #endregion

        #region Methods
        void CalculateCosts();
        #endregion
    }
}
