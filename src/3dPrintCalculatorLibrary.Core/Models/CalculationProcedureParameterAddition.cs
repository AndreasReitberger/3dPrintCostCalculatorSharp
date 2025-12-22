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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(CalculationProcedureParameter))]
#endif
        [ObservableProperty]
        public partial Guid CalculationProcedureParameterId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Value { get; set; } = 0;

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

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.CalculationProcedureParameterAddition);
        #endregion
    }
}
