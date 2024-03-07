using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core
{
    public partial class ProcedureAddition : ObservableObject, ICloneable, IProcedureAddition
    {
        #region Properties 
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        string toolTip = string.Empty;

        [ObservableProperty]
        bool enabled = true;

        [ObservableProperty]
        Material3dFamily targetFamily;

        [ObservableProperty]
        ProcedureAdditionTarget target = ProcedureAdditionTarget.General;

        #endregion

        #region Collections

        [ObservableProperty]
        IList<IProcedureCalculationParameter> parameters = [];
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
            };
            return costs;
        }

        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not ProcedureAddition item)
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
