﻿using AndreasReitberger.Print3d.Core.Enums;

namespace AndreasReitberger.Print3d.Core.Interfaces
{
    public interface IStorage3dTransaction
    {
        #region Properties
        public Guid Id { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public IStorage3dItem? Item { get; set; }
        public double Amount { get; set; }
        public Unit Unit { get; set; }
        public bool IsAddition { get; }
        #endregion
    }
}
