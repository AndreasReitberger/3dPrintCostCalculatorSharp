using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface ICustomAddition
    {

        #region Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Percentage { get; set; }
        public int Order { get; set; }
        public CustomAdditionCalculationType CalculationType { get; set; }
        #endregion

        #region Metods
        public object Clone();
        #endregion
    }
}
