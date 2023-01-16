using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.PrinterAdditions
{
    [Table("PrinterAttributes")]
    public partial class Printer3dAttribute : ObservableObject, IPrinter3dAttribute
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Printer3d))]
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
