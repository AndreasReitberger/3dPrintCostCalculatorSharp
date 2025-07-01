
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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }
#endif

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string ToolTip { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool Enabled { get; set; } = true;

        [ObservableProperty]
        public partial Material3dFamily TargetFamily { get; set; }

        [ObservableProperty]
        public partial ProcedureAdditionTarget Target { get; set; } = ProcedureAdditionTarget.General;

        #endregion

        #region Collections
#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<ProcedureCalculationParameter> Parameters { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IProcedureCalculationParameter> Parameters { get; set; } = [];
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
