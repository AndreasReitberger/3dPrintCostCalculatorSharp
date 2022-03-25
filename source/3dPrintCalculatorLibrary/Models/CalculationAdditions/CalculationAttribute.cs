using AndreasReitberger.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CalculationAdditions
{
    [Table("CalculationAttributes")]
    public class CalculationAttribute
    {
        #region Properties

        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }

        public Guid LinkedId { get; set; } = Guid.Empty;
        public string Attribute { get; set; }
        public CalculationAttributeType Type { get; set; }
        public double Value { get; set; }
        public bool IsPercentageValue { get; set; } = false;
        public bool SkipForCalculation { get; set; } = false;
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
