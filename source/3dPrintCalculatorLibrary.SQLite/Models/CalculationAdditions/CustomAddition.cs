using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table("CustomAdditions")]
    public partial class CustomAddition : ObservableObject, ICloneable, ICustomAddition
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        public Guid calculationId;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public double percentage;

        [ObservableProperty]
        public int order = 0;

        [ObservableProperty]
        public CustomAdditionCalculationType calculationType;
        #endregion

        #region Constructor
        public CustomAddition()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
