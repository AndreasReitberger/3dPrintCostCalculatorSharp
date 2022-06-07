﻿using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.PrinterAdditions
{
    public class Printer3dCalculation
    {
        [ForeignKey(typeof(Printer3d))]
        public Guid PrinterId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}