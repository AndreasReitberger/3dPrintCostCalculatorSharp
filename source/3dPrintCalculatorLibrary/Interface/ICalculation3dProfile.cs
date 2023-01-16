using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface ICalculation3dProfile
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }

        #region Linked Customer
        //public List<ICustomer3d> Customers { get; set; }
        #endregion

        #region Presets

        #region Rates

        public double FailRate { get; set; }
        public bool ApplyTaxRate { get; set; }
        public double TaxRate { get; set; }
        public double MarginRate { get; set; }

        #endregion

        #region Handling
        public double HandlingsFee { get; set; }
        #endregion

        #region Energy
        public bool ApplyEnergyCost { get; set; }
        public int PowerLevel { get; set; }
        public double EnergyCostsPerkWh { get; set; }

        #endregion

        #region ProcedureSpecific

        #region Filament
        public bool ApplyNozzleWearCosts { get; set; }
        public double NozzleReplacementCosts { get; set; }
        public double NozzleWearFactorPerPrintJob { get; set; }
        public double NozzleWearCostsPerPrintJob { get; set; }
        public bool ApplyPrintSheetWearCosts { get; set; }
        public double PrintSheetReplacementCosts { get; set; }
        public double PrintSheetWearFactorPerPrintJob { get; set; }
        public double PrintSheetWearCostsPerPrintJob { get; set; }
        public bool ApplyMultiMaterialCosts { get; set; }
        public double MultiMaterialChangeCosts { get; set; }
        public bool MultiMaterialAllSelectetMaterialsAreUsed { get; set; }
        public double MultiMaterialChangesPerPrint { get; set; }
        #endregion

        #region Resin
        public bool ApplyResinGlovesCosts { get; set; }
        public int GlovesPerPrintJob { get; set; }
        public int GlovesInPackage { get; set; }
        public double GlovesPackagePrice { get; set; }
        public bool ApplyResinFilterCosts { get; set; }
        public double FiltersPerPrintJob { get; set; }
        public int FiltersInPackage { get; set; }
        public double FiltersPackagePrice { get; set; }
        public bool ApplyResinWashingCosts { get; set; }
        public double IsopropanolContainerContent { get; set; }
        public double IsopropanolContainerPrice { get; set; }
        public double IsopropanolPerPrintJob { get; set; }
        public bool ApplyResinCuringCosts { get; set; }
        public double CuringCostsPerHour { get; set; }
        public double CuringDurationInMintues { get; set; }
        #endregion

        #region Powder
        public bool ApplySLSRefreshing { get; set; }
        public double PowderInBuildArea { get; set; }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}
