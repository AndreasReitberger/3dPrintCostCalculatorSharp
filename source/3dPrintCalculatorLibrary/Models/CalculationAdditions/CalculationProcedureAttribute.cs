using AndreasReitberger.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models.CalculationAdditions
{
    [Table("CalculationProcedureAttribute")]
    public class CalculationProcedureAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId
        { get; set; }
        public Material3dFamily Family
        { get; set; }
        public ProcedureAttribute Attribute 
        { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CalculationProcedureParameter> Parameters
        { get; set; } = new List<CalculationProcedureParameter>();
        public CalculationLevel Level
        { get; set; } = CalculationLevel.Printer;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
