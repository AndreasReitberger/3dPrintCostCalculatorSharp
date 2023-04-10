using AndreasReitberger.Print3d.Enums;
using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface ICalculationAttribute
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public Guid LinkedId { get; set; }
        public string Attribute { get; set; }
        public CalculationAttributeType Type { get; set; }
        public CalculationAttributeItem Item { get; set; }
        public double Value { get; set; }
        public bool IsPercentageValue { get; set; }
        public bool ApplyPerFile { get; set; }
        public bool SkipForCalculation { get; set; }
        public bool SkipForMargin { get; set; }
        #endregion
    }
}
