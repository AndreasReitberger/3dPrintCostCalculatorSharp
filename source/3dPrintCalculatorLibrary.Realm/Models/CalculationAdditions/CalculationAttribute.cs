using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class CalculationAttribute : RealmObject, ICalculationAttribute
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid CalculationId { get; set; }

        public Guid FileId { get; set; } = Guid.Empty;

        public string FileName { get; set; }

        public Guid LinkedId { get; set; } = Guid.Empty;

        public string Attribute { get; set; }

        public CalculationAttributeType Type
        {
            get => (CalculationAttributeType)TypeId;
            set { TypeId = (int)value; }
        }
        public int TypeId { get; set; }

        public double Value { get; set; }

        public bool IsPercentageValue { get; set; } = false;

        public bool SkipForCalculation { get; set; } = false;
        public CalculationAttributeItem Item
        {
            get => (CalculationAttributeItem)ItemId;
            set { ItemId = (int)value; }
        }
        public int ItemId { get; set; }
        public bool ApplyPerFile { get; set; }
        public bool SkipForMargin { get; set; }
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
