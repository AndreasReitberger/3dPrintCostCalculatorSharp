using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.Events;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Calculation3d : ObservableObject, ICalculation3d, ICloneable
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        #region Basics
        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        DateTimeOffset created = DateTime.Now;

        [ObservableProperty]
        Guid printerId;

        [ObservableProperty]
        Printer3d printer;
        partial void OnPrinterChanged(Printer3d value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnPrinterChangedEvent(new()
                {
                    CalculationId = Id,
                    Printer = value,
                });
            }
        }

        [ObservableProperty]
        Guid materialId;

        [ObservableProperty]
        Material3d material;
        partial void OnMaterialChanged(Material3d value)
        {
            if (!RecalculationRequired)
            {
                RecalculationRequired = true;
                IsCalculated = false;
                OnMaterialChangedEvent(new()
                {
                    CalculationId = Id,
                    Material = value,
                });
            }
        }

        [ObservableProperty]
        Guid customerId;

        [ObservableProperty]
        Customer3d customer;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        bool isCalculated = false;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
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
        bool combineMaterialCosts = false;

        [ObservableProperty]
        bool differFileCosts = true;

        #endregion

        #region Details
        [ObservableProperty]
        List<Printer3d> printers = new();

        [ObservableProperty]
        List<Material3d> materials = new();

        [ObservableProperty]
        List<CustomAddition> customAdditions = new();

        [ObservableProperty]
        List<WorkstepUsage> workstepUsages = new();

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsages class instead")]
        List<Workstep> workSteps = new();

        [ObservableProperty]
        [Obsolete("Use the WorkstepUsages class instead")]
        List<WorkstepDuration> workStepDurations = new();

        [ObservableProperty]
        List<Item3dUsage> additionalItems = new();

        [ObservableProperty]
        ObservableCollection<CalculationAttribute> printTimes = new();

        [ObservableProperty]
        ObservableCollection<CalculationAttribute> materialUsage = new();

        [ObservableProperty]
        ObservableCollection<CalculationAttribute> overallMaterialCosts = new();

        [ObservableProperty]
        ObservableCollection<CalculationAttribute> overallPrinterCosts = new();

        [ObservableProperty]
        ObservableCollection<CalculationAttribute> costs = new();

        [ObservableProperty]
        List<CalculationAttribute> rates = new();

        [ObservableProperty]
        List<File3d> files = new();

        [ObservableProperty]
        List<Print3dInfo> printInfos = new();
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
        ObservableCollection<CalculationProcedureAttribute> procedureAttributes = new();

        [ObservableProperty]
        ObservableCollection<IProcedureAddition> procedureAdditions = new();
        #endregion

        #region Calculated
        [JsonIgnore]
        public int TotalQuantity => GetTotalQuantity();

        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
        public double ItemsCosts
        {
            get
            {
                if (IsCalculated)
                    return GetTotalCosts(CalculationAttributeType.AdditionalItem);
                else
                    return 0;
            }
        }
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        [JsonIgnore]
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
        public Calculation3d()
        {
            Id = Guid.NewGuid();
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
        public override bool Equals(object obj)
        {
            if (obj is not Calculation3d item)
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
