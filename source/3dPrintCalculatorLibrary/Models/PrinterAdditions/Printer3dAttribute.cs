using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.PrinterAdditions
{
    [Table("PrinterAttributes")]
    public class Printer3dAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }
        [ForeignKey(typeof(Printer3d))]
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
