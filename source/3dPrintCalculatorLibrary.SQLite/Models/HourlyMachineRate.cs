﻿using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;

namespace AndreasReitberger.Print3d.SQLite
{
    [Table("HourlyMachineRates")]
    public partial class HourlyMachineRate : ObservableObject, IHourlyMachineRate, ICloneable
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        Guid id;

        [ObservableProperty]
        Guid printerId;

        [ObservableProperty]
        string name = string.Empty;

        [ObservableProperty]
        bool perYear = true;

        [ObservableProperty]
        double machineHours = 0;

        [ObservableProperty]
        double replacementCosts = 0;

        [ObservableProperty]
        int usefulLifeYears = 4;

        [Ignore, JsonIgnore]
        public double CalcDepreciation
        {
            get
            {
                if (ReplacementCosts == 0 || UsefulLifeYears == 0)
                    return 0;
                else
                    return ReplacementCosts / UsefulLifeYears;
            }
        }

        [ObservableProperty]
        double interestRate = 3;

        [Ignore, JsonIgnore]
        public double CalcInterest
        {
            get
            {
                if (ReplacementCosts == 0 || InterestRate == 0)
                    return 0;
                else
                {
                    return (ReplacementCosts / 2) / 100 * InterestRate;
                }
            }
        }

        [ObservableProperty]
        double maintenanceCosts = 0;

        [ObservableProperty]
        double locationCosts = 0;

        [ObservableProperty]
        double energyCosts = 0;

        [ObservableProperty]
        double additionalCosts = 0;

        [ObservableProperty]
        double maintenanceCostsVariable = 0;

        [ObservableProperty]
        double energyCostsVariable = 0;

        [ObservableProperty]
        double additionalCostsVariable = 0;

        [ObservableProperty]
        double fixMachineHourRate = -1;

        [Ignore, JsonIgnore]
        public double CalcMachineHourRate => GetMachineHourRate();

        [Ignore, JsonIgnore]
        public double TotalCosts => GetTotalCosts();
        #endregion

        #region Constructor
        public HourlyMachineRate()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region PrivateMethods
        double GetMachineHourRate()
        {
            double res;
            try
            {
                // If a fix machine hour rate was set, return this value.
                if (FixMachineHourRate >= 0) return FixMachineHourRate;
                // If the machine hours are 0 or less, return 0 
                if (MachineHours <= 0)
                    return 0;
                res = (CalcDepreciation + CalcInterest + (MaintenanceCosts + LocationCosts + EnergyCosts + AdditionalCosts) * (PerYear ? 1.0 : 12.0)
                    + (MaintenanceCostsVariable + EnergyCostsVariable + AdditionalCostsVariable) * (PerYear ? 1.0 : 12.0)) / (MachineHours * (PerYear ? 1.0 : 12.0));
                return res;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        double GetTotalCosts()
        {
            double res;
            try
            {
                res = (ReplacementCosts + (CalcInterest +
                    ((MaintenanceCosts + LocationCosts + EnergyCosts + AdditionalCosts)
                    + (MaintenanceCostsVariable + EnergyCostsVariable + AdditionalCostsVariable)) * (PerYear ? 1.0 : 12.0))
                    * UsefulLifeYears);
                return res;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            //return string.Format("{0} {1}", CalcMachineHourRate, CurrencySymbol);
            return string.Format("{0:C2}", CalcMachineHourRate);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }
}
