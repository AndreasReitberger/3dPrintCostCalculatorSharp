using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{

    [Table($"{nameof(Print3dInfo)}s")]
    public partial class Print3dInfo : ObservableObject, IPrint3dInfo, ICloneable
    {

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        string? name;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationEnhancedId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid fileId;

        [ObservableProperty]
        [property: ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        File3d file;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid printerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        Printer3d printer;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Item3dUsage> items = [];

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Material3dUsage> materialUsages = [];
        #endregion

        #region Constructor

        public Print3dInfo()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not Print3dInfo item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        public object Clone() => MemberwiseClone();

        #endregion

    }
}
