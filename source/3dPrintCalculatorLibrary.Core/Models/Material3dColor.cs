using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Material3dColor : ObservableObject, IMaterial3dColor
    {
        #region Properties 
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        string hexColorCode = string.Empty;

        #endregion

        #region Constructors
        public Material3dColor()
        {
            Id = Guid.NewGuid();
        }
        public Material3dColor(string name, string hexColorCode)
        {
            Id = Guid.NewGuid();
            Name = name;
            HexColorCode = hexColorCode;
        }
        public Material3dColor(Guid id, string name, string hexColorCode)
        {
            Id = id;
            Name = name;
            HexColorCode = hexColorCode;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"{Name} (#{HexColorCode})";
        }
        public override bool Equals(object obj)
        {
            if (obj is not Material3dColor item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public object Clone() => MemberwiseClone();
        #endregion
    }
}
