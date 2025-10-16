using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
#if !SQL
#endif

#if SQL

namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Calculation3dEnhanced)}s")]
#else

namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Calculation3dEnhanced : Calculation3d, ICalculation3dEnhanced, ICloneable
    {

        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public new partial Guid Id { get; set; }

        #region Basics

#if SQL      
        [ObservableProperty]
        [ForeignKey(typeof(Printer3d))]
        [Obsolete("Will be removed")]
        public new partial Guid PrinterId { get; set; }

        [ObservableProperty]
        [Ignore]
        [Obsolete("Will be removed")]
        public new partial Printer3d? Printer { get; set; }

        [ObservableProperty]
        [ForeignKey(typeof(Material3d))]
        [Obsolete("Will be removed")]
        public new partial Guid MaterialId { get; set; }

        [ObservableProperty]
        [Ignore]
        [Obsolete("Will be removed")]
        public new partial Material3d? Material { get; set; }
#else

        [ObservableProperty]
        [Obsolete("Will be removed")]
        public new partial IPrinter3d? Printer { get; set; }

        [ObservableProperty]
        [Obsolete("Will be removed")]
        public new partial IMaterial3d? Material { get; set; }
#endif
        #endregion

        #region Details

#if SQL
        public List<Printer3d?> AvailablePrinters => [.. PrintInfos.Select(pi => pi?.Printer).Distinct()];

        public List<Material3d?> AvailableMaterials => [.. PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu?.Material).Distinct()];

        [Obsolete("Will be removed")]
        [ObservableProperty]
        [Ignore]
        //[ManyToMany(typeof(Printer3dCalculation3d), CascadeOperations = CascadeOperation.All)]
        public new partial List<Printer3d> Printers { get; set; } = [];

        [Obsolete("Will be removed")]
        [ObservableProperty]
        [Ignore]
        //[ManyToMany(typeof(Material3dCalculation3d), CascadeOperations = CascadeOperation.All)]
        public new partial List<Material3d> Materials { get; set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public partial List<Print3dInfo> PrintInfos { get; set; } = [];

        [Obsolete("Will be removed")]
        [ObservableProperty]
        [Ignore]
        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        public new partial List<File3d> Files { get; set; } = [];
#else
        public IList<IPrinter3d?> AvailablePrinters => [.. PrintInfos.Select(pi => pi.Printer).Distinct()];

        public IList<IMaterial3d?> AvailableMaterials => [.. PrintInfos.SelectMany(pi => pi.Materials).Select(mu => mu.Material).Distinct()];

        [ObservableProperty]
        [Obsolete("Will be removed")]
        public new partial IList<IPrinter3d> Printers { get; set; } = [];

        [ObservableProperty]
        [Obsolete("Will be removed")]
        public new partial IList<IMaterial3d> Materials { get; set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(AvailablePrinters))]
        [NotifyPropertyChangedFor(nameof(AvailableMaterials))]
        public partial IList<IPrint3dInfo> PrintInfos { get; set; } = [];

        [ObservableProperty]
        [Obsolete("Will be removed")]
        public new partial IList<IFile3d> Files { get; set; } = [];
#endif
        #endregion

        #endregion

        #region Constructor
        public Calculation3dEnhanced() : base()
        {
            Id = Guid.NewGuid();
            PrintInfos = [];
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Calculation3dEnhanced item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        #endregion
    }
}
