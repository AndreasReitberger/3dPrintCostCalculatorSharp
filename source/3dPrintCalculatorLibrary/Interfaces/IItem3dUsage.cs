using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IItem3dUsage
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid CalculationId { get; set; }
        public Guid CalculationProfileId { get; set; }
        public Guid PrintInfoId { get; set; }
        public Guid? FileId { get; set; }
        public Guid ItemId { get; set; }
        public double Quantity { get; set; }
        public bool LinkedToFile { get; set; }
        #endregion 
    }
}
