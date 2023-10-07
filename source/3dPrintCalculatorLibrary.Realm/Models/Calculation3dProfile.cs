using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Calculation3dProfile : RealmObject, ICalculation3dProfile
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        #region Linked Customer

        public IList<Customer3d> Customers { get; }
        #endregion

        #region Presets

        #region Rates

        public double FailRate { get; set; } = 0;

        public bool ApplyTaxRate { get; set; } = false;

        public double TaxRate { get; set; } = 0;

        public double MarginRate { get; set; } = 0;

        #endregion

        #region Handling

        public double HandlingsFee { get; set; } = 0;

        #endregion

        #region Energy


        public bool ApplyEnergyCost { get; set; } = false;

        public int PowerLevel { get; set; } = 0;

        public double EnergyCostsPerkWh { get; set; } = 0;

        #endregion

        #region ProcedureSpecific

        #region Filament

        public bool ApplyNozzleWearCosts { get; set; } = false;

        public double NozzleReplacementCosts { get; set; } = 0;

        public double NozzleWearFactorPerPrintJob { get; set; } = 0;

        public double NozzleWearCostsPerPrintJob { get; set; } = 0;

        public bool ApplyPrintSheetWearCosts { get; set; } = false;

        public double PrintSheetReplacementCosts { get; set; } = 0;

        public double PrintSheetWearFactorPerPrintJob { get; set; } = 0;

        public double PrintSheetWearCostsPerPrintJob { get; set; } = 0;

        public bool ApplyMultiMaterialCosts { get; set; } = false;

        public double MultiMaterialChangeCosts { get; set; } = 0;

        public bool MultiMaterialAllSelectetMaterialsAreUsed { get; set; } = false;

        public double MultiMaterialChangesPerPrint { get; set; } = 0;

        #endregion

        #region Resin

        public bool ApplyResinGlovesCosts { get; set; } = false;

        public int GlovesPerPrintJob { get; set; } = 0;

        public int GlovesInPackage { get; set; } = 0;

        public double GlovesPackagePrice { get; set; } = 0;

        public bool ApplyResinFilterCosts { get; set; } = false;

        public double FiltersPerPrintJob { get; set; } = 0;

        public int FiltersInPackage { get; set; } = 0;

        public double FiltersPackagePrice { get; set; } = 0;

        public bool ApplyResinWashingCosts { get; set; } = false;

        public double IsopropanolContainerContent { get; set; } = 0;

        public double IsopropanolContainerPrice { get; set; } = 0;

        public double IsopropanolPerPrintJob { get; set; } = 0;

        public bool ApplyResinCuringCosts { get; set; } = false;

        public double CuringCostsPerHour { get; set; } = 0;

        public double CuringDurationInMintues { get; set; } = 0;
        #endregion

        #region Powder

        public bool ApplySLSRefreshing { get; set; } = false;

        public double PowderInBuildArea { get; set; } = 0;

        #endregion

        #region Custom
        public IList<ProcedureAddition> ProcedureAdditions { get; }
        #endregion

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
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
