namespace AndreasReitberger.Print3d.Models.GcodeAdditions
{
    public struct GcodeCommand
    {
        public double X;
        public double Y;
        public double Z;
        public bool Extrude;
        public double Retract;
        public bool NoMove;
        public double Extrusion;
        public string Extruder;
        public double PreviousX;
        public double PreviousY;
        public double PreviousZ;
        public double Speed;
        public int GCodeLine;
        public double VolumePerMM;
        public string Command;
        public string OriginalLine;
    }
}
