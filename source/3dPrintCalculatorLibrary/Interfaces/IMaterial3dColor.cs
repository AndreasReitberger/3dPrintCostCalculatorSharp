using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IMaterial3dColor
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HexColorCode { get; set; }
        #endregion
    }
}
