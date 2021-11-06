//using SQLite;
using System;

namespace AndreasReitberger.Models
{
    public class Manufacturer : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        //[PrimaryKey]
        public Guid Id
        { get; set; }
        public string Name
        { get; set; }
        public string DebitorNumber
        { get; set; }
        public bool IsActive
        { get; set; }
        public string Website
        { get; set; }
        #endregion

        #region Constructor
        public Manufacturer() { }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(DebitorNumber) ? Name : string.Format("{0} ({1})", Name, DebitorNumber);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Manufacturer item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
