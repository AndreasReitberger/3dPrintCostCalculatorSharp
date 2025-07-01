using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Printer3dSlicerConfig)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Printer3dSlicerConfig : ObservableObject, IPrinter3dSlicerConfig
    {
        #region Properties
        public static IPrinter3dSlicerConfig Default => new Printer3dSlicerConfig()
        {

        };

#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string PrinterName { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double AMax_xy { get; set; } = 1000;

        [ObservableProperty]
        public partial double AMax_z { get; set; } = 1000;

        [ObservableProperty]
        public partial double AMax_e { get; set; } = 5000;

        [ObservableProperty]
        public partial double AMax_eExtrude { get; set; } = 1250;

        [ObservableProperty]
        public partial double AMax_eRetract { get; set; } = 1250;

        [ObservableProperty]
        public partial double PrintDurationCorrection { get; set; } = 1;

        #endregion

        #region Constructor
        public Printer3dSlicerConfig()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
