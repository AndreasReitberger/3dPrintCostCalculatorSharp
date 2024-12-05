#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IWorkstepCategory
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
