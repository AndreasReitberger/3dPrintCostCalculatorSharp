using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface ISparepart
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid MaintenanceId { get; set; }
        public string Name { get; set; }
        public string Partnumber { get; set; }
        public double Costs { get; set; }
        #endregion
    }
}
