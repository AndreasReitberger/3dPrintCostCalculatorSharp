using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Maintenance3d)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Maintenance3d : ObservableObject, ICloneable, IMaintenance3d
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

        /*
        [ObservableProperty]
        [property: ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        Printer3d? printer;
        */
#else
        [ObservableProperty]
        public partial IPrinter3d? Printer { get; set; }
#endif

        [ObservableProperty]
        public partial string Description { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Category { get; set; } = string.Empty;

        [ObservableProperty]
        public partial DateTimeOffset Date { get; set; }

        [ObservableProperty]
        public partial double Duration { get; set; }

        [ObservableProperty]
        public partial double AdditionalCosts { get; set; }

#if SQL
        [ObservableProperty]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Sparepart> Spareparts { get; set; } = [];
#else
        [ObservableProperty]
        public partial IList<ISparepart> Spareparts { get; set; } = [];
#endif

#if SQL
        [Ignore]
#endif
        public double Costs => Spareparts?.Sum(sparepart => sparepart.Costs) ?? 0 + AdditionalCosts;
        #endregion

        #region Constructor
        public Maintenance3d()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Maintenance3d);
        public override bool Equals(object? obj)
        {
            if (obj is not Maintenance3d item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
