
using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(ProcedureAddition)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class ProcedureAddition : ObservableObject, ICloneable, IProcedureAddition
    {
        #region Properties 
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;
#endif

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
#if SQL


        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<ProcedureCalculationParameter> parameters = [];
#else
        [ObservableProperty]
        IList<IProcedureCalculationParameter> parameters = [];
#endif
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
#if SQL
            foreach (ProcedureCalculationParameter para in Parameters)
#else
            foreach (IProcedureCalculationParameter para in Parameters)
#endif
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
