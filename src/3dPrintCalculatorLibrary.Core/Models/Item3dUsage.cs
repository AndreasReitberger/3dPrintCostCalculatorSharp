using CommunityToolkit.Mvvm.ComponentModel;
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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dEnhanced))]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Calculation3dProfile))]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Print3dInfo))]
        public partial Guid PrintInfoId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(Item3d))]
        public partial Guid ItemId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(ItemId), CascadeOperations = CascadeOperation.All)]
        public partial Item3d? Item { get; set; }
#else
        [ObservableProperty]
        public partial IItem3d? Item { get; set; }
#endif
        [ObservableProperty]
        public partial double Quantity { get; set; }

#if SQL
        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(File3d))]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(FileId), CascadeOperations = CascadeOperation.All)]
        public partial File3d? File { get; set; }
        partial void OnFileChanged(File3d? value)
#else
        [ObservableProperty]
        public partial IFile3d? File { get; set; }
        partial void OnFileChanged(IFile3d? value)
#endif
        {
#if SQL
            FileId = value?.Id ?? Guid.Empty;
#endif
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        public partial bool LinkedToFile { get; set; } = false;
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
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Item3dUsage);

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
