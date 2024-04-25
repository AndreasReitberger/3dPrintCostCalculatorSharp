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
        Guid id;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        [property: Obsolete("Use Calculation3dEnhanced instead")]
        Guid calculationId;

        [ObservableProperty]
        Guid calculationEnhancedId;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid fileId;

        [ObservableProperty]
        File3dUsage? fileUsage;

        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid printerId;

        [ObservableProperty]
        Printer3d? printer;

        [ObservableProperty]
        List<Item3dUsage> items = [];

        [ObservableProperty]
        List<Material3dUsage> materialUsages = [];
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
