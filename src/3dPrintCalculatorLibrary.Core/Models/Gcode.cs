using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Gcode)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Gcode : ObservableObject, IGcode
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Slicer { get; set; } = "Unknown";

        [ObservableProperty]
        public partial double PrintTime { get; set; } = 0;

        [ObservableProperty]
        public partial double TotalFilament { get; set; } = 0;

        [ObservableProperty]
        public partial int SpeedIndex { get; set; } = 0;

        [ObservableProperty]
        public partial double Width { get; set; } = 0;

        [ObservableProperty]
        public partial double Height { get; set; } = 0;

        [ObservableProperty]
        public partial double Depth { get; set; } = 0;

        [ObservableProperty]
        public partial int Layers { get; set; } = 0;

        [ObservableProperty]
        public partial double LayerHeight { get; set; } = 0;

        [ObservableProperty]
        public partial List<double> VolSpeeds { get; set; } = [];

        [ObservableProperty]
        public partial List<double> ExtrusionSpeeds { get; set; } = [];

        [ObservableProperty]
        public partial Dictionary<double, List<double>> VolSpeedsByLayer { get; set; } = [];

        [ObservableProperty]
        public partial Dictionary<double, List<double>> ExtrusionSpeedsByLayer { get; set; } = [];

        [ObservableProperty]
        public partial Dictionary<string, double> SpeedsByLayer { get; set; } = [];

        [ObservableProperty]
        public partial Dictionary<double, int> ZHeights { get; set; } = [];

#if SQL

        [ObservableProperty]
        public partial List<List<GcodeCommand>> Commands { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<IList<IGcodeCommand>> Commands { get; set; } = [];
#endif

        #endregion

        #region Constructor
        public Gcode()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Gcode);

        public override bool Equals(object? obj)
        {
            if (obj is not Gcode item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
