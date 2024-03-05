using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    /// <summary>
    /// This is an additional item usage which can be added to the calculation job. 
    /// For instance, if you need to add screws or other material to the calculation.
    /// </summary>
    public partial class Item3dUsage : ObservableObject, ICloneable, IItem3dUsage
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationEnhancedId;

        [ObservableProperty]
        Guid calculationProfileId;

        [ObservableProperty]
        Guid printInfoId;

        [ObservableProperty]
        Guid itemId;

        [ObservableProperty]
        Item3d item;

        [ObservableProperty]
        double quantity;

        [ObservableProperty]
        Guid? fileId;

        [ObservableProperty]
        File3d file;
        partial void OnFileChanged(File3d value)
        {
            FileId = value?.Id ?? Guid.Empty;
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        bool linkedToFile = false;
        #endregion

        #region Constructor
        public Item3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not Item3dUsage item)
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
