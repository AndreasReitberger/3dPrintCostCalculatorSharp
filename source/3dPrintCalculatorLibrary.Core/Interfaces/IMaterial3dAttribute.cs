namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaterial3dAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public string Attribute { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
