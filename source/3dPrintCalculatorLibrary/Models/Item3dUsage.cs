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
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid CalculationEnhancedId { get; set; }

        [ObservableProperty]
        public partial Guid CalculationProfileId { get; set; }

        [ObservableProperty]
        public partial Guid PrintInfoId { get; set; }

        [ObservableProperty]
        public partial Guid ItemId { get; set; }

        [ObservableProperty]
        public partial Item3d? Item { get; set; }

        [ObservableProperty]
        public partial double Quantity { get; set; }

        [ObservableProperty]
        public partial Guid? FileId { get; set; }

        [ObservableProperty]
        public partial File3d? File { get; set; }

        partial void OnFileChanged(File3d? value)
        {
            FileId = value?.Id ?? Guid.Empty;
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        public partial bool LinkedToFile { get; set; } = false;
        #endregion

        #region Constructor
        public Item3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object? obj)
        {
            if (obj is not Item3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();

        #endregion
    }
}
