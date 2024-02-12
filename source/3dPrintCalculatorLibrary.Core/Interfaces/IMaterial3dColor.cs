namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaterial3dColor : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HexColorCode { get; set; }
        #endregion
    }
}
