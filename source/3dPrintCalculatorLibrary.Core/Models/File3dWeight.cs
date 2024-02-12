using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class File3dWeight : ObservableObject, IFile3dWeight
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        bool recalculateWeightInGramm = false;

        [ObservableProperty]
        double weight = 0;
        partial void OnWeightChanged(double value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        Unit unit = Unit.Gram;
        partial void OnUnitChanged(Unit value)
        {
            RecalculateWeightInGramm = true;
            RecalculateWeight();
        }

        [ObservableProperty]
        double weightInGramm = 0;

        #endregion

        #region Constructor
        public File3dWeight()
        {
            Id = Guid.NewGuid();
        }
        public File3dWeight(double weight, Unit unit)
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
        public override string ToString()
        {
            return string.Format("{0} {1}", Weight, Unit);
        }
        #endregion
    }
}
