using AndreasReitberger.Print3d.Models.GcodeAdditions;

namespace AndreasReitberger.Print3d.SQLite.GcodeAdditions
{
    public class GcodeModel
    {
        public string Slicer = "Unknown";
        public double PrintTime = 0;
        public double TotalFilament = 0;
        public int SpeedIndex = 0;
        public List<double> volSpeeds = [];
        public List<double> extrusionSpeeds = [];
        public double Width = 0;
        public double Height = 0;
        public double Depth = 0;
        public int Layers = 0;
        public double LayerHeight = 0;

        public List<List<GcodeCommand>> Commands = [];

        public Dictionary<double, List<double>> volSpeedsByLayer = [];
        public Dictionary<double, List<double>> extrusionSpeedsByLayer = [];

        public Dictionary<string, double> speedsByLayer = [];
        public Dictionary<double, int> zHeights = [];
    }
}
