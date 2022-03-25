using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CalculationAdditions
{
    [Table("CalculationProcedureParameterAddition")]
    public class CalculationProcedureParameterAddition
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(CalculationProcedureParameter))]
        public Guid CalculationProcedureParameterId
        { get; set; }
        public string Name
        { get; set; }
        public double Value
        { get; set; } = 0;

        #endregion

        #region Constructor
        public CalculationProcedureParameterAddition()
        {
            Id = Guid.NewGuid();
        }
        public CalculationProcedureParameterAddition(string name, double value)
        {
            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }
        #endregion
    }
}
