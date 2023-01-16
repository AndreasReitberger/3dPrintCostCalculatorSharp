using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IMaintenance3d
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid PrinterId { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public double AdditionalCosts { get; set; }
        public double Costs { get; }
        #endregion

        #region List
        //public List<ISparepart> Spareparts { get; set; }
        #endregion

    }
}
