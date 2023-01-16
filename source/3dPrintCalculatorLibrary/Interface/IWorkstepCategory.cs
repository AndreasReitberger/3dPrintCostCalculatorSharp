using System;

namespace AndreasReitberger.Print3d.Interface
{
    public interface IWorkstepCategory
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        #endregion
    }
}
