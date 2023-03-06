using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Utilities;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.FileAdditions
{
    public partial class ModelWeight : RealmObject, IModelWeight
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid FileId { get; set; }

        public bool RecalculateWeightInGramm { get; set; } = false;

        double weight { get; set; } = 0;
        public double Weight
        {
            get => weight;
            set
            {
                weight = value;
                OnWeightChanged(value);
            }
        }
        void OnWeightChanged(double value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        public Unit Unit
        {
            get => (Unit)UnitId;
            set
            {
                UnitId = (int)value;
                OnUnitChanged(value);
            }
        }
        int UnitId { get; set; } = (int)Unit.Gramm;
        void OnUnitChanged(Unit value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        public double WeightInGramm { get; set; } = 0;

        #endregion

        #region Constructor
        public ModelWeight()
        {
            Id = Guid.NewGuid();
        }
        public ModelWeight(double weight, Unit unit)
        {
            Id = Guid.NewGuid();
            Weight = weight;
            Unit = unit;
        }
        #endregion

        #region Methods
        void RecalculateWeight()
        {
            if (RecalculateWeightInGramm)
            {
                RecalculateWeightInGramm = false;
                WeightInGramm = Weight * UnitFactor.GetUnitFactor(Unit);
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0} {1}", Weight, Unit);
        }
        #endregion
    }
}
