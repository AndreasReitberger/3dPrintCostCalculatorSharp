using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Manufacturer)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Manufacturer : ObservableObject, ICloneable, IManufacturer
    {
        #region Properties 
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Supplier))]
        Guid supplierId;
#endif

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string debitorNumber = string.Empty;

        [ObservableProperty]
        bool isActive = true;

        [ObservableProperty]
        string website = string.Empty;

        [ObservableProperty]
        string note = string.Empty;

        [ObservableProperty]
        string countryCode = string.Empty;
        #endregion

        #region Constructor
        public Manufacturer()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Manufacturer item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        
        #endregion
    }
}
