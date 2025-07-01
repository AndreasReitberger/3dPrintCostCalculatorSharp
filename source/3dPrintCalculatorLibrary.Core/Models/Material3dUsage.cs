using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Material3dUsage)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Material3dUsage : ObservableObject, ICloneable, IMaterial3dUsage
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Print3dInfo))]
        public partial Guid PrintInfoId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        public partial Material3d? Material { get; set; }
#else

        [ObservableProperty]
        public partial IMaterial3d? Material { get; set; }
#endif

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Percentage))]
        public partial double PercentageValue { get; set; } = 1;

        public double Percentage => PercentageValue * 100;

        #endregion

        #region Constructor
        public Material3dUsage()
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
            if (obj is not Material3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
