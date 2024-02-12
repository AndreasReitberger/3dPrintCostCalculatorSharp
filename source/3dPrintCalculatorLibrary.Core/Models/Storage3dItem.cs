using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Storage3dItem : ObservableObject, IStorage3dItem
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        IMaterial3d? material;

        [ObservableProperty]
        double amount;
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
