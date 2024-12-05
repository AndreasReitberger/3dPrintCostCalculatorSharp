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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(ProcedureAddition))]
        Guid procedureAdditionId;
#endif

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        ProcedureCalculationType type;

        [ObservableProperty]
        double quantityInPackage = 1;

        [ObservableProperty]
        double amountTakenForCalculation = 1;

        [ObservableProperty]
        double price = 0;
        partial void OnPriceChanged(double value)
        {
            CalculatedCosts = value / WearFactor;
        }

        [ObservableProperty]
        double wearFactor = 0;
        partial void OnWearFactorChanged(double value)
        {
            CalculatedCosts = Price / value;
        }

        [ObservableProperty]
        double calculatedCosts;
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
