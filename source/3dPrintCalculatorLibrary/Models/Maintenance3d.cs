using AndreasReitberger.Print3d.Models.MaintenanceAdditions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.Models
{
    [Table("Maintenances")]
    public class Maintenance3d : ICloneable
    {

        #region Clone
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Printer3d))]
        public Guid PrinterId { get; set; }

        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double Duration { get; set; }

        public double AdditionalCosts { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Sparepart> Spareparts { get; set; } = new();

        public double Costs => Spareparts?.Sum(sparepart => sparepart.Costs) ?? 0 + AdditionalCosts;
        #endregion

        #region Constructor
        public Maintenance3d() 
        {
            Id = Guid.NewGuid();
        }
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
