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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(CalculationProcedureAttribute))]
#endif
        Guid calculationProcedureAttributeId;

        [ObservableProperty]
        ProcedureParameter type;

        [ObservableProperty]
        double value = 0;

        [ObservableProperty]
#if SQL
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<CalculationProcedureParameterAddition> additions = [];
#else
        IList<ICalculationProcedureParameterAddition> additions = [];
#endif

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
