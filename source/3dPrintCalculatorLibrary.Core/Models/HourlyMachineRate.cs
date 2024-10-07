using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(HourlyMachineRate)}s")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class HourlyMachineRate : ObservableObject, IHourlyMachineRate, ICloneable
    {
        #region Properties
        [ObservableProperty]
#if SQL
        [property: PrimaryKey]
#endif
        Guid id;

#if SQL
        [ObservableProperty]
        [property: JsonIgnore, XmlIgnore]
        Guid printerId;

        [ObservableProperty]
        [property: ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        Printer3d? printer;
#else
        [ObservableProperty]
        IPrinter3d? printer;
#endif

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

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
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

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
        public double CalcInterest
        {
            get
            {
                if (ReplacementCosts == 0 || InterestRate == 0)
                    return 0;
                else
                    return (ReplacementCosts / 2) / 100 * InterestRate;
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

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
        public double CalcMachineHourRate => GetMachineHourRate();

        [JsonIgnore]
#if SQL
        [Ignore]
#endif
        public double TotalCosts => GetTotalCosts();
#endregion

        #region Constructor
        public HourlyMachineRate()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods
        public double GetMachineHourRate()
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
        public double GetTotalCosts()
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
