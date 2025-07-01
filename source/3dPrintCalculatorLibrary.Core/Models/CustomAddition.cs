using AndreasReitberger.Print3d.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(CustomAddition)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class CustomAddition : ObservableObject, ICloneable, ICustomAddition
    {
        #region Properties
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Percentage { get; set; } = 1;

        [ObservableProperty]
        public partial int Order { get; set; } = 0;

        [ObservableProperty]
        public partial CustomAdditionCalculationType CalculationType { get; set; }
        #endregion

        #region Constructor
        public CustomAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

    }
}
