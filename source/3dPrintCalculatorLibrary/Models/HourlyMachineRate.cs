using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class HourlyMachineRate : ObservableObject, IHourlyMachineRate
    {
        #region Properties
        [ObservableProperty]
        public Guid id;

        [ObservableProperty]
        [property: JsonIgnore]
        public Guid printerId;

        [ObservableProperty]
        [property: JsonIgnore]
        string name = string.Empty;

        [ObservableProperty]
        [property: JsonIgnore]
        bool perYear = true;

        [ObservableProperty]
        [property: JsonIgnore]
        double machineHours = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double replacementCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        int usefulLifeYears = 4;

        [JsonIgnore]
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
        [property: JsonIgnore]
        double interestRate = 3;

        [JsonIgnore]
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
        [property: JsonIgnore]
        double maintenanceCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double locationCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double energyCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double additionalCosts = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double maintenanceCostsVariable = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double energyCostsVariable = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double additionalCostsVariable = 0;

        [ObservableProperty]
        [property: JsonIgnore]
        double fixMachineHourRate = -1;

        [JsonIgnore]
        public double CalcMachineHourRate => GetMachineHourRate();
        
        [JsonIgnore]
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

        #region overrides
        public override string ToString()
        {
            //return string.Format("{0} {1}", CalcMachineHourRate, CurrencySymbol);
            return string.Format("{0:C2}", CalcMachineHourRate);
        }
        #endregion
    }
}
