﻿using System;

namespace AndreasReitberger.Print3d.Interfaces
{
    public interface IHourlyMachineRate
    {
        #region Properties
        public Guid Id { get; set; }
        public Guid PrinterId { get; set; }
        public string Name { get; set; }
        public bool PerYear { get; set; }
        public double MachineHours { get; set; }
        public double ReplacementCosts { get; set; }
        public int UsefulLifeYears { get; set; }
        public double CalcDepreciation { get; }
        public double InterestRate { get; set; }
        public double CalcInterest { get; }
        public double MaintenanceCosts { get; set; }
        public double LocationCosts { get; set; }
        public double EnergyCosts { get; set; }
        public double AdditionalCosts { get; set; }
        public double MaintenanceCostsVariable { get; set; }
        public double EnergyCostsVariable { get; set; }
        public double AdditionalCostsVariable { get; set; }
        public double FixMachineHourRate { get; set; }
        public double CalcMachineHourRate { get; }
        public double TotalCosts { get; }
        #endregion

    }
}
