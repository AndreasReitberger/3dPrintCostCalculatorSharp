using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(GcodeCommand)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class GcodeCommand : ObservableObject, IGcodeCommand
    {
        [ObservableProperty]
#if SQL
        [PrimaryKey]
#endif
        public partial Guid Id {  get; set; }

        [ObservableProperty]
        public partial double X {  get; set; }

        [ObservableProperty]
        public partial double Y {  get; set; }

        [ObservableProperty]
        public partial double Z {  get; set; }

        [ObservableProperty]
        public partial bool Extrude {  get; set; }

        [ObservableProperty]
        public partial double Retract {  get; set; }

        [ObservableProperty]
        public partial bool NoMove {  get; set; }

        [ObservableProperty]
        public partial double Extrusion {  get; set; }

        [ObservableProperty]
        public partial string Extruder {  get; set; }

        [ObservableProperty]
        public partial double PreviousX {  get; set; }

        [ObservableProperty]
        public partial double PreviousY {  get; set; }

        [ObservableProperty]
        public partial double PreviousZ {  get; set; }

        [ObservableProperty]
        public partial double Speed {  get; set; }

        [ObservableProperty]
        public partial int GCodeLine {  get; set; }

        [ObservableProperty]
        public partial double VolumePerMM {  get; set; }

        [ObservableProperty]
        public partial string Command {  get; set; }

        [ObservableProperty]
        public partial string OriginalLine {  get; set; }
    }
}
