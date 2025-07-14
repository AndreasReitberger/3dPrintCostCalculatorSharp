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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Printer3d))]
        public partial Guid PrinterId { get; set; }
#endif

        [ObservableProperty]
        public partial string Attribute { get; set; } = string.Empty;

        [ObservableProperty]
        public partial double Value { get; set; }
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
