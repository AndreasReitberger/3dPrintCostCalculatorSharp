using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;

namespace AndreasReitberger.Print3d.Models
{
    public partial class HourlyMachineRate : ObservableObject, IHourlyMachineRate, ICloneable
    {
        #region Properties
        [ObservableProperty]
        public partial Guid Id { get; set; }

        [ObservableProperty]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        public partial string Name { get; set; } = string.Empty;

        [ObservableProperty]
        public partial bool PerYear { get; set; } = true;

        [ObservableProperty]
        public partial double MachineHours { get; set; } = 0;

        [ObservableProperty]
        public partial double ReplacementCosts { get; set; } = 0;

        [ObservableProperty]
        public partial int UsefulLifeYears { get; set; } = 4;

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
        public partial double InterestRate { get; set; } = 3;

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
        public partial double MaintenanceCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double LocationCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double AdditionalCosts { get; set; } = 0;

        [ObservableProperty]
        public partial double MaintenanceCostsVariable { get; set; } = 0;

        [ObservableProperty]
        public partial double EnergyCostsVariable { get; set; } = 0;

        [ObservableProperty]
        public partial double AdditionalCostsVariable { get; set; } = 0;

        [ObservableProperty]
        public partial double FixMachineHourRate { get; set; } = -1;

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

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        public object Clone() => MemberwiseClone();

        #endregion
    }
}
