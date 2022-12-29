using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models
{
    [Table("CalculationProfiles")]
    public class Calculation3dProfile : BaseModel
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [JsonProperty(nameof(Name))]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #region Linked Customer
        /*
        [JsonIgnore, XmlIgnore]
        public Guid CustomerId { get; set; }

        [JsonProperty(nameof(Customer))]
        Customer3d _customer;
        [JsonIgnore]
        [ManyToOne(nameof(CustomerId))]
        public Customer3d Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }
        */
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Customer3d> Customers { get; set; } = new();
        #endregion

        #region Presets

        #region Rates

        [JsonProperty(nameof(FailRate))]
        double _failRate = 0;
        [JsonIgnore]
        public double FailRate
        {
            get { return _failRate; }
            set { SetProperty(ref _failRate, value); }
        }

        [JsonProperty(nameof(ApplyTaxRate))]
        bool _applyTaxRate = false;
        [JsonIgnore]
        public bool ApplyTaxRate
        {
            get { return _applyTaxRate; }
            set { SetProperty(ref _applyTaxRate, value); }
        }

        [JsonProperty(nameof(TaxRate))]
        double _taxRate = 0;
        [JsonIgnore]
        public double TaxRate
        {
            get { return _taxRate; }
            set { SetProperty(ref _taxRate, value); }
        }

        [JsonProperty(nameof(MarginRate))]
        double _marginRate = 0;
        [JsonIgnore]
        public double MarginRate
        {
            get { return _marginRate; }
            set { SetProperty(ref _marginRate, value); }
        }

        #endregion

        #region Handling

        [JsonProperty(nameof(HandlingsFee))]
        double _handlingsFee = 0;
        [JsonIgnore]
        public double HandlingsFee
        {
            get { return _handlingsFee; }
            set { SetProperty(ref _handlingsFee, value); }
        }

        #endregion

        #region Energy

        [JsonProperty(nameof(ApplyenergyCost))]
        bool _applyEnergyCost = false;
        [JsonIgnore]
        public bool ApplyenergyCost
        {
            get { return _applyEnergyCost; }
            set { SetProperty(ref _applyEnergyCost, value); }
        }

        [JsonProperty(nameof(PowerLevel))]
        int _powerLevel = 0;
        [JsonIgnore]
        public int PowerLevel
        {
            get { return _powerLevel; }
            set { SetProperty(ref _powerLevel, value); }
        }

        [JsonProperty(nameof(EnergyCostsPerkWh))]
        double _energyCostsPerkWh = 0;
        [JsonIgnore]
        public double EnergyCostsPerkWh
        {
            get { return _energyCostsPerkWh; }
            set { SetProperty(ref _energyCostsPerkWh, value); }
        }

        #endregion

        #region ProcedureSpecific

        #region Filament

        [JsonProperty(nameof(ApplyNozzleWearCosts))]
        bool _applyNozzleWearCosts = false;
        [JsonIgnore]
        public bool ApplyNozzleWearCosts
        {
            get { return _applyNozzleWearCosts; }
            set { SetProperty(ref _applyNozzleWearCosts, value); }
        }

        [JsonProperty(nameof(NozzleReplacementCosts))]
        double _nozzleReplacementCosts = 0;
        [JsonIgnore]
        public double NozzleReplacementCosts
        {
            get { return _nozzleReplacementCosts; }
            set { SetProperty(ref _nozzleReplacementCosts, value); }
        }

        [JsonProperty(nameof(NozzleWearFactorPerPrintJob))]
        double _nozzleWearFactorPerPrintJob = 0;
        [JsonIgnore]
        public double NozzleWearFactorPerPrintJob
        {
            get { return _nozzleWearFactorPerPrintJob; }
            set { SetProperty(ref _nozzleWearFactorPerPrintJob, value); }
        }

        [JsonProperty(nameof(NozzleWearCostsPerPrintJob))]
        double _nozzleWearCostsPerPrintJob = 0;
        [JsonIgnore]
        public double NozzleWearCostsPerPrintJob
        {
            get { return _nozzleWearCostsPerPrintJob; }
            set { SetProperty(ref _nozzleWearCostsPerPrintJob, value); }
        }

        [JsonProperty(nameof(ApplyPrintSheetWearCosts))]
        bool _applyPrintSheetWearCosts = false;
        [JsonIgnore]
        public bool ApplyPrintSheetWearCosts
        {
            get { return _applyPrintSheetWearCosts; }
            set { SetProperty(ref _applyPrintSheetWearCosts, value); }
        }

        [JsonProperty(nameof(PrintSheetReplacementCosts))]
        double _printSheetReplacementCosts = 0;
        [JsonIgnore]
        public double PrintSheetReplacementCosts
        {
            get { return _printSheetReplacementCosts; }
            set { SetProperty(ref _printSheetReplacementCosts, value); }
        }

        [JsonProperty(nameof(PrintSheetWearFactorPerPrintJob))]
        double _printSheetWearFactorPerPrintJob = 0;
        [JsonIgnore]
        public double PrintSheetWearFactorPerPrintJob
        {
            get { return _printSheetWearFactorPerPrintJob; }
            set { SetProperty(ref _printSheetWearFactorPerPrintJob, value); }
        }

        [JsonProperty(nameof(PrintSheetWearCostsPerPrintJob))]
        double _printSheetWearCostsPerPrintJob = 0;
        [JsonIgnore]
        public double PrintSheetWearCostsPerPrintJob
        {
            get { return _printSheetWearCostsPerPrintJob; }
            set { SetProperty(ref _printSheetWearCostsPerPrintJob, value); }
        }

        [JsonProperty(nameof(ApplyMultiMaterialCosts))]
        bool _applyMultiMaterialCosts = false;
        [JsonIgnore]
        public bool ApplyMultiMaterialCosts
        {
            get { return _applyMultiMaterialCosts; }
            set { SetProperty(ref _applyMultiMaterialCosts, value); }
        }

        [JsonProperty(nameof(MultiMaterialChangeCosts))]
        double _multiMaterialChangeCosts = 0;
        [JsonIgnore]
        public double MultiMaterialChangeCosts
        {
            get { return _multiMaterialChangeCosts; }
            set { SetProperty(ref _multiMaterialChangeCosts, value); }
        }

        [JsonProperty(nameof(MultiMaterialAllSelectetMaterialsAreUsed))]
        bool _multiMaterialAllSelectetMaterialsAreUsed = false;
        [JsonIgnore]
        public bool MultiMaterialAllSelectetMaterialsAreUsed
        {
            get { return _multiMaterialAllSelectetMaterialsAreUsed; }
            set { SetProperty(ref _multiMaterialAllSelectetMaterialsAreUsed, value); }
        }

        [JsonProperty(nameof(MultiMaterialChangesPerPrint))]
        double _multiMaterialChangesPerPrint = 0;
        [JsonIgnore]
        public double MultiMaterialChangesPerPrint
        {
            get { return _multiMaterialChangesPerPrint; }
            set { SetProperty(ref _multiMaterialChangesPerPrint, value); }
        }
        #endregion

        #region Resin

        [JsonProperty(nameof(ApplyResinGlovesCosts))]
        bool _applyResinGlovesCosts = false;
        [JsonIgnore]
        public bool ApplyResinGlovesCosts
        {
            get { return _applyResinGlovesCosts; }
            set { SetProperty(ref _applyResinGlovesCosts, value); }
        }

        [JsonProperty(nameof(GlovesPerPrintJob))]
        int _glovesPerPrintJob = 0;
        [JsonIgnore]
        public int GlovesPerPrintJob
        {
            get { return _glovesPerPrintJob; }
            set { SetProperty(ref _glovesPerPrintJob, value); }
        }

        [JsonProperty(nameof(GlovesInPackage))]
        int _glovesInPackage = 0;
        [JsonIgnore]
        public int GlovesInPackage
        {
            get { return _glovesInPackage; }
            set { SetProperty(ref _glovesInPackage, value); }
        }

        [JsonProperty(nameof(GlovesPackagePrice))]
        double _glovesPackagePrice = 0;
        [JsonIgnore]
        public double GlovesPackagePrice
        {
            get { return _glovesPackagePrice; }
            set { SetProperty(ref _glovesPackagePrice, value); }
        }
        
        [JsonProperty(nameof(ApplyResinFilterCosts))]
        bool _applyResinFilterCosts = false;
        [JsonIgnore]
        public bool ApplyResinFilterCosts
        {
            get { return _applyResinFilterCosts; }
            set { SetProperty(ref _applyResinFilterCosts, value); }
        }

        [JsonProperty(nameof(FiltersPerPrintJob))]
        double _filtersPerPrintJob = 0;
        [JsonIgnore]
        public double FiltersPerPrintJob
        {
            get { return _filtersPerPrintJob; }
            set { SetProperty(ref _filtersPerPrintJob, value); }
        }

        [JsonProperty(nameof(FiltersInPackage))]
        int _filtersInPackage = 0;
        [JsonIgnore]
        public int FiltersInPackage
        {
            get { return _filtersInPackage; }
            set { SetProperty(ref _filtersInPackage, value); }
        }

        [JsonProperty(nameof(FiltersPackagePrice))]
        double _filtersPackagePrice = 0;
        [JsonIgnore]
        public double FiltersPackagePrice
        {
            get { return _filtersPackagePrice; }
            set { SetProperty(ref _filtersPackagePrice, value); }
        }
              
        [JsonProperty(nameof(ApplyResinWashingCosts))]
        bool _applyResinWashingCosts = false;
        [JsonIgnore]
        public bool ApplyResinWashingCosts
        {
            get { return _applyResinWashingCosts; }
            set { SetProperty(ref _applyResinWashingCosts, value); }
        }

        [JsonProperty(nameof(IsopropanolContainerContent))]
        double _isopropanolContainerContent = 0;
        [JsonIgnore]
        public double IsopropanolContainerContent
        {
            get { return _isopropanolContainerContent; }
            set { SetProperty(ref _isopropanolContainerContent, value); }
        }

        [JsonProperty(nameof(IsopropanolContainerPrice))]
        double _isopropanolContainerPrice = 0;
        [JsonIgnore]
        public double IsopropanolContainerPrice
        {
            get { return _isopropanolContainerPrice; }
            set { SetProperty(ref _isopropanolContainerPrice, value); }
        }

        [JsonProperty(nameof(IsopropanolPerPrintJob))]
        double _isopropanolPerPrintJob = 0;
        [JsonIgnore]
        public double IsopropanolPerPrintJob
        {
            get { return _isopropanolPerPrintJob; }
            set { SetProperty(ref _isopropanolPerPrintJob, value); }
        }
        
        [JsonProperty(nameof(ApplyResinCuringCosts))]
        bool _applyResinCuringCosts = false;
        [JsonIgnore]
        public bool ApplyResinCuringCosts
        {
            get { return _applyResinCuringCosts; }
            set { SetProperty(ref _applyResinCuringCosts, value); }
        }

        [JsonProperty(nameof(CuringCostsPerHour))]
        double _curingCostsPerHour = 0;
        [JsonIgnore]
        public double CuringCostsPerHour
        {
            get { return _curingCostsPerHour; }
            set { SetProperty(ref _curingCostsPerHour, value); }
        }   

        [JsonProperty(nameof(CuringDurationInMintues))]
        double _curingDurationInMintues = 0;
        [JsonIgnore]
        public double CuringDurationInMintues
        {
            get { return _curingDurationInMintues; }
            set { SetProperty(ref _curingDurationInMintues, value); }
        }
        #endregion

        #region Powder

        [JsonProperty(nameof(ApplySLSRefreshing))]
        bool _applySLSRefreshing = false;
        [JsonIgnore]
        public bool ApplySLSRefreshing
        {
            get { return _applySLSRefreshing; }
            set { SetProperty(ref _applySLSRefreshing, value); }
        }

        [JsonProperty(nameof(PowderInBuildArea))]
        double _powderInBuildArea = 0;
        [JsonIgnore]
        public double PowderInBuildArea
        {
            get { return _powderInBuildArea; }
            set { SetProperty(ref _powderInBuildArea, value); }
        }

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
