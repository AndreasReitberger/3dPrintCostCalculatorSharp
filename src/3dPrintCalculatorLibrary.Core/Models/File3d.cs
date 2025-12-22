using CommunityToolkit.Mvvm.ComponentModel;

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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

#if SQL
        [Ignore]
#endif
        [ObservableProperty]
        [JsonIgnore]
        public partial object? File { get; set; }

        [ObservableProperty]
        public partial string FileName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string FilePath { get; set; } = string.Empty;

        partial void OnFilePathChanged(string value)
        {
            if (value is not null)
            {
                FileName = new FileInfo(value)?.Name ?? string.Empty;
                if (string.IsNullOrEmpty(Name)) Name = FileName;
            }
        }

        [ObservableProperty]
        public partial double Volume { get; set; } = 0;

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(File3dWeight))]
        public partial Guid WeightId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(WeightId), CascadeOperations = CascadeOperation.All)]
        public partial File3dWeight? Weight { get; set; } = new(-1, Core.Enums.Unit.Gram);
#else
        [ObservableProperty]
        public partial IFile3dWeight? Weight { get; set; } = new File3dWeight(-1, Enums.Unit.Gram);
#endif

        [ObservableProperty]
        public partial double PrintTime { get; set; } = 0;

        [ObservableProperty]
        public partial byte[] Image { get; set; } = [];

        [ObservableProperty]
        public partial int Quantity { get; set; } = 0;

        [ObservableProperty]
        public partial bool MultiplyPrintTimeWithQuantity { get; set; } = true;

        [ObservableProperty]
        public partial double PrintTimeQuantityFactor { get; set; } = 1;
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
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.File3d);
        public override bool Equals(object? obj)
        {
            if (obj is not File3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
