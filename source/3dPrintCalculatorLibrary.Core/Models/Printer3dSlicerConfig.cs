using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Address)}es")]
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

        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

        [ObservableProperty]
        string printerName = string.Empty;

        [ObservableProperty]
        double aMax_xy = 1000;

        [ObservableProperty]
        double aMax_z = 1000;

        [ObservableProperty]
        double aMax_e = 5000;

        [ObservableProperty]
        double aMax_eExtrude = 1250;

        [ObservableProperty]
        double aMax_eRetract = 1250;

        [ObservableProperty]
        double printDurationCorrection = 1;

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
