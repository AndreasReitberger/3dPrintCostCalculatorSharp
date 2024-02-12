namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IFile3d : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public object File { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Volume { get; set; }
        public IModelWeight Weight { get; set; }
        public double PrintTime { get; set; }
        public int Quantity { get; set; }
        public bool MultiplyPrintTimeWithQuantity { get; set; }
        public double PrintTimeQuantityFactor { get; set; }
        public byte[] Image { get; set; }
        #endregion

    }
}
