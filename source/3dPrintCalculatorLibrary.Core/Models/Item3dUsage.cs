using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Xml.Serialization;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Item3dUsage)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    /// <summary>
    /// This is an additional item usage which can be added to the calculation job. 
    /// For instance, if you need to add screws or other material to the calculation.
    /// </summary>
    public partial class Item3dUsage : ObservableObject, IItem3dUsage
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
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
#else
        [ObservableProperty]
        IItem3d? item;
#endif
        [ObservableProperty]
        double quantity;

#if SQL
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid fileId;
#endif

        [ObservableProperty]
#if SQL
        [property: ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        File3d? file;
        partial void OnFileChanged(File3d? value)
#else
        IFile3d? file;
        partial void OnFileChanged(IFile3d? value)
#endif
        {
#if SQL
            FileId = value?.Id ?? Guid.Empty;
#endif
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        bool linkedToFile = false;
        #endregion

        #region Constructor
        public Item3dUsage()
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
            if (obj is not Item3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
