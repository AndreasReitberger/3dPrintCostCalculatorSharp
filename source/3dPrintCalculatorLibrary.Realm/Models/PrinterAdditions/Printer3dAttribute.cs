using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.PrinterAdditions
{
    public partial class Printer3dAttribute : RealmObject, IPrinter3dAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid PrinterId { get; set; }

        public string Attribute { get; set; }

        public double Value { get; set; }
        #endregion

        #region Constructor
        public Printer3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
