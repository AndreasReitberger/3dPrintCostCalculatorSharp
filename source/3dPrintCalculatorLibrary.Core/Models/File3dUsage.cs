using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(File3dUsage)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class File3dUsage : ObservableObject, ICloneable, IFile3dUsage
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Print3dInfo))]
        public partial Guid PrintInfoId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(File3d))]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        public partial File3d? File { get; set; }
#else
        [ObservableProperty]
        public partial IFile3d? File { get; set; }
#endif

        [ObservableProperty]
        public partial int Quantity { get; set; } = 1;

        [ObservableProperty]
        public partial bool MultiplyPrintTimeWithQuantity { get; set; } = true;

        [ObservableProperty]
        public partial double PrintTimeQuantityFactor { get; set; } = 1;

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
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
