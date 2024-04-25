using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.ProcedureAdditions
{
    public partial class ProcedureCalculationParameter : RealmObject, IProcedureCalculationParameter
    {
        #region Properties

        [PrimaryKey]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ProcedureCalculationType Type
        {
            get => (ProcedureCalculationType)TypeId;
            set { TypeId = (int)value; }
        }
        public int TypeId { get; set; } = (int)ProcedureCalculationType.ReplacementCosts;

        public double QuantityInPackage { get; set; } = 1;

        public double AmountTakenForCalculation { get; set; } = 1;

        public double Price { get; set; } = 0;

        public double WearFactor { get; set; } = 0;

        public double CalculatedCosts { get; set; }
        #endregion

        #region Ctor
        public ProcedureCalculationParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
