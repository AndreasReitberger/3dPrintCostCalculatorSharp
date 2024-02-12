using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IStorage3dTransaction
    {
        #region Properties
        public Guid Id { get; set; }

        public Guid? CalculationId { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public double Amount { get; set; }

        public Unit Unit { get; set; }
        #endregion
    }
}
