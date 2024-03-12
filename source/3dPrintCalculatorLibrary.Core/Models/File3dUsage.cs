using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core
{
    public partial class File3dUsage : ObservableObject, ICloneable, IFile3dUsage
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        IFile3d? file;

        [ObservableProperty]
        int quantity = 1;

        [ObservableProperty]
        bool multiplyPrintTimeWithQuantity = true;

        [ObservableProperty]
        double printTimeQuantityFactor = 1;

        #endregion

        #region Constructor
        public File3dUsage()
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
            if (obj is not File3dUsage item)
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
