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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Print3dInfo))]
        Guid printInfoId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid materialId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        Material3d? material;
#else

        [ObservableProperty]
        IMaterial3d? material;
#endif

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Percentage))]
        double percentageValue = 1;

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
