using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("CalculationsEnhanced")]
    public partial class Calculation3dEnhanced : ObservableObject, ICalculation3dEnhanced, ICloneable
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        #region Basics

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        CalculationState state = CalculationState.Draft;

        [ObservableProperty]
        DateTimeOffset created = DateTime.Now;

        [ObservableProperty]
        Guid customerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(CustomerId), CascadeOperations = CascadeOperation.All)]
        Customer3d customer;

        [ObservableProperty]
        [property: Ignore, JsonIgnore, XmlIgnore]
        bool isCalculated = false;

        [ObservableProperty]
        [property: Ignore, JsonIgnore, XmlIgnore]
        bool recalculationRequired = false;
        partial void OnRecalculationRequiredChanged(bool value)
        {
            if (value)
            {
                OnRecalculationNeeded(new()
                {
                    CalculationId = Id,
                });
            }
        }

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        double powerLevel = 0;

        [ObservableProperty]
        double failRate = 0;

        [ObservableProperty]
        double energyCostsPerkWh = 0;

        [ObservableProperty]
        bool applyEnergyCost = false;

        [ObservableProperty]
        double totalCosts = 0;

        [ObservableProperty]
        [property: Obsolete("Not needed anymore, will be removed")]
        bool combineMaterialCosts = false;

        [ObservableProperty]
        bool differFileCosts = true;

        #endregion

        #region Details
        public List<Printer3d>? AvailablePrinters => PrintInfos.Select(pi => pi?.Printer)?.Distinct()?.ToList();

        public List<Material3d>? AvailableMaterials => PrintInfos.SelectMany(pi => pi?.MaterialUsages).Select(mu => mu.Material)?.Distinct()?.ToList();

        [ObservableProperty]
        [property: ManyToMany(typeof(CustomAdditionCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        List<CustomAddition> customAdditions = [];

        [ObservableProperty]
        [property: ManyToMany(typeof(WorkstepUsageCalculation3dEnhanced), CascadeOperations = CascadeOperation.All)]
        List<WorkstepUsage> workstepUsages = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Item3dUsage> additionalItems = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> printTimes = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> materialUsage = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> overallMaterialCosts = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> overallPrinterCosts = [];

        [ObservableProperty]
        [property: Ignore]
        ObservableCollection<CalculationAttribute> costs = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<CalculationAttribute> rates = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Print3dInfo> printInfos = [];
        partial void OnPrintInfosChanged(List<Print3dInfo> value)
        {

        }
        #endregion

        #region AdditionalSettings
        [ObservableProperty]
        bool applyEnhancedMarginSettings = false;

        [ObservableProperty]
        bool excludePrinterCostsFromMarginCalculation = false;

        [ObservableProperty]
        bool excludeMaterialCostsFromMarginCalculation = false;

        [ObservableProperty]
        bool excludeWorkstepsFromMarginCalculation = false;

        #endregion

        #region ProcedureSpecific
        [ObservableProperty]
        bool applyProcedureSpecificAdditions = false;

        [ObservableProperty]
        Material3dFamily procedure = Material3dFamily.Misc;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<CalculationProcedureAttribute> procedureAttributes = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        ObservableCollection<ProcedureAddition> procedureAdditions = [];

        #endregion

        #region Calculated
        [Ignore, XmlIgnore, JsonIgnore]
        public int TotalQuantity => GetTotalQuantity();

        [Ignore, JsonIgnore]
        public double TotalPrintTime
        {
            get
            {
                if (IsCalculated)
                    return GetTotalPrintTime();
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double TotalVolume
        {
            get
            {
                if (IsCalculated)
                    return GetTotalVolume();
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double TotalMaterialUsed
        {
            get
            {
                if (IsCalculated)
                    return GetTotalMaterialUsed();
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double MachineCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Machine) + GetTotalCosts(CalculationAttributeType.Energy);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double MaterialCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Material);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double ItemsCost
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.AdditionalItem);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double EnergyCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Energy);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double HandlingCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.FixCost);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double CustomAdditionCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.CustomAddition);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double WorkstepCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Workstep);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double CalculatedMargin
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Margin);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double CalculatedTax
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.Tax);
                else
                    return 0;
            }
        }
        [Ignore, XmlIgnore, JsonIgnore]
        public double CostsPerPiece
        {
            get
            {
                if (IsCalculated)
                    return TotalCosts / TotalQuantity;
                else
                    return 0;
            }
        }

        #endregion

        #endregion

        #region EventHandlers
        public event EventHandler Error;
        protected virtual void OnError()
        {
            Error?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
        protected virtual void OnError(UnhandledExceptionEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        public event EventHandler<CalculatorEventArgs> RecalculationNeeded;
        protected virtual void OnRecalculationNeeded(CalculatorEventArgs e)
        {
            RecalculationNeeded?.Invoke(this, e);
        }

        public event EventHandler<PrinterChangedEventArgs> PrinterChanged;
        protected virtual void OnPrinterChangedEvent(PrinterChangedEventArgs e)
        {
            PrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialChangedEventArgs> MaterialChanged;
        protected virtual void OnMaterialChangedEvent(MaterialChangedEventArgs e)
        {
            MaterialChanged?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        public Calculation3dEnhanced()
        {
            Id = Guid.NewGuid();
            PrintInfos = [];
        }
        #endregion

        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Calculation3dEnhanced item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
