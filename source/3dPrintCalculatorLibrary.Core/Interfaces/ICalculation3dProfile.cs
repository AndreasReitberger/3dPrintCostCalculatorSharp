#if SQL
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface ICalculation3dProfile
    {
        #region Properties

        #region Basic
        public Guid Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Linked Customer
#if SQL
        public List<Customer3d> Customers { get; set; }
#else
        public IList<ICustomer3d> Customers { get; set; }
#endif
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
        public double PowerLevel { get; set; }
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
        public double GlovesPerPrintJob { get; set; }
        public double GlovesInPackage { get; set; }
        public double GlovesPackagePrice { get; set; }
        public bool ApplyResinFilterCosts { get; set; }
        public double FiltersPerPrintJob { get; set; }
        public double FiltersInPackage { get; set; }
        public double FiltersPackagePrice { get; set; }
        public bool ApplyResinWashingCosts { get; set; }
        public double IsopropanolContainerContent { get; set; }
        public double IsopropanolContainerPrice { get; set; }
        public double IsopropanolPerPrintJob { get; set; }
        public bool ApplyResinCuringCosts { get; set; }
        public double CuringCostsPerHour { get; set; }
        public double CuringDurationInMintues { get; set; }
        public bool ApplyResinTankWearCosts { get; set; }
        public double ResinTankReplacementCosts { get; set; }
        public double ResinTankWearFactorPerPrintJob { get; set; }
        public double ResinTankWearCostsPerPrintJob { get; set; }
        #endregion

        #region Powder
        public bool ApplySLSRefreshing { get; set; }
        public double PowderInBuildArea { get; set; }

        #endregion

        #region Custom
#if SQL
        public List<ProcedureAddition> ProcedureAdditions { get; set; }
#else
        public IList<IProcedureAddition> ProcedureAdditions { get; set; }
#endif

        #endregion

        #region Presets
#if SQL
        public List<Printer3d> Printers { get; set; }
        public List<Material3d> Materials { get; set; }
        public List<Item3dUsage> Items { get; set; }
        public List<WorkstepUsage> Worksteps { get; set; }
#else
        public IList<IPrinter3d> Printers { get; set; }
        public IList<IMaterial3d> Materials { get; set; }
        public IList<IItem3dUsage> Items { get; set; }
        public IList<IWorkstepUsage> Worksteps { get; set; }
#endif
        #endregion

        #endregion

        #endregion

        #endregion
    }
}
