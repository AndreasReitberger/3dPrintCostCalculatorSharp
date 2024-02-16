using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Slicer3dCommand : ObservableObject, ISlicer3dCommand
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        ISlicer3d? slicer;

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
