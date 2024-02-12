using System;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IItem3dUsage : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public IItem3d Item { get; set; }
        public IFile3d File { get; set; }
        public double Quantity { get; set; }
        public bool LinkedToFile { get; set; }
        #endregion 
    }
}
