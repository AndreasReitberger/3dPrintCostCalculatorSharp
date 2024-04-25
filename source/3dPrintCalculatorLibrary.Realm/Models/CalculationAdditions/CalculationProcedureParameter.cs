using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class CalculationProcedureParameter : RealmObject, ICalculationProcedureParameter
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationProcedureAttributeId { get; set; }

        public ProcedureParameter Type
        {
            get => (ProcedureParameter)TypeId;
            set { TypeId = (int)value; }
        }
        public int TypeId { get; set; }

        public double Value { get; set; } = 0;

        public IList<CalculationProcedureParameterAddition> Additions { get; } = [];

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
