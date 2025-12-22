using CommunityToolkit.Mvvm.ComponentModel;

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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Supplier))]
        public partial Guid SupplierId { get; set; }
#endif

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string DebitorNumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsActive { get; set; } = true;

        [ObservableProperty]
        public partial string Website { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string CountryCode { get; set; } = string.Empty;
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
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Manufacturer);
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
