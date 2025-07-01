using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models.FileAdditions
{
    public partial class ModelWeight : ObservableObject, IModelWeight
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        public partial bool RecalculateWeightInGramm { get; set; } = false;

        [ObservableProperty]
        public partial double Weight { get; set; } = 0;

        partial void OnWeightChanged(double value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        public partial Unit Unit { get; set; } = Unit.Gram;

        partial void OnUnitChanged(Unit value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        public partial double WeightInGramm { get; set; } = 0;

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
        public void RecalculateWeight()
        {
            if (RecalculateWeightInGramm)
            {
                RecalculateWeightInGramm = false;
                WeightInGramm = Weight * UnitFactor.GetUnitFactor(Unit);
            }
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
