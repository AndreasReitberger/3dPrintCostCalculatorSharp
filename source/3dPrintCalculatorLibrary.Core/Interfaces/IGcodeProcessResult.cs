namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IGcodeProcessResult
    {
        #region Properties
        public Guid Id { get; set; }
        public bool ValidX { get; set; }
        public bool ValidY { get; set; }
        public bool ValidZ { get; set; }
        public double MaxX { get; set; }
        public double MinX { get; set; }
        public double MaxY { get; set; }
        public double MinY { get; set; }
        public double MaxZ { get; set; }
        public double MinZ { get; set; }
        public double PrintTimeAddition { get; set; }
        public double TotalFilament { get; set; }
        public double LastSpeed { get; set; }
        public long Order { get; set; }
        public IGcode Gcode { get; set; }
        #endregion
    }
}
