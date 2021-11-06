using AndreasReitberger.Core.Utilities;
//using SQLite;
using System;

namespace AndreasReitberger.Models
{
    public class Maintenance3d : BaseModel
    {
        #region Properties
        //[PrimaryKey]
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public double Costs { get; set; }
        #endregion

        #region Constructor
        public Maintenance3d() { }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return this.Description;
        }
        public override bool Equals(object obj)
        {
            if (obj is not Maintenance3d item)
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
