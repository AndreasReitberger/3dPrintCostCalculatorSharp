using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaterial3dProcedureAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public Material3dFamily Family { get; set; }
        public ProcedureAttribute Attribute { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
