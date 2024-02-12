namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IPrinter3dAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid PrinterId { get; set; }
        public string Attribute { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
