using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Manufacturer : ObservableObject, ICloneable, IManufacturer
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid SupplierId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string DebitorNumber { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool IsActive { get; set; } = true;

        [ObservableProperty]
        public partial string Website { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string Note { get; set; } = string.Empty;

        [ObservableProperty]
        public partial string CountryCode { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public Manufacturer()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public override bool Equals(object? obj)
        {
            if (obj is not Manufacturer item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
