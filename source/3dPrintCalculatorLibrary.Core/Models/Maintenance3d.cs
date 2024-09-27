using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

#if SQL
using AndreasReitberger.Print3d.SQLite;

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
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: ForeignKey(typeof(Printer3d))]
        Guid printerId;

        [ObservableProperty]
        Printer3d? printer;
#else
        [ObservableProperty]
        IPrinter3d? printer;
#endif

        [ObservableProperty]
        string description = string.Empty;

        [ObservableProperty]
        string category = string.Empty;

        [ObservableProperty]
        DateTimeOffset date;

        [ObservableProperty]
        double duration;

        [ObservableProperty]
        double additionalCosts;

        [ObservableProperty]
#if SQL
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<Sparepart> spareparts = [];
#else
        IList<ISparepart> spareparts = [];
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
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Maintenance3d item)
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
