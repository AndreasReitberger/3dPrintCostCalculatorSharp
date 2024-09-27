using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.FileAdditions
{
    [Table($"{nameof(File3dUsage)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class File3dUsage : ObservableObject, ICloneable, IFile3dUsage
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Print3dInfo))]
        Guid printInfoId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid fileId;

        [ObservableProperty]
        [property: ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        File3d? file;
#else
        [ObservableProperty]
        IFile3d? file;
#endif

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
        double printTimeQuantityFactor = 1;

        #endregion

        #region Constructor
        public File3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not File3dUsage item)
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
