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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        Guid slicerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(SlicerId), CascadeOperations = CascadeOperation.All)]
        Slicer3d? slicer;
#else

        [ObservableProperty]
        ISlicer3d? slicer;
#endif

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string command = string.Empty;

        [ObservableProperty]
        string outputFilePatternString = string.Empty;

        [ObservableProperty]
        bool autoAddFilePath = false;

        #endregion

        #region Constructor
        public Slicer3dCommand()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
