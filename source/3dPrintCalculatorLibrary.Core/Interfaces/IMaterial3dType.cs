using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IMaterial3dType
    {
        #region Properties 
        public Guid Id { get; set; }
        public Material3dFamily Family { get; set; }
        public string Material { get; set; }
        public string Polymer { get; set; }
        #endregion
    }
}
