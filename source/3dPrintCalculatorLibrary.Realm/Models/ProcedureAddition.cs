using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Realm.ProcedureAdditions;
using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class ProcedureAddition : RealmObject, ICloneable//, IProcedureAddition
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

        public string Name { get; set; }

        public string Description { get; set; }

        public string ToolTip { get; set; }

        public bool Enabled { get; set; }

        public Material3dFamily TargetFamily
        {
            get => (Material3dFamily)TargetFamilyId;
            set { TargetFamilyId = (int)value; }
        }
        public int TargetFamilyId { get; set; } = (int)Material3dFamily.Filament;

        public ProcedureAdditionTarget Target
        {
            get => (ProcedureAdditionTarget)TargetId;
            set { TargetId = (int)value; }
        }
        public int TargetId { get; set; } = (int)ProcedureAdditionTarget.General;

        #endregion

        #region Collections

        public IList<ProcedureCalculationParameter> Parameters { get; }
        #endregion

        #region Constructor
        public ProcedureAddition() {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public double CalculateCosts()
        {
            double costs = 0;
            foreach(IProcedureCalculationParameter para in Parameters)
            {
                switch (para.Type)
                {
                    case ProcedureCalculationType.ReplacementCosts:
                        costs = (para.Price / para.QuantityInPackage) / para.WearFactor;
                        break;
                    case ProcedureCalculationType.ConsumableGoods:
                        costs = (para.Price / para.QuantityInPackage) * para.AmountTakenForCalculation;
                        break;
                    default:
                        break;
                }
            };
            return costs;
        }

        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not ProcedureAddition item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
