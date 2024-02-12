using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
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
