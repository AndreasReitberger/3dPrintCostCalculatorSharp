namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IFile3dUsage
    {
        #region Properties
        public Guid Id { get; set; }
        public IFile3d File { get; set; }
        public int Quantity { get; set; }
        public bool MultiplyPrintTimeWithQuantity { get; set; }
        public double PrintTimeQuantityFactor { get; set; }
        #endregion 
    }
}
