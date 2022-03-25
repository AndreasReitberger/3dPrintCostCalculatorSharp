using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Models
{
    [Table("CalculationProfiles")]
    public class Calculation3dProfile : BaseModel
    {
        #region Properties
        [PrimaryKey]
        public Guid Id { get; set; }

        [JsonProperty(nameof(Name))]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        #region Linked Customer
        /*
        [JsonIgnore, XmlIgnore]
        public Guid CustomerId { get; set; }

        [JsonProperty(nameof(Customer))]
        Customer3d _customer;
        [JsonIgnore]
        [ManyToOne(nameof(CustomerId))]
        public Customer3d Customer
        {
            get { return _customer; }
            set { SetProperty(ref _customer, value); }
        }
        */
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Customer3d> Customers { get; set; } = new();
        #endregion

        #region Presets

        #region Rates

        [JsonProperty(nameof(FailRate))]
        double _failRate = 0;
        [JsonIgnore]
        public double FailRate
        {
            get { return _failRate; }
            set { SetProperty(ref _failRate, value); }
        }

        [JsonProperty(nameof(ApplyTaxRate))]
        bool _applyTaxRate = false;
        [JsonIgnore]
        public bool ApplyTaxRate
        {
            get { return _applyTaxRate; }
            set { SetProperty(ref _applyTaxRate, value); }
        }

        [JsonProperty(nameof(TaxRate))]
        double _taxRate = 0;
        [JsonIgnore]
        public double TaxRate
        {
            get { return _taxRate; }
            set { SetProperty(ref _taxRate, value); }
        }

        [JsonProperty(nameof(MarginRate))]
        double _marginRate = 0;
        [JsonIgnore]
        public double MarginRate
        {
            get { return _marginRate; }
            set { SetProperty(ref _marginRate, value); }
        }

        #endregion

        #region Handling

        [JsonProperty(nameof(HandlingsFee))]
        double _handlingsFee = 0;
        [JsonIgnore]
        public double HandlingsFee
        {
            get { return _handlingsFee; }
            set { SetProperty(ref _handlingsFee, value); }
        }

        #endregion

        #region Energy

        [JsonProperty(nameof(ApplyenergyCost))]
        bool _applyEnergyCost = false;
        [JsonIgnore]
        public bool ApplyenergyCost
        {
            get { return _applyEnergyCost; }
            set { SetProperty(ref _applyEnergyCost, value); }
        }

        [JsonProperty(nameof(PowerLevel))]
        int _powerLevel = 0;
        [JsonIgnore]
        public int PowerLevel
        {
            get { return _powerLevel; }
            set { SetProperty(ref _powerLevel, value); }
        }

        [JsonProperty(nameof(EnergyCostsPerkWh))]
        double _energyCostsPerkWh = 0;
        [JsonIgnore]
        public double EnergyCostsPerkWh
        {
            get { return _energyCostsPerkWh; }
            set { SetProperty(ref _energyCostsPerkWh, value); }
        }

        #endregion

        #endregion

        #endregion

        #region Constructor
        public Calculation3dProfile() 
        {
            Id = Guid.NewGuid();
        }
        public Calculation3dProfile(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
