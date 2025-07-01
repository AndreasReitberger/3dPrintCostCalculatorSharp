using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models.MaterialAdditions
{
    public partial class Material3dUsage : ObservableObject, ICloneable, IMaterial3dUsage
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
        public partial Guid PrintInfoId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid MaterialId { get; set; }

        [ObservableProperty]
        public partial Material3d? Material { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Percentage))]
        public partial double PercentageValue { get; set; } = 1;

        public double Percentage => PercentageValue * 100;

        #endregion

        #region Constructor
        public Material3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Material3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
