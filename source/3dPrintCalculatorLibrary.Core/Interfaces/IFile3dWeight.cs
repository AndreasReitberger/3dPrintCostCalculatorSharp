using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IFile3dWeight
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
