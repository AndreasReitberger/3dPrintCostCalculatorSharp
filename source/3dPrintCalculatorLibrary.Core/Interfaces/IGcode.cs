#if SQL
using AndreasReitberger.Print3d.SQLite;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IGcode
    {
        #region Properties
        public Guid Id { get; set; }
        public string Slicer { get; set; }
        public double PrintTime { get; set; }
        public double TotalFilament { get; set; }
        public int SpeedIndex { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public int Layers { get; set; }
        public double LayerHeight { get; set; }
        public List<double> VolSpeeds { get; set; }
        public List<double> ExtrusionSpeeds { get; set; }
        public Dictionary<double, List<double>> VolSpeedsByLayer { get; set; }
        public Dictionary<double, List<double>> ExtrusionSpeedsByLayer { get; set; }
        public Dictionary<string, double> SpeedsByLayer { get; set; }
        public Dictionary<double, int> ZHeights { get; set; }
#if SQL
        public List<List<GcodeCommand>> Commands { get; set; }
#else
        public IList<IList<IGcodeCommand>> Commands { get; set; }
#endif
        #endregion
    }
}
