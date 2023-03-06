using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

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
        [property: ForeignKey(typeof(Calculation3d))]
        Guid calculationId;

        [ObservableProperty]
        [property: ForeignKey(typeof(Item3d))]
        Guid itemId;

        [ObservableProperty]
        [property: ManyToOne(nameof(ItemId))]
        Item3d item;

        [ObservableProperty]
        [property: ForeignKey(typeof(File3d))]
        Guid? fileId;

        [ObservableProperty]
        [property: ManyToOne(nameof(FileId))]
        File3d file;
        partial void OnFileChanged(File3d value)
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
        public override bool Equals(object obj)
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
