using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IModelWeight
    {
        #region Properties
        public Guid Id { get; set; }
        public bool RecalculateWeightInGramm { get; set; }
        public double Weight { get; set; }
        public Unit Unit { get; set; }
        public double WeightInGramm { get; set; }
        #endregion

        #region Methods
        public void RecalculateWeight();
        #endregion
    }
}
