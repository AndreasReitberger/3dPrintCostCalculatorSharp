using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite.MaterialAdditions
{
    [Table("MaterialUsages")]
    public partial class Material3dUsage : ObservableObject, ICloneable, IMaterial3dUsage
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Print3dInfo))]
        Guid printInfoId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid materialId;

        [ObservableProperty]
        [property: ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        Material3d? material;

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

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Material3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
