namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IWorkstepCategory
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
