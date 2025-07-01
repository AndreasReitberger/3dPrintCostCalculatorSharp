using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(ProcedureCalculationParameter)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class ProcedureCalculationParameter : ObservableObject, IProcedureCalculationParameter
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(ProcedureAddition))]
        public partial Guid ProcedureAdditionId { get; set; }
#endif

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ProcedureCalculationType Type { get; set; }

        [ObservableProperty]
        public partial double QuantityInPackage { get; set; } = 1;

        [ObservableProperty]
        public partial double AmountTakenForCalculation { get; set; } = 1;

        [ObservableProperty]
        public partial double Price { get; set; } = 0;
        partial void OnPriceChanged(double value)
        {
            CalculatedCosts = value / WearFactor;
        }

        [ObservableProperty]
        public partial double WearFactor { get; set; } = 0;
        partial void OnWearFactorChanged(double value)
        {
            CalculatedCosts = Price / value;
        }

        [ObservableProperty]
        public partial double CalculatedCosts { get; set; }
        #endregion

        #region Ctor
        public ProcedureCalculationParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
