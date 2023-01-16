using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models.FileAdditions
{
    public partial class ModelWeight : ObservableObject, IModelWeight
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        public Guid fileId;

        [ObservableProperty]
        [property: JsonIgnore]
        bool recalculateWeightInGramm = false;

        [ObservableProperty]
        [property: JsonIgnore]
        double weight = 0;
        partial void OnWeightChanged(double value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        [property: JsonIgnore]
        Unit unit = Unit.g;
        partial void OnUnitChanged(Unit value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        [property: JsonIgnore]
        double weightInGramm = 0;

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
