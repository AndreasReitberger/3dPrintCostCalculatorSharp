using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models.GcodeAdditions
{
    public class GcodeModel
    {
        public string Slicer = "Unknown";
        public double PrintTime = 0;
        public double TotalFilament = 0;
        public int SpeedIndex = 0;
        public List<double> volSpeeds = new List<double>();
        public List<double> extrusionSpeeds = new List<double>();
        public double Width = 0;
        public double Height = 0;
        public double Depth = 0;
        public int Layers = 0;
        public double LayerHeight = 0;

        public List<List<GcodeCommand>> Commands = new List<List<GcodeCommand>>();

        public Dictionary<double, List<double>> volSpeedsByLayer = new Dictionary<double, List<double>>();
        public Dictionary<double, List<double>> extrusionSpeedsByLayer = new Dictionary<double, List<double>>();

        public Dictionary<string, double> speedsByLayer = new Dictionary<string, double>();
        public Dictionary<double, int> zHeights = new Dictionary<double, int>();
    }
}
