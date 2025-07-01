using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Supplier)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Supplier : ObservableObject, ICloneable, ISupplier
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string DebitorNumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsActive { get; set; }

        [ObservableProperty]
        public partial string Website { get; set; } = string.Empty;

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Manufacturer> Manufacturers { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IManufacturer> Manufacturers { get; set; } = [];
#endif
        #endregion

        #region Constructor
        public Supplier() { }
        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Supplier item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}
