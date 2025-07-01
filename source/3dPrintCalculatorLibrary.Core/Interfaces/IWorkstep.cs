using AndreasReitberger.Print3d.Core.Enums;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IWorkstep : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
#if SQL
        public Guid CalculationId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Guid CategoryId { get; set; }
        public WorkstepCategory? Category { get; set; }
#else
        public IWorkstepCategory? Category { get; set; }
#endif
        public CalculationType CalculationType { get; set; }
        public WorkstepType Type { get; set; }
        public string Note { get; set; }
        #endregion

    }
}
