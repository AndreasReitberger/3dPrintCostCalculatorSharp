﻿using SQLiteNetExtensions.Attributes;
using System;

namespace AndreasReitberger.Models.CalculationAdditions
{
    public class CustomAdditionCalculation
    {
        [ForeignKey(typeof(CustomAddition))]
        public Guid CustomAdditionId { get; set; }

        [ForeignKey(typeof(Calculation3d))]
        public Guid CalculationId { get; set; }
    }
}
