using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(CalculationProcedureAttribute)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(Calculation3dEnhanced))]
#endif
        [ObservableProperty]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial Material3dFamily Family { get; set; }

        [ObservableProperty]
        public partial ProcedureAttribute Attribute { get; set; }

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<CalculationProcedureParameter> Parameters { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<ICalculationProcedureParameter> Parameters { get; set; } = [];
#endif

        [ObservableProperty]
        public partial CalculationLevel Level { get; set; } = CalculationLevel.Printer;

        /// <summary>
        /// Multiplies the costs per piece (quantity of the files)
        /// </summary>
        [ObservableProperty]
        public partial bool PerPiece { get; set; } = false;

        [ObservableProperty]
        public partial bool PerFile { get; set; } = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.CalculationProcedureAttribute);
        #endregion
    }
}
