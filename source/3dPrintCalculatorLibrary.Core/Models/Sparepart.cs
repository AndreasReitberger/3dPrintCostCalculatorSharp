using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Sparepart)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Sparepart : ObservableObject, ISparepart
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
#if SQL
        [property: ForeignKey(typeof(Maintenance3d))]
#endif
        Guid maintenanceId;


        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string partnumber = string.Empty;

        [ObservableProperty]
        double costs = 0;
        #endregion

        #region Constructor
        public Sparepart()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => Name;
        
        public override bool Equals(object? obj)
        {
            if (obj is not Sparepart item)
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
