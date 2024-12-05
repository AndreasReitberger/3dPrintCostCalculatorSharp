﻿#if SQL
namespace AndreasReitberger.Print3d.SQLite.Interfaces
#else
namespace AndreasReitberger.Print3d.Core.Interfaces
#endif
{
    public interface IPrinter3dAttribute
    {
        #region Properties
        public Guid Id { get; set; }
#if SQL
        public Guid PrinterId { get; set; }
#endif
        public string Attribute { get; set; }
        public double Value { get; set; }
        #endregion
    }
}
