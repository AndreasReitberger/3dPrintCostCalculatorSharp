using CommunityToolkit.Mvvm.ComponentModel;

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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string debitorNumber = string.Empty;

        [ObservableProperty]
        bool isActive;

        [ObservableProperty]
        string website = string.Empty;

        [ObservableProperty]
#if SQL
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Manufacturer> manufacturers = [];
#else
        IList<IManufacturer> manufacturers = [];
#endif
        #endregion

        #region Constructor
        public Supplier() { }
        #endregion

        #region Override
        public override string ToString() => string.IsNullOrEmpty(DebitorNumber) ? Name : $"{Name} ({DebitorNumber})";
        
        public override bool Equals(object? obj)
        {
            if (obj is not Supplier item)
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
