using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.Events;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Calculation3dEnhanced : ObservableObject, ICalculation3dEnhanced, ICloneable
    {

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        #region Basics

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial CalculationState State { get; set; } = CalculationState.Draft;

        [ObservableProperty]
        public partial DateTimeOffset Created { get; set; } = DateTime.Now;

        [ObservableProperty]
        public partial Guid CustomerId { get; set; }

        [ObservableProperty]
        public partial Customer3d? Customer { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial bool IsCalculated { get; set; } = false;

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial bool RecalculationRequired { get; set; } = false;

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
        public partial int Quantity { get; set; } = 1;

        [ObservableProperty]
        public partial double PowerLevel { get; set; } = 0;

        [ObservableProperty]
        public partial double FailRate { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCostsPerkWh { get; set; } = 0;

        [ObservableProperty]
        public partial bool ApplyEnergyCost { get; set; } = false;

        [ObservableProperty]
        public partial double TotalCosts { get; set; } = 0;

        [ObservableProperty]
        [Obsolete("Not needed anymore, will be removed")]
        public partial bool CombineMaterialCosts { get; set; } = false;

        [ObservableProperty]
        public partial bool DifferFileCosts { get; set; } = true;

        #endregion

        #region Details
        public List<Printer3d?> AvailablePrinters => PrintInfos.Select(pi => pi.Printer).Distinct().ToList();

        public List<Material3d?> AvailableMaterials => PrintInfos.SelectMany(pi => pi.MaterialUsages).Select(mu => mu.Material).Distinct().ToList();

        [ObservableProperty]
        public partial List<CustomAddition> CustomAdditions { get; set; } = [];

        [ObservableProperty]
        public partial List<WorkstepUsage> WorkstepUsages { get; set; } = [];

        [ObservableProperty]
        public partial List<Item3dUsage> AdditionalItems { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<CalculationAttribute> PrintTimes { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<CalculationAttribute> MaterialUsage { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<CalculationAttribute> OverallMaterialCosts { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<CalculationAttribute> OverallPrinterCosts { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<CalculationAttribute> Costs { get; set; } = [];

        [ObservableProperty]
        public partial List<CalculationAttribute> Rates { get; set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        public partial List<Print3dInfo> PrintInfos { get; set; } = [];

        partial void OnPrintInfosChanged(List<Print3dInfo> value)
        {

        }
        #endregion

        #region AdditionalSettings
        [ObservableProperty]
        public partial bool ApplyEnhancedMarginSettings { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludePrinterCostsFromMarginCalculation { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludeMaterialCostsFromMarginCalculation { get; set; } = false;

        [ObservableProperty]
        public partial bool ExcludeWorkstepsFromMarginCalculation { get; set; } = false;

        #endregion

        #region ProcedureSpecific
        [ObservableProperty]
        public partial bool ApplyProcedureSpecificAdditions { get; set; } = false;

        [ObservableProperty]
        public partial Material3dFamily Procedure { get; set; } = Material3dFamily.Misc;

        [ObservableProperty]
        public partial ObservableCollection<CalculationProcedureAttribute> ProcedureAttributes { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<ProcedureAddition> ProcedureAdditions { get; set; } = [];

        #endregion

        #region Calculated
        [XmlIgnore, JsonIgnore]
        public int TotalQuantity => GetTotalQuantity();

        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        [XmlIgnore, JsonIgnore]
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
        public event EventHandler? Error;
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

        public event EventHandler<CalculatorEventArgs>? RecalculationNeeded;
        protected virtual void OnRecalculationNeeded(CalculatorEventArgs e)
        {
            RecalculationNeeded?.Invoke(this, e);
        }

        public event EventHandler<PrinterChangedEventArgs>? PrinterChanged;
        protected virtual void OnPrinterChangedEvent(PrinterChangedEventArgs e)
        {
            PrinterChanged?.Invoke(this, e);
        }

        public event EventHandler<MaterialChangedEventArgs>? MaterialChanged;
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
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
