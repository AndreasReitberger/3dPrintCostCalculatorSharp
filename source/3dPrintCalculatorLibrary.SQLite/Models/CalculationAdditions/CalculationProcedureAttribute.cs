﻿using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table("CalculationProcedureAttribute")]
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3dEnhanced))]
        Guid calculationEnhancedId;

        [ObservableProperty]
        Material3dFamily family;

        [ObservableProperty]
        ProcedureAttribute attribute;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        List<CalculationProcedureParameter> parameters = [];

        [ObservableProperty]
        CalculationLevel level = CalculationLevel.Printer;

        [ObservableProperty]
        bool perFile = false;

        [ObservableProperty]
        bool perPiece = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
