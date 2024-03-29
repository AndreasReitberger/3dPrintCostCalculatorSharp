﻿using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IItem3d
    {
        #region Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public double PackageSize { get; set; }
        public double PackagePrice { get; set; }
        public double Tax { get; set; }
        public bool PriceIncludesTax { get; set; }
        public string Uri { get; set; }
        public string Note { get; set; }
        public string SafetyDatasheet { get; set; }
        public string TechnicalDatasheet { get; set; }
        public double PricePerPiece { get; }
        #endregion 
    }
}
