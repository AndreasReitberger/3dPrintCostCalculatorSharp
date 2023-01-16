using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("CalculationProfiles")]
    public partial class Calculation3dProfile : ObservableObject, ICalculation3dProfile
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: JsonIgnore]
        string name = string.Empty;

        #region Linked Customer
        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Customer3d> customers = new();
        #endregion

        #region Presets

        #region Rates

        [ObservableProperty]
        [property: JsonIgnore]
        double failRate = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyTaxRate = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double taxRate = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double marginRate = 0;

        #endregion

        #region Handling

        [ObservableProperty]
        [property: JsonIgnore]
        double handlingsFee = 0;

        #endregion

        #region Energy

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyEnergyCost = false;

        [ObservableProperty]
        [property: JsonIgnore]
        int powerLevel = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double energyCostsPerkWh = 0;

        #endregion

        #region ProcedureSpecific

        #region Filament

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyNozzleWearCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double nozzleReplacementCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double nozzleWearFactorPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double nozzleWearCostsPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyPrintSheetWearCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double printSheetReplacementCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double printSheetWearFactorPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double printSheetWearCostsPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyMultiMaterialCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double multiMaterialChangeCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool multiMaterialAllSelectetMaterialsAreUsed = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double multiMaterialChangesPerPrint = 0;

        #endregion

        #region Resin

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyResinGlovesCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        int glovesPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        int glovesInPackage = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double glovesPackagePrice = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyResinFilterCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double filtersPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        int filtersInPackage = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double filtersPackagePrice = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyResinWashingCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double isopropanolContainerContent = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double isopropanolContainerPrice = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double isopropanolPerPrintJob = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        bool applyResinCuringCosts = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double curingCostsPerHour = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double curingDurationInMintues = 0;
        #endregion

        #region Powder

        [ObservableProperty]
        [property: JsonIgnore]
        bool applySLSRefreshing = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double powderInBuildArea = 0;
 
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
