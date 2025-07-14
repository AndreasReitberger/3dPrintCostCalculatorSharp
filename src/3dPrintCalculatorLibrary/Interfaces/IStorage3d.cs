using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IStorage3d
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        #endregion
    }
}
