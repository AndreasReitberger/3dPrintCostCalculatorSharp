using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class CalculationProcedureAttribute : ObservableObject, ICalculationProcedureAttribute
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid calculationId;

        [ObservableProperty]
        Material3dFamily family;

        [ObservableProperty]
        ProcedureAttribute attribute;

        [ObservableProperty]
        IList<ICalculationProcedureParameter> parameters = [];

        [ObservableProperty]
        CalculationLevel level = CalculationLevel.Printer;

        /// <summary>
        /// Multiplies the costs per piece (quantity of the files)
        /// </summary>
        [ObservableProperty]
        bool perPiece = false;

        [ObservableProperty]
        bool perFile = false;
        #endregion

        #region Constructor
        public CalculationProcedureAttribute()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
