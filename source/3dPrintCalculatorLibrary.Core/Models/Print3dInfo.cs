using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Print3dInfo)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Print3dInfo : ObservableObject, IPrint3dInfo, ICloneable
    {

        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        public partial File3dUsage? FileUsage { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        public partial Printer3d? Printer { get; set; }

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Item3dUsage> Items { get; set; } = [];

        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Material3dUsage> Materials { get; set; } = [];
#else
        [ObservableProperty]
        public partial IFile3dUsage? FileUsage { get; set; }

        [ObservableProperty]
        public partial IPrinter3d? Printer { get; set; }

        [ObservableProperty]
        public partial IList<IItem3dUsage> Items { get; set; } = [];
        [ObservableProperty]
        public partial IList<IMaterial3dUsage> Materials { get; set; } = [];
#endif

        #endregion

        #region Constructor

        public Print3dInfo()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Print3dInfo item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        public object Clone() => MemberwiseClone();

        #endregion

    }
}
