using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace AndreasReitberger.Print3d.Models.FileAdditions
{
    public partial class File3dUsage : ObservableObject, ICloneable, IFile3dUsage
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
        public partial Guid FileId { get; set; }

        [ObservableProperty]
        public partial File3d? File { get; set; }

        [ObservableProperty]
        public partial int Quantity { get; set; } = 1;

        [ObservableProperty]
        public partial bool MultiplyPrintTimeWithQuantity { get; set; } = true;

        [ObservableProperty]
        public partial double PrintTimeQuantityFactor { get; set; } = 1;

        #endregion

        #region Constructor
        public File3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not File3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
