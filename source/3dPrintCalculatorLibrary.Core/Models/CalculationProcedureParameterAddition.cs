using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(CalculationProcedureParameterAddition)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationProcedureParameterAddition : ObservableObject, ICalculationProcedureParameterAddition
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(CalculationProcedureParameter))]
#endif
        Guid calculationProcedureParameterId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value = 0;

        #endregion

        #region Constructor
        public CalculationProcedureParameterAddition()
        {
            Id = Guid.NewGuid();
        }
        public CalculationProcedureParameterAddition(string name, double value)
        {
            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }
        #endregion
    }
}
