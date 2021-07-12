﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AndreasReitberger.Models
{
    public class Supplier : ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        public Guid Id
        { get; set; }
        public string Name
        { get; set; }
        public string DebitorNumber
        { get; set; }
        public bool isActive
        { get; set; }
        public string Website
        { get; set; }
        public List<Manufacturer> Manufacturers
        { get; set; }
        #endregion

        #region Constructor
        public Supplier() { }
        #endregion

        #region Override
        public override string ToString()
        {
            return string.IsNullOrEmpty(DebitorNumber) ? Name : string.Format("{0} ({1})", Name, DebitorNumber);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Supplier;
            if (item == null)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
