using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Linq;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Address)}es")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Printer3dAttribute : ObservableObject, IPrinter3dAttribute
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Printer3d))]
        Guid printerId;
#endif

        [ObservableProperty]
        string attribute = string.Empty;

        [ObservableProperty]
        double value;
#endregion

        #region Constructor
        public Printer3dAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
