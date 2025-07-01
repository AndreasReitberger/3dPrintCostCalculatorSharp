using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(File3dWeight)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class File3dWeight : ObservableObject, IFile3dWeight
    {
        #region Properties
#if SQL
        [property: PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

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
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        #endregion
    }
}
