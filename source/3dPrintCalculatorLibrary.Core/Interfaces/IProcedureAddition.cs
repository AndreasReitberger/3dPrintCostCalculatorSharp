using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IProcedureAddition : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ToolTip { get; set; }

        public bool Enabled { get; set; }

        public Material3dFamily TargetFamily { get; set; }

        public ProcedureAdditionTarget Target { get; set; }
        #endregion

        #region Collections

        public IList<IProcedureCalculationParameter> Parameters { get; set; }

        #endregion

        #region Methods

        public double CalculateCosts();

        #endregion
    }
}
