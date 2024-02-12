namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IGcodeCommand
    {
        #region Properties
        public Guid Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public bool Extrude { get; set; }
        public double Retract { get; set; }
        public bool NoMove { get; set; }
        public double Extrusion { get; set; }
        public string Extruder { get; set; }
        public double PreviousX { get; set; }
        public double PreviousY { get; set; }
        public double PreviousZ { get; set; }
        public double Speed { get; set; }
        public int GCodeLine { get; set; }
        public double VolumePerMM { get; set; }
        public string Command { get; set; }
        public string OriginalLine { get; set; }
        #endregion
    }
}
