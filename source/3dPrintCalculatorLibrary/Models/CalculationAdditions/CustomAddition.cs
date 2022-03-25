using AndreasReitberger.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CalculationAdditions
{
    [Table("CustomAdditions")]
    public class CustomAddition : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public int Order { get; set; } = 0;
        public CustomAdditionCalculationType CalculationType { get; set; }
        #endregion

        #region Constructor
        public CustomAddition() 
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
