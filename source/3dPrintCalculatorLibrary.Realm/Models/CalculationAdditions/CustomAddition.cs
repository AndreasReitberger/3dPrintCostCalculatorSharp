using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm.CalculationAdditions
{
    public partial class CustomAddition : RealmObject, ICloneable, ICustomAddition
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

        public Guid CalculationId { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Percentage { get; set; }

        public int Order { get; set; } = 0;

        public CustomAdditionCalculationType CalculationType
        {
            get => (CustomAdditionCalculationType)CalculationTypeId;
            set { CalculationTypeId = (int)value; }
        }
        public int CalculationTypeId { get; set; }
        #endregion

        #region Constructor
        public CustomAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
