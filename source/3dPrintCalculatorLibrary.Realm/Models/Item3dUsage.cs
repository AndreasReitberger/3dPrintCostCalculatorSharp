using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Realm
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
        public Guid id;

        [ObservableProperty]
        public Guid calculationId;

        [ObservableProperty]
        public Guid itemId;

        [ObservableProperty]
        public Item3d item;

        [ObservableProperty]
        public double quantity;

        [ObservableProperty]
        public Guid? fileId;

        [ObservableProperty]
        public File3d file;
        partial void OnFileChanged(File3d value)
        {
            FileId = value?.Id ?? Guid.Empty;
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        public bool linkedToFile = false;

        #endregion

        #region Constructor
        public Item3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
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
