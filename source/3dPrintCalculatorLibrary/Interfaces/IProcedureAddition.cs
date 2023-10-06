﻿using AndreasReitberger.Print3d.Enums;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IProcedureAddition
    {
        #region Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ToolTip { get; set; }

        public bool Enabled { get; set; }

        public Material3dFamily TargetFamily { get; set; }
        #endregion

        #region Collections

        public List<IProcedureCalculationParameter> Parameters { get; set; }

        #endregion

        #region Methods

        public double CalculateCosts();

        #endregion
    }
}
