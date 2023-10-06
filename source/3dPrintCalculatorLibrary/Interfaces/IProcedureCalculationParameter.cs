using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IProcedureCalculationParameter
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ProcedureCalculationType Type { get; set; }

        public double Price { get; set; }

        public double WearFactor { get; set; }

        public double CalculatedCosts { get; set; }

        public double QuantityInPackage { get; set; }

        public double AmountTakenForCalculation { get; set; }
        #endregion
    }
}
