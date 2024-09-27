using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table($"{nameof(CalculationProcedureAttribute)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
#endif
        Guid calculationId;

        [ObservableProperty]
        Material3dFamily family;

        [ObservableProperty]
        ProcedureAttribute attribute;

        [ObservableProperty]
#if SQL
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<CalculationProcedureParameter> parameters = [];
#else
        IList<ICalculationProcedureParameter> parameters = [];
#endif

        [ObservableProperty]
        CalculationLevel level = CalculationLevel.Printer;

        /// <summary>
        /// Multiplies the costs per piece (quantity of the files)
        /// </summary>
        [ObservableProperty]
        bool perPiece = false;

        [ObservableProperty]
        bool perFile = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
