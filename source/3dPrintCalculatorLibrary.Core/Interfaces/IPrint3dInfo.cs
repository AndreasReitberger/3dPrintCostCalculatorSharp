﻿using System;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IPrint3dInfo : ICloneable
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IFile3d File { get; set; }
        public IMaterial3d Material { get; set; }
        public IList<IItem3dUsage> Items { get; set; }
        public IList<IMaterial3dUsage> Materials { get; set; }
        #endregion
    }
}
