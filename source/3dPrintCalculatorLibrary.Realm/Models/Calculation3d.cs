using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Realm.Interfaces;
using AndreasReitberger.Print3d.Realm.CalculationAdditions;
using AndreasReitberger.Print3d.Realm.Events;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using ErrorEventArgs = System.IO.ErrorEventArgs;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class Calculation3d : RealmObject, ICalculation3d, ICloneable
    {

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        #region Basics
        [Required]
        public string Name { get; set; } = string.Empty;

        public DateTimeOffset Created { get; set; } = DateTime.Now;

        public Guid PrinterId { get; set; }

        Printer3d printer { get; set; }
        public Printer3d Printer
        {
            get => printer;
            set
            {
                printer = value;
                OnPrinterChanged(value);
            }
        }
        void OnPrinterChanged(Printer3d value)
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

        public Guid MaterialId { get; set; }

        Material3d material { get; set; }
        public Material3d Material
        {
            get => material;
            set
            {
                material = value;
                OnMaterialChanged(value);
            }
        }
        void OnMaterialChanged(Material3d value)
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

        public Guid CustomerId { get; set; }

        public Customer3d Customer { get; set; }

        public bool IsCalculated { get; private set; } = false;

        bool recalculationRequired { get; set; } = false;
        public bool RecalculationRequired
        {
            get => recalculationRequired;
            set
            {
                recalculationRequired = value;
                OnRecalculationRequiredChanged(value);
            }
        }
        void OnRecalculationRequiredChanged(bool value)
        {
            if (value)
            {
                OnRecalculationNeeded(new()
                {
                    CalculationId = Id,
                });
            }
        }

        public int Quantity { get; set; } = 1;

        public double PowerLevel { get; set; } = 0;

        public double FailRate { get; set; } = 0;

        public double EnergyCostsPerkWh { get; set; } = 0;

        public bool ApplyEnergyCost { get; set; } = false;

        public double TotalCosts { get; set; } = 0;

        public bool CombineMaterialCosts { get; set; } = false;

        public bool DifferFileCosts { get; set; } = true;

        #endregion

        #region Details

        public IList<Printer3d> Printers { get; }

        public IList<Material3d> Materials { get; }

        public IList<CustomAddition> CustomAdditions { get; }

        public IList<WorkstepUsage> WorkstepUsages { get; }

        [Obsolete("Use the WorkstepUsages class instead")]
        public IList<Workstep> WorkSteps { get; }

        [Obsolete("Use the WorkstepUsages class instead")]
        public IList<WorkstepDuration> WorkStepDurations { get; }

        public IList<Item3dUsage> AdditionalItems { get; }

        public IList<CalculationAttribute> PrintTimes { get; }

        public IList<CalculationAttribute> MaterialUsage { get; }

        public IList<CalculationAttribute> OverallMaterialCosts { get; }

        public IList<CalculationAttribute> OverallPrinterCosts { get; }

        public IList<CalculationAttribute> Costs { get; }

        public IList<CalculationAttribute> Rates { get; }

        public IList<File3d> Files { get; }
        #endregion

        #region AdditionalSettings

        public bool ApplyEnhancedMarginSettings { get; set; } = false;

        public bool ExcludePrinterCostsFromMarginCalculation { get; set; } = false;

        public bool ExcludeMaterialCostsFromMarginCalculation { get; set; } = false;

        public bool ExcludeWorkstepsFromMarginCalculation { get; set; } = false;

        #endregion

        #region ProcedureSpecific

        public bool ApplyProcedureSpecificAdditions { get; set; } = false;

        public Material3dFamily Procedure
        {
            get => (Material3dFamily)ProcedureId;
            set { ProcedureId = (int)value; }
        }
        public int ProcedureId { get; set; } = (int)Material3dFamily.Misc;

        public IList<CalculationProcedureAttribute> ProcedureAttributes { get; }

        public IList<ProcedureAddition> ProcedureAdditions { get; }
        #endregion

        #region Calculated
        [JsonIgnore, Ignored]
        public int TotalQuantity => GetTotalQuantity();

        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
        [JsonIgnore, Ignored]
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
