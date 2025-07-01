using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public partial class Material3dColor : ObservableObject, IMaterial3dColor
    {
        #region Properties 
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string HexColorCode { get; set; } = string.Empty;

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
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Material3dColor item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
