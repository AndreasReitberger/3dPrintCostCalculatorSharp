using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IMaterial3dAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public string Attribute { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
