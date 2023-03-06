using AndreasReitberger.Print3d.Interfaces;
using Newtonsoft.Json;
using Realms;
using System;

namespace AndreasReitberger.Print3d.Realm
{
    public partial class HourlyMachineRate : RealmObject, IHourlyMachineRate, ICloneable
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid PrinterId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public bool PerYear { get; set; } = true;

        public double MachineHours { get; set; } = 0;

        public double ReplacementCosts { get; set; } = 0;

        public int UsefulLifeYears { get; set; } = 4;

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

        public double InterestRate { get; set; } = 3;

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

        public double MaintenanceCosts { get; set; } = 0;

        public double LocationCosts { get; set; } = 0;

        public double EnergyCosts { get; set; } = 0;

        public double AdditionalCosts { get; set; } = 0;

        public double MaintenanceCostsVariable { get; set; } = 0;

        public double EnergyCostsVariable { get; set; } = 0;

        public double AdditionalCostsVariable { get; set; } = 0;

        public double FixMachineHourRate { get; set; } = -1;

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
