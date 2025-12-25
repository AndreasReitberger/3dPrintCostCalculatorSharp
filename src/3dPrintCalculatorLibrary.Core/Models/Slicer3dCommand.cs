using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Slicer3dCommand)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Slicer3d))]
        public partial Guid SlicerId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(SlicerId), CascadeOperations = CascadeOperation.All)]
        public partial Slicer3d? Slicer { get; set; }
#else

        [ObservableProperty]
        public partial ISlicer3d? Slicer { get; set; }
#endif

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Command { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string OutputFilePatternString { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool AutoAddFilePath { get; set; } = false;

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override

        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Slicer3dCommand);

        #endregion
    }
}
