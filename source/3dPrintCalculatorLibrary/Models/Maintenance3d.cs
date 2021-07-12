using AndreasReitberger.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models
{
    public class Maintenance3d : BaseModel
    {
        #region Properties
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
            var item = obj as Maintenance3d;
            if (item == null)
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
