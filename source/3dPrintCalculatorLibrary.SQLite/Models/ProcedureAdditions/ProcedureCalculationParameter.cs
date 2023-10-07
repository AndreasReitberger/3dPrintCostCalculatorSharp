using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace AndreasReitberger.Print3d.SQLite.ProcedureAdditions
{
    [Table($"{nameof(ProcedureCalculationParameter)}s")]
    public partial class ProcedureCalculationParameter : ObservableObject, IProcedureCalculationParameter
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        [property: ForeignKey(typeof(ProcedureAddition))]
        Guid procedureAdditionId;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        ProcedureCalculationType type;

        [ObservableProperty]
        double quantityInPackage = 1;

        [ObservableProperty]
        double amountTakenForCalculation = 1;

        [ObservableProperty]
        double price = 0;
        partial void OnPriceChanged(double value)
        {
            CalculatedCosts = value / WearFactor;
        }

        [ObservableProperty]
        double wearFactor = 0;
        partial void OnWearFactorChanged(double value)
        {
            CalculatedCosts = Price / value;
        }

        [ObservableProperty]
        double calculatedCosts;
        #endregion

        #region Ctor
        public ProcedureCalculationParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        #endregion
    }
}
