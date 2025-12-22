using CommunityToolkit.Mvvm.ComponentModel;

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
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ObservableProperty]
        [JsonIgnore, XmlIgnore]
        [ForeignKey(typeof(Printer3d))]
        public partial Guid PrinterId { get; set; }

        [ObservableProperty]
        [ManyToOne(nameof(PrinterId), CascadeOperations = CascadeOperation.All)]
        public partial Printer3d? Printer { get; set; }
#else
        [ObservableProperty]
        public partial IPrinter3d? Printer { get; set; }
#endif

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
        public partial double InterestRate { get; set; } = 3;

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
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.HourlyMachineRate);
        public object Clone() => MemberwiseClone();

        #endregion
    }
}
