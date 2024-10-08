﻿using AndreasReitberger.Print3d.Interfaces;
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
        public Guid id;

        [ObservableProperty]
        string name = string.Empty;

        #region Linked Customer
        [ObservableProperty]
        public List<Customer3d> customers = [];
        #endregion

        #region Presets

        #region Rates

        [ObservableProperty]
        double failRate = 0;

        [ObservableProperty]
        bool applyTaxRate = false;

        [ObservableProperty]
        double taxRate = 0;

        [ObservableProperty]
        double marginRate = 0;

        #endregion

        #region Handling

        [ObservableProperty]
        double handlingsFee = 0;

        #endregion

        #region Energy

        [ObservableProperty]
        bool applyEnergyCost = false;

        [ObservableProperty]
        double powerLevel = 0;

        [ObservableProperty]
        double energyCostsPerkWh = 0;

        #endregion

        #region ProcedureSpecific

        #region Filament

        [ObservableProperty]
        bool applyNozzleWearCosts = false;

        [ObservableProperty]
        double nozzleReplacementCosts = 0;

        [ObservableProperty]
        double nozzleWearFactorPerPrintJob = 0;

        [ObservableProperty]
        double nozzleWearCostsPerPrintJob = 0;

        [ObservableProperty]
        bool applyPrintSheetWearCosts = false;

        [ObservableProperty]
        double printSheetReplacementCosts = 0;

        [ObservableProperty]
        double printSheetWearFactorPerPrintJob = 0;

        [ObservableProperty]
        double printSheetWearCostsPerPrintJob = 0;

        [ObservableProperty]
        bool applyMultiMaterialCosts = false;

        [ObservableProperty]
        double multiMaterialChangeCosts = 0;

        [ObservableProperty]
        bool multiMaterialAllSelectetMaterialsAreUsed = false;

        [ObservableProperty]
        double multiMaterialChangesPerPrint = 0;

        #endregion

        #region Resin

        [ObservableProperty]
        bool applyResinGlovesCosts = false;

        [ObservableProperty]
        double glovesPerPrintJob = 0;

        [ObservableProperty]
        double glovesInPackage = 0;

        [ObservableProperty]
        double glovesPackagePrice = 0;

        [ObservableProperty]
        bool applyResinFilterCosts = false;

        [ObservableProperty]
        double filtersPerPrintJob = 0;

        [ObservableProperty]
        double filtersInPackage = 0;

        [ObservableProperty]
        double filtersPackagePrice = 0;

        [ObservableProperty]
        bool applyResinWashingCosts = false;

        [ObservableProperty]
        double isopropanolContainerContent = 0;

        [ObservableProperty]
        double isopropanolContainerPrice = 0;

        [ObservableProperty]
        double isopropanolPerPrintJob = 0;

        [ObservableProperty]
        bool applyResinCuringCosts = false;

        [ObservableProperty]
        double curingCostsPerHour = 0;

        [ObservableProperty]
        double curingDurationInMintues = 0;

        [ObservableProperty]
        bool applyResinTankWearCosts = false;

        [ObservableProperty]
        double resinTankReplacementCosts = 0;

        [ObservableProperty]
        double resinTankWearFactorPerPrintJob = 0;

        [ObservableProperty]
        double resinTankWearCostsPerPrintJob = 0;
        #endregion

        #region Powder

        [ObservableProperty]
        bool applySLSRefreshing = false;

        [ObservableProperty]
        double powderInBuildArea = 0;

        #endregion

        #region Custom

        [ObservableProperty]
        ObservableCollection<IProcedureAddition> procedureAdditions = [];
        #endregion

        #endregion

        #region Presets

        [ObservableProperty]
        ObservableCollection<IPrinter3d> printers = [];

        [ObservableProperty]
        ObservableCollection<IMaterial3d> materials = [];

        [ObservableProperty]
        ObservableCollection<IItem3dUsage> items = [];

        [ObservableProperty]
        ObservableCollection<IWorkstepUsage> worksteps = [];
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
