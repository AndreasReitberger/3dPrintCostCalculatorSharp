using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Utilities;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models.FileAdditions
{
    [Table("ModelWeights")]
    public class ModelWeight : BaseModel
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [ForeignKey(typeof(File3d))]
        public Guid FileId { get; set; }

        bool _recalculateWeightInGramm = false;

        [JsonProperty(nameof(Weight))]
        double _weight = 0;
        [JsonIgnore]
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (_weight == value) return;
                _recalculateWeightInGramm = true;
                _weight = value;
                //SetProperty(ref _weight, value);
                RecalculateWeight();
                OnPropertyChanged();
            }
        }

        [JsonProperty(nameof(Unit))]
        Unit _unit = Unit.g;
        [JsonIgnore]
        public Unit Unit
        {
            get { return _unit; }
            set
            {
                if (_unit == value) return;
                _recalculateWeightInGramm = true;
                _unit = value;
                RecalculateWeight();
                //SetProperty(ref _unit, value);
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        [JsonProperty(nameof(WeightInGramm))]
        double _weightInGramm = 0;
        [JsonIgnore, XmlIgnore]
        public double WeightInGramm
        {
            get => _weightInGramm;
            set
            {
                if (_weightInGramm == value) return;
                _weightInGramm = value;
                OnPropertyChanged();
            }
        }
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
            if (_recalculateWeightInGramm)
            {
                _recalculateWeightInGramm = false;
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
