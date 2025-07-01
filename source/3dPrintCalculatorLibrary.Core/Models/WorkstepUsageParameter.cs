using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        WorkstepUsageParameterType parameterType = WorkstepUsageParameterType.Quantity;

        [ObservableProperty]
        double value = 0;
        #endregion

        #region Constructors
        public WorkstepUsageParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not WorkstepUsageParameter item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
