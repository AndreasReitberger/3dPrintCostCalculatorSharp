using AndreasReitberger.Print3d.Interfaces;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Models.FileAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Print3dInfo : ObservableObject, IPrint3dInfo, ICloneable
    {

        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        [Obsolete("Use Calculation3dEnhanced instead")]
        public partial Guid CalculationId { get; set; }

        [ObservableProperty]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        public partial File3dUsage? FileUsage { get; set; }

        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        public partial Printer3d? Printer { get; set; }

        [ObservableProperty]
        public partial List<Item3dUsage> Items { get; set; } = [];

        [ObservableProperty]
        public partial List<Material3dUsage> MaterialUsages { get; set; } = [];
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
