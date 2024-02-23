using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ICalculationProcedureAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public Material3dFamily Family { get; set; }
        public ProcedureAttribute Attribute { get; set; }
        public IList<ICalculationProcedureParameter> Parameters { get; set; }
        public CalculationLevel Level { get; set; }
        public bool PerPiece { get; set; }
        public bool PerFile { get; set; }
        #endregion
    }
}
