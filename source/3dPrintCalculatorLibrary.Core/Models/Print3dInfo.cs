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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationEnhancedId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid fileId;

        [ObservableProperty]
        [property: ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        File3dUsage? fileUsage;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid printerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        Printer3d? printer;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Item3dUsage> items = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Material3dUsage> materials = [];
#else
        [ObservableProperty]
        IFile3dUsage? fileUsage;

        [ObservableProperty]
        IPrinter3d? printer;

        [ObservableProperty]
        IList<IItem3dUsage> items = [];

        [ObservableProperty]
        IList<IMaterial3dUsage> materials = [];
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
