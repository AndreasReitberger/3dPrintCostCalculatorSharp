using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [property: ForeignKey(typeof(Maintenance3d))]
#endif
        [ObservableProperty]
        public partial Guid MaintenanceId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Partnumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Costs { get; set; } = 0;
        #endregion

        #region Constructor
        public Sparepart()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Sparepart item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}
