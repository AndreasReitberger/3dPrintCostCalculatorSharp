using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Calculation3dProfile)}s")]
#else
using AndreasReitberger.Print3d.Core.Interfaces;

namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Calculation3dProfile : ObservableObject, ICalculation3dProfile
    {
        #region Properties

        #region Basic
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;
        #endregion

        #region Linked Customer
        [ObservableProperty]
#if SQL
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Customer3d> Customers { get; set; } = [];
#else
        public partial IList<ICustomer3d> Customers { get; set; } = [];
#endif
        #endregion

        #region Presets

        #region Rates

        [ObservableProperty]
        public partial double FailRate { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyTaxRate { get; set; } = false;

        [ObservableProperty]
        public partial double TaxRate { get; set; } = 0;

        [ObservableProperty]
        public partial double MarginRate { get; set; } = 0;

        #endregion

        #region Handling

        [ObservableProperty]
        public partial double HandlingsFee { get; set; } = 0;

        #endregion

        #region Energy

        [ObservableProperty]
        public partial bool ApplyEnergyCost { get; set; } = false;

        [ObservableProperty]
        public partial double PowerLevel { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCostsPerkWh { get; set; } = 0;

        #endregion

        #region ProcedureSpecific

        #region Filament

        [ObservableProperty]
        public partial bool ApplyNozzleWearCosts { get; set; } = false;

        [ObservableProperty]
        public partial double NozzleReplacementCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double NozzleWearFactorPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial double NozzleWearCostsPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyPrintSheetWearCosts { get; set; } = false;

        [ObservableProperty]
        public partial double PrintSheetReplacementCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double PrintSheetWearFactorPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial double PrintSheetWearCostsPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyMultiMaterialCosts { get; set; } = false;

        [ObservableProperty]
        public partial double MultiMaterialChangeCosts { get; set; } = 0;

        [ObservableProperty]
        public partial bool MultiMaterialAllSelectetMaterialsAreUsed { get; set; } = false;

        [ObservableProperty]
        public partial double MultiMaterialChangesPerPrint { get; set; } = 0;

        #endregion

        #region Resin

        [ObservableProperty]
        public partial bool ApplyResinGlovesCosts { get; set; } = false;

        [ObservableProperty]
        public partial double GlovesPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial double GlovesInPackage { get; set; } = 0;

        [ObservableProperty]
        public partial double GlovesPackagePrice { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyResinFilterCosts { get; set; } = false;

        [ObservableProperty]
        public partial double FiltersPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial double FiltersInPackage { get; set; } = 0;

        [ObservableProperty]
        public partial double FiltersPackagePrice { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyResinWashingCosts { get; set; } = false;

        [ObservableProperty]
        public partial double IsopropanolContainerContent { get; set; } = 0;

        [ObservableProperty]
        public partial double IsopropanolContainerPrice { get; set; } = 0;

        [ObservableProperty]
        public partial double IsopropanolPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyResinCuringCosts { get; set; } = false;

        [ObservableProperty]
        public partial double CuringCostsPerHour { get; set; } = 0;

        [ObservableProperty]
        public partial double CuringDurationInMintues { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyResinTankWearCosts { get; set; } = false;

        [ObservableProperty]
        public partial double ResinTankReplacementCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double ResinTankWearFactorPerPrintJob { get; set; } = 0;

        [ObservableProperty]
        public partial double ResinTankWearCostsPerPrintJob { get; set; } = 0;
        #endregion

        #region Powder

        [ObservableProperty]
        public partial bool ApplySLSRefreshing { get; set; } = false;

        [ObservableProperty]
        public partial double PowderInBuildArea { get; set; } = 0;

        #endregion

        #region Custom

        [ObservableProperty]
#if SQL
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<ProcedureAddition> ProcedureAdditions { get; set; } = [];
#else
        public partial IList<IProcedureAddition> ProcedureAdditions { get; set; } = [];
#endif
        #endregion

        #endregion

        #region Presets

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Printer3d> Printers { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Material3d> Materials { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Item3dUsage> Items { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<WorkstepUsage> Worksteps { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IPrinter3d> Printers { get; set; } = [];

        [ObservableProperty]
        public partial IList<IMaterial3d> Materials { get; set; } = [];

        [ObservableProperty]
        public partial IList<IItem3dUsage> Items { get; set; } = [];

        [ObservableProperty]
        public partial IList<IWorkstepUsage> Worksteps { get; set; } = [];
#endif
        #endregion

        #endregion

        #endregion

        #region Constructor
        public Calculation3dProfile()
        {
            Id = Guid.NewGuid();
        }
        public Calculation3dProfile(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
