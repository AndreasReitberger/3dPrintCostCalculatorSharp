using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(File3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class File3d : ObservableObject, IFile3d
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        object? file;

        [ObservableProperty]
        string fileName = string.Empty;

        [ObservableProperty]
        string filePath = string.Empty;
        partial void OnFilePathChanged(string value)
        {
            if (value is not null)
            {
                FileName = new FileInfo(value)?.Name ?? string.Empty;
                if (string.IsNullOrEmpty(Name)) Name = FileName;
            }
        }

        [ObservableProperty]
        double volume = 0;

        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
        [property: ManyToOne(nameof(ModelWeightId), CascadeOperations = CascadeOperation.All)]
        File3dWeight weight = new(-1, Core.Enums.Unit.Gram);
#else
        IFile3dWeight weight = new File3dWeight(-1, Enums.Unit.Gram);
#endif

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        byte[] image = [];
        #endregion

        #region Constructor
        public File3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Overrides
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not File3d item)
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
