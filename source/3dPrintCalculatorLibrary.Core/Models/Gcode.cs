using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string slicer = "Unknown";

        [ObservableProperty]
        double printTime = 0;

        [ObservableProperty]
        double totalFilament = 0;

        [ObservableProperty]
        int speedIndex = 0;

        [ObservableProperty]
        double width = 0;

        [ObservableProperty]
        double height = 0;

        [ObservableProperty]
        double depth = 0;

        [ObservableProperty]
        int layers = 0;

        [ObservableProperty]
        double layerHeight = 0;

        [ObservableProperty]
        List<double> volSpeeds = [];

        [ObservableProperty]
        List<double> extrusionSpeeds = [];

        [ObservableProperty]
        Dictionary<double, List<double>> volSpeedsByLayer = [];

        [ObservableProperty]
        Dictionary<double, List<double>> extrusionSpeedsByLayer = [];

        [ObservableProperty]
        Dictionary<string, double> speedsByLayer = [];

        [ObservableProperty]
        Dictionary<double, int> zHeights = [];

#if SQL

        [ObservableProperty]
        List<List<GcodeCommand>> commands = [];
#else
        [ObservableProperty]
        IList<IList<IGcodeCommand>> commands = [];
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
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
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
