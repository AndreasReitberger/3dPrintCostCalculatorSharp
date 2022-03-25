using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Enums;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models.CalculationAdditions
{
    [Table("CalculationProcedureParameter")]
    public class CalculationProcedureParameter
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        [ForeignKey(typeof(CalculationProcedureAttribute))]
        public Guid CalculationProcedureAttributeId
        { get; set; }
        public ProcedureParameter Type
        { get; set; }
        public double Value
        { get; set; } = 0;
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CalculationProcedureParameterAddition> Additions
        { get; set; } = new();

        [Ignore, Obsolete("Replaced by 'Additions' due to database upgrade")]
        public SerializableDictionary<string, double> AdditionalInformation
        { get; set; } = new SerializableDictionary<string, double>();
        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
