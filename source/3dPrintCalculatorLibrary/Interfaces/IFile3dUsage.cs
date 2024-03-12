using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IFile3dUsage : ICloneable
    {

        #region Properties
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public bool MultiplyPrintTimeWithQuantity { get; set; }
        public double PrintTimeQuantityFactor { get; set; }
        #endregion

    }
}
