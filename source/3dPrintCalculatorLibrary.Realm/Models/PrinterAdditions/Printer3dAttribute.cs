using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AndreasReitberger.Print3d.Realm.PrinterAdditions
{
    public partial class Printer3dAttribute : ObservableObject, IPrinter3dAttribute
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid printerId;

        [ObservableProperty]
        public string attribute;

        [ObservableProperty]
        public double value;
        #endregion

        #region Constructor
        public Printer3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
