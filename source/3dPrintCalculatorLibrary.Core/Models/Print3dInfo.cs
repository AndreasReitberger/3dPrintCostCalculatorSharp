using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Print3dInfo : ObservableObject, IPrint3dInfo, ICloneable
    {

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string? name = string.Empty;

        [ObservableProperty]
        IFile3dUsage? fileUsage;

        [ObservableProperty]
        IPrinter3d? printer;

        [ObservableProperty]
        IList<IItem3dUsage> items = [];

        [ObservableProperty]
        IList<IMaterial3dUsage> materials = [];
        #endregion

        #region Constructor

        public Print3dInfo()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
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
