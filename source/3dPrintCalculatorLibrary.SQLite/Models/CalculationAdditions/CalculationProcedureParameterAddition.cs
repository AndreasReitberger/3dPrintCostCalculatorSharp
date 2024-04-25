using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.CalculationAdditions
{
    [Table("CalculationProcedureParameterAddition")]
    public partial class CalculationProcedureParameterAddition : ObservableObject, ICalculationProcedureParameterAddition
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(CalculationProcedureParameter))]
        Guid calculationProcedureParameterId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        double value = 0;

        #endregion

        #region Constructor
        public CalculationProcedureParameterAddition()
        {
            Id = Guid.NewGuid();
        }
        public CalculationProcedureParameterAddition(string name, double value)
        {
            Id = Guid.NewGuid();
            Name = name;
            Value = value;
        }
        #endregion
    }
}
