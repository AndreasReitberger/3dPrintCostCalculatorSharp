using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface ICustomAddition
    {

        #region Properties

        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public int Order { get; set; }
        public CustomAdditionCalculationType CalculationType { get; set; }
        #endregion
    }
}
