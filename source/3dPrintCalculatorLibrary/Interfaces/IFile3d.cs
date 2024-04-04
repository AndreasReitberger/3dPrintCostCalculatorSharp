using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IFile3d : ICloneable
    {

        #region Properties
        public Guid Id { get; set; }

        [Obsolete("The file is now linked to the `Print3dInfo` instead of the `Calculation3d`. This property will be removed later")]
        public Guid CalculationId { get; set; }
        public string Name { get; set; }
        public object File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Volume { get; set; }
        public Guid ModelWeightId { get; set; }
        //public IModelWeight Weight { get; set; }
        public double PrintTime { get; set; }
        public int Quantity { get; set; }
        public bool MultiplyPrintTimeWithQuantity { get; set; }
        public double PrintTimeQuantityFactor { get; set; }
        public byte[] Image { get; set; }
        #endregion

    }
}
