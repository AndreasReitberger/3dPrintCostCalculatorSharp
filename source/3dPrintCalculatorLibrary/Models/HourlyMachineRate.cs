using AndreasReitberger.Core.Utilities;
using AndreasReitberger.Print3d.Interface;
using Newtonsoft.Json;
using SQLite;
using System;

namespace AndreasReitberger.Print3d.Models
{
    [Table("HourlyMachineRates")]
    public class HourlyMachineRate : BaseModel, IHourlyMachineRate
    {
        #region Properties
        [PrimaryKey]
        public Guid Id
        { get; set; }

        public Guid PrinterId
        { get; set; }

        [JsonProperty(nameof(Name))]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(nameof(PerYear))]
        bool _perYear = true;
        [JsonIgnore]
        public bool PerYear
        {
            get { return _perYear; }
            set { SetProperty(ref _perYear, value); }
        }

        [JsonProperty(nameof(MachineHours))]
        double _machineHours = 0;
        [JsonIgnore]
        public double MachineHours
        {
            get { return _machineHours; }
            set { SetProperty(ref _machineHours, value); }
        }

        [JsonProperty(nameof(ReplacementCosts))]
        double _replacementCosts = 0;
        [JsonIgnore]
        public double ReplacementCosts
        {
            get { return _replacementCosts; }
            set { SetProperty(ref _replacementCosts, value); }
        }

        [JsonProperty(nameof(UsefulLifeYears))]
        int _usefulLife = 4;
        [JsonIgnore]
        public int UsefulLifeYears
        {
            get { return _usefulLife; }
            set { SetProperty(ref _usefulLife, value); }
        }

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

        [JsonProperty(nameof(InterestRate))]
        double _interestRate = 3;
        [JsonIgnore]
        public double InterestRate
        {
            get { return _interestRate; }
            set { SetProperty(ref _interestRate, value); }
        }
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

        [JsonProperty(nameof(MaintenanceCosts))]
        double _maintenanceCosts = 0;
        [JsonIgnore]
        public double MaintenanceCosts
        {
            get { return _maintenanceCosts; }
            set { SetProperty(ref _maintenanceCosts, value); }
        }

        [JsonProperty(nameof(LocationCosts))]
        double _locationCosts = 0;
        [JsonIgnore]
        public double LocationCosts
        {
            get { return _locationCosts; }
            set { SetProperty(ref _locationCosts, value); }
        }

        [JsonProperty(nameof(EnergyCosts))]
        double _energyCosts = 0;
        [JsonIgnore]
        public double EnergyCosts
        {
            get { return _energyCosts; }
            set { SetProperty(ref _energyCosts, value); }
        }

        //Additonal
        [JsonProperty(nameof(AdditionalCosts))]
        double _additionalCosts = 0;
        [JsonIgnore]
        public double AdditionalCosts
        {
            get { return _additionalCosts; }
            set { SetProperty(ref _additionalCosts, value); }
        }

        [JsonProperty(nameof(MaintenanceCostsVariable))]
        double _maintenanceCostsVariable = 0;
        [JsonIgnore]
        public double MaintenanceCostsVariable
        {
            get { return _maintenanceCostsVariable; }
            set { SetProperty(ref _maintenanceCostsVariable, value); }
        }

        [JsonProperty(nameof(EnergyCostsVariable))]
        double _energyCostsVariable = 0;
        [JsonIgnore]
        public double EnergyCostsVariable
        {
            get { return _energyCostsVariable; }
            set { SetProperty(ref _energyCostsVariable, value); }
        }

        [JsonProperty(nameof(AdditionalCostsVariable))]
        double _additionalCostsVariable = 0;
        [JsonIgnore]
        public double AdditionalCostsVariable
        {
            get { return _additionalCostsVariable; }
            set { SetProperty(ref _additionalCostsVariable, value); }
        }

        [JsonProperty(nameof(FixMachineHourRate))]
        double _fixMachineHourRate = -1;
        [JsonIgnore]
        public double FixMachineHourRate
        {
            get { return _fixMachineHourRate; }
            set { SetProperty(ref _fixMachineHourRate, value); }
        }

        [Ignore, JsonIgnore]
        public double CalcMachineHourRate
        {
            get
            {
                return GetMachineHourRate();
            }
        }
        
        [Ignore, JsonIgnore]
        public double TotalCosts
        {
            get
            {
                return GetTotalCosts();
            }
        }
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
