using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Calculation3dProfile : ObservableObject, ICalculation3dProfile
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        #region Linked Customer
        [ObservableProperty]
        public partial List<Customer3d> Customers { get; set; } = [];
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
        public partial ObservableCollection<IProcedureAddition> ProcedureAdditions { get; set; } = [];
        #endregion

        #endregion

        #region Presets

        [ObservableProperty]
        public partial ObservableCollection<IPrinter3d> Printers { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<IMaterial3d> Materials { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<IItem3dUsage> Items { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<IWorkstepUsage> Worksteps { get; set; } = [];
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
