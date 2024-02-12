using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class CalculationProcedureParameter : ObservableObject, ICalculationProcedureParameter
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationProcedureAttributeId;

        [ObservableProperty]
        ProcedureParameter type;

        [ObservableProperty]
        double value = 0;

        [ObservableProperty]
        IList<ICalculationProcedureParameterAddition> additions = [];

        #endregion

        #region Constructor
        public CalculationProcedureParameter()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
