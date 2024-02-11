using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IPrint3dInfo
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FileId { get; set; }
        //public IFile3d File { get; set; }
        //public Guid MaterialId { get; set; }
        //public IMaterial3d Material { get; set; }
        public Guid PrinterId { get; set; }
        //public IPrinter3d Printer { get; set; }
        #endregion
    }
}
