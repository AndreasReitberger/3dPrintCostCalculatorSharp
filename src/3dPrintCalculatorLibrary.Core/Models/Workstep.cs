using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Workstep)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Workstep : ObservableObject, IWorkstep
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Price { get; set; } = 0;
#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(WorkstepCategory))]
        public partial Guid CategoryId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(CategoryId), CascadeOperations = CascadeOperation.All)]
        public partial WorkstepCategory? Category { get; set; }
#else
        [ObservableProperty]
        public partial IWorkstepCategory? Category { get; set; }
#endif
        [ObservableProperty]
        public partial CalculationType CalculationType { get; set; }

        [ObservableProperty]
        public partial WorkstepType Type { get; set; }

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;
        #endregion

        #region Constructors
        public Workstep()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Workstep);

        public override bool Equals(object? obj)
        {
            if (obj is not Workstep item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        public object Clone() => MemberwiseClone();

        #endregion
    }
}
