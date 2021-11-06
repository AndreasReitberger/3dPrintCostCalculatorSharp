//using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndreasReitberger.Models
{
    public class HourlyMachineRate : INotifyPropertyChanged, ICloneable
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        //[PrimaryKey]
        public Guid Id { get; set; }
        public Guid PrinterId { get; set; }

        string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        bool _perYear = true;
        public bool PerYear
        {
            get => _perYear;
            set
            {
                if (_perYear != value)
                {
                    _perYear = value;
                    OnPropertyChanged();
                }
            }
        }

        double _machineHours = 0;

        public double MachineHours
        {
            get => _machineHours;
            set
            {
                if (_machineHours != value)
                {
                    _machineHours = value;
                    OnPropertyChanged();
                }
            }
        }

        double _replacementCosts = 0;
        public double ReplacementCosts
        {
            get => _replacementCosts;
            set
            {
                if (_replacementCosts != value)
                {
                    _replacementCosts = value;
                    OnPropertyChanged();
                }
            }
        }


        int _usefulLife = 4;
        public int UsefulLifeYears
        {
            get => _usefulLife;
            set
            {
                if (_usefulLife != value)
                {
                    _usefulLife = value;
                    OnPropertyChanged();
                }
            }
        }

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

        double _interestRate = 3;
        public double InterestRate
        {
            get => _interestRate;
            set
            {
                if (_interestRate != value)
                {
                    _interestRate = value;
                    OnPropertyChanged();
                }
            }
        }
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

        double _maintenanceCosts = 0;
        public double MaintenanceCosts
        {
            get => _maintenanceCosts;
            set
            {
                if (_maintenanceCosts != value)
                {
                    _maintenanceCosts = value;
                    OnPropertyChanged();
                }
            }

        }

        double _locationCosts = 0;
        public double LocationCosts
        {
            get => _locationCosts;
            set
            {
                if (_locationCosts != value)
                {
                    _locationCosts = value;
                    OnPropertyChanged();

                }
            }
        }

        double _energyCosts = 0;
        public double EnergyCosts
        {
            get => _energyCosts;
            set
            {
                if (_energyCosts != value)
                {
                    _energyCosts = value;
                    OnPropertyChanged();
                }
            }
        }

        //Additonal
        double _additionalCosts = 0;
        public double AdditionalCosts
        {
            get => _additionalCosts;
            set
            {
                if (_additionalCosts != value)
                {
                    _additionalCosts = value;
                    OnPropertyChanged();
                }
            }
        }

        double _maintenanceCostsVariable = 0;
        public double MaintenanceCostsVariable
        {
            get => _maintenanceCostsVariable;
            set
            {
                if (_maintenanceCostsVariable != value)
                {
                    _maintenanceCostsVariable = value;
                    OnPropertyChanged();
                }
            }
        }

        double _energyCostsVariable = 0;
        public double EnergyCostsVariable
        {
            get => _energyCostsVariable;
            set
            {
                if (_energyCostsVariable != value)
                {
                    _energyCostsVariable = value;
                    OnPropertyChanged();
                }
            }
        }

        double _additionalCostsVariable = 0;
        public double AdditionalCostsVariable
        {
            get => _additionalCostsVariable;
            set
            {
                if (_additionalCostsVariable != value)
                {
                    _additionalCostsVariable = value;
                    OnPropertyChanged();
                }
            }
        }
        
        double _fixMachineHourRate = -1;
        public double FixMachineHourRate
        {
            get => _fixMachineHourRate;
            set
            {
                if (_fixMachineHourRate == value) return;
                _fixMachineHourRate = value;
                OnPropertyChanged();
                
            }
        }

        public double CalcMachineHourRate
        {
            get
            {
                return GetMachineHourRate();
            }
        }
        public double TotalCosts
        {
            get
            {
                return GetTotalCosts();
            }
        }
        #endregion

        #region Constructor
        public HourlyMachineRate() { }
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
