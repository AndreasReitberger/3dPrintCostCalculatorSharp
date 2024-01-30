using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IMaterial3dUsage
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid PrintInfoId { get; set; }
        public Guid MaterialId { get; set; }
        public double Percentage { get; set; }
        #endregion 
    }
}
