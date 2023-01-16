using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    [Table("CalculationAttributes")]
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        public Guid calculationId;

        [ObservableProperty]
        public Guid fileId = Guid.Empty;

        [ObservableProperty]
        public string fileName;

        [ObservableProperty]
        public Guid linkedId = Guid.Empty;

        [ObservableProperty]
        public string attribute;

        [ObservableProperty]
        public CalculationAttributeType type;

        [ObservableProperty]
        public double value;

        [ObservableProperty]
        public bool isPercentageValue = false;

        [ObservableProperty]
        public bool skipForCalculation = false;
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
