using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models.CalculationAdditions
{
    [Table("CalculationProcedureAttribute")]
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(Calculation3d))]
        public Guid calculationId;

        [ObservableProperty]
        public Material3dFamily family;

        [ObservableProperty]
        public ProcedureAttribute attribute;

        [ObservableProperty]
        
        public List<CalculationProcedureParameter> parameters = new();

        [ObservableProperty]
        public CalculationLevel level = CalculationLevel.Printer;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
