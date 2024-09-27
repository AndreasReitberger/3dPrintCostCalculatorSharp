using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
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

        #region Methods
        public object Clone();
        #endregion
    }
}
