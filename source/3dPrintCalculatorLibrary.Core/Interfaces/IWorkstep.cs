using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IWorkstep : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public IWorkstepCategory Category { get; set; }
        public CalculationType CalculationType { get; set; }
        public WorkstepType Type { get; set; }
        public string Note { get; set; }
        #endregion

    }
}
