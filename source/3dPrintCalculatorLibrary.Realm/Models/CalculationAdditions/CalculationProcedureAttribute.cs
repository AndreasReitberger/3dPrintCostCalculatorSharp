using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class CalculationProcedureAttribute : RealmObject, ICalculationProcedureAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationEnhancedId { get; set; }

        public Material3dFamily Family
        {
            get => (Material3dFamily)FamilyId;
            set { FamilyId = (int)value; }
        }
        public int FamilyId { get; set; }

        public ProcedureAttribute Attribute
        {
            get => (ProcedureAttribute)AttributeId;
            set { AttributeId = (int)value; }
        }
        public int AttributeId { get; set; }

        public IList<CalculationProcedureParameter> Parameters { get; }

        public CalculationLevel Level
        {
            get => (CalculationLevel)LevelId;
            set { LevelId = (int)value; }
        }
        public int LevelId { get; set; } = (int)CalculationLevel.Printer;

        public bool PerFile { get; set; } = false;
        public bool PerPiece { get; set; } = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
