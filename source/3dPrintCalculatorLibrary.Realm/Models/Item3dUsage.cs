using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    /// <summary>
    /// This is an additional item usage which can be added to the calculation job. 
    /// For instance, if you need to add screws or other material to the calculation.
    /// </summary>
    public partial class Item3dUsage : RealmObject, ICloneable, IItem3dUsage
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationEnhancedId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Guid PrintInfoId { get; set; }

        public Guid ItemId { get; set; }

        public Item3d? Item { get; set; }

        public double Quantity { get; set; }

        public Guid? FileId { get; set; }

        File3d? file { get; set; }
        public File3d? File
        {
            get => file;
            set
            {
                file = value;
                OnFileChanged(value);
            }
        }
        void OnFileChanged(File3d? value)
        {
            FileId = value?.Id ?? Guid.Empty;
            LinkedToFile = value is not null;
        }

        public bool LinkedToFile { get; set; } = false;

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
