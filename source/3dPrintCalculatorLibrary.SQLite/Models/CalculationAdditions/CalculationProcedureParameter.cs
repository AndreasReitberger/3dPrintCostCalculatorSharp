using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table("CalculationProcedureParameter")]
    public partial class CalculationProcedureParameter : ObservableObject, ICalculationProcedureParameter
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(CalculationProcedureAttribute))]
        public Guid calculationProcedureAttributeId;

        [ObservableProperty]
        public ProcedureParameter type;

        [ObservableProperty]
        public double value = 0;

        [ObservableProperty]
        [property: OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<CalculationProcedureParameterAddition> additions = new();

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
