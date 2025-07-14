using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
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
        public CalculationAttributeTarget Target { get; set; }
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
