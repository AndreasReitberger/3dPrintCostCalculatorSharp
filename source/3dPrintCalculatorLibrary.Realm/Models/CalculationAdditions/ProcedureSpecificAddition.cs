using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class ProcedureSpecificAddition : RealmObject, IProcedureSpecificAddition
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Printer3dType Procedure
        {
            get => (Printer3dType)ProcedureId;
            set { ProcedureId = (int)value; }
        }
        public int ProcedureId { get; set; } = (int)Printer3dType.FDM;

        public ProcedureSpecificCalculationType CalculationType
        {
            get => (ProcedureSpecificCalculationType)ProcedureId;
            set { ProcedureId = (int)value; }
        }
        public int CalculationTypeId { get; set; } = (int)ProcedureSpecificCalculationType.PerPart;

        public double Addition { get; set; }

        public bool IsPercantageAddition { get; set; }
        #endregion
    }
}
