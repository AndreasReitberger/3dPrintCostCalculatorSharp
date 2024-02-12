namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IGcode
    {
        #region Properties
        public Guid Id { get; set; }
        public string Slicer { get; set; }
        public double PrintTime  { get; set; }
        public double TotalFilament  { get; set; }
        public int SpeedIndex  { get; set; }
        public IList<double> VolSpeeds { get; set; }
        public IList<double> ExtrusionSpeeds { get; set; }
        public double Width  { get; set; }
        public double Height  { get; set; }
        public double Depth  { get; set; }
        public int Layers  { get; set; }
        public double LayerHeight  { get; set; }
        public IList<IList<IGcodeCommand>> Commands { get;set; }
        public IDictionary<double, IList<double>> VolSpeedsByLayer { get; set; }
        public IDictionary<double, IList<double>> ExtrusionSpeedsByLayer { get; set; }
        public IDictionary<string, double> SpeedsByLayer { get; set; }
        public IDictionary<double, int> ZHeights { get; set; }
        #endregion
    }
}
