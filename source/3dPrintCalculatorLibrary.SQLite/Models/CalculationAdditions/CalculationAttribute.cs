using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table("CalculationAttributes")]
    public partial class CalculationAttribute : ObservableObject, ICalculationAttribute
    {
        #region Properties

        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        Guid calculationId;

        [ObservableProperty]
        Guid fileId = Guid.Empty;

        [ObservableProperty]
        string fileName;

        [ObservableProperty]
        Guid linkedId = Guid.Empty;

        [ObservableProperty]
        string attribute;

        [ObservableProperty]
        CalculationAttributeType type;

        [ObservableProperty]
        CalculationAttributeItem item = CalculationAttributeItem.Default;

        [ObservableProperty]
        double value;

        [ObservableProperty]
        bool isPercentageValue = false;

        [ObservableProperty]
        bool skipForCalculation = false;
        #endregion

        #region Constructor
        public CalculationAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
