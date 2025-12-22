using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(WorkstepUsageParameter)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class WorkstepUsageParameter : ObservableObject, IWorkstepUsageParameter
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial WorkstepUsageParameterType ParameterType { get; set; } = WorkstepUsageParameterType.Quantity;

        [ObservableProperty]
        public partial double Value { get; set; } = 0;
        #endregion

        #region Constructors
        public WorkstepUsageParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.WorkstepUsageParameter);
        public override bool Equals(object? obj)
        {
            if (obj is not WorkstepUsageParameter item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
