using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.SQLite
{
    /// <summary>
    /// This is an additional item usage which can be added to the calculation job. 
    /// For instance, if you need to add screws or other material to the calculation.
    /// </summary>
    public partial class Item3dUsage : ObservableObject, ICloneable, IItem3dUsage
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
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationEnhancedId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dProfile))]
        Guid calculationProfileId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Print3dInfo))]
        Guid printInfoId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid itemId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ItemId), CascadeOperations = CascadeOperation.All)]
        Item3d? item;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid? fileId;

        [ObservableProperty]
        [property: ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        File3d? file;
        partial void OnFileChanged(File3d? value)
        {
            FileId = value?.Id ?? Guid.Empty;
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        double quantity;

        [ObservableProperty]
        bool linkedToFile = false;

        #endregion

        #region Constructor
        public Item3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Item3dUsage item)
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
