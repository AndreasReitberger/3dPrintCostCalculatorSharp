using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.Models
{
    public partial class ProcedureAddition : ObservableObject, ICloneable, IProcedureAddition
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ToolTip { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool Enabled { get; set; }

        [ObservableProperty]
        public partial Material3dFamily TargetFamily { get; set; }

        [ObservableProperty]
        public partial ProcedureAdditionTarget Target { get; set; } = ProcedureAdditionTarget.General;

        #endregion

        #region Collections

        [ObservableProperty]
        public partial ObservableCollection<IProcedureCalculationParameter> Parameters { get; set; } = new();
        #endregion

        #region Constructor
        public ProcedureAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public double CalculateCosts()
        {
            double costs = 0;
            foreach (IProcedureCalculationParameter para in Parameters)
            {
                switch (para.Type)
                {
                    case ProcedureCalculationType.ReplacementCosts:
                        costs = ((para.Price / para.QuantityInPackage) / 100) * para.WearFactor;
                        break;
                    case ProcedureCalculationType.ConsumableGoods:
                        costs = (para.Price / para.QuantityInPackage) * para.AmountTakenForCalculation;
                        break;
                    default:
                        break;
                }
            }
            ;
            return costs;
        }

        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not ProcedureAddition item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
