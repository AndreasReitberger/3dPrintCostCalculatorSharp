using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(CalculationProcedureParameter)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationProcedureParameter : ObservableObject, ICalculationProcedureParameter
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(CalculationProcedureAttribute))]
#endif
        [ObservableProperty]
        public partial Guid CalculationProcedureAttributeId { get; set; }

        [ObservableProperty]
        public partial ProcedureParameter Type { get; set; }

        [ObservableProperty]
        public partial double Value { get; set; } = 0;

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<CalculationProcedureParameterAddition> Additions { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<ICalculationProcedureParameterAddition> Additions { get; set; } = [];
#endif

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.CalculationProcedureParameter);
        #endregion
    }
}
