using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
using AndreasReitberger.Print3d.SQLite.StorageAdditions;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Storage3dItem)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Storage3dItem : ObservableObject, IStorage3dItem
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

#if SQL
        [ObservableProperty]
        [ForeignKey(typeof(Material3d))]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(MaterialId), CascadeOperations = CascadeOperation.All)]
        public partial Material3d? Material { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        //[ManyToMany(typeof(Storage3dItemStorage3dTransaction), CascadeOperations = CascadeOperation.All)]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Storage3dTransaction> Transactions { get; set; } = [];

#else
        [ObservableProperty]
        public partial IMaterial3d? Material { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        public partial IList<IStorage3dTransaction> Transactions { get; set; } = [];
#endif
        public double Amount => GetAvailableAmount();
        #endregion

        #region Ctor
        public Storage3dItem()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public double GetAvailableAmount() => Transactions?.Select(x => x.Amount).Sum() ?? 0;

        #endregion

        #region Overrides
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Storage3dItem);
        #endregion
    }
}
