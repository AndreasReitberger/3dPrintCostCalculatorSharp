﻿using AndreasReitberger.Enums;
using AndreasReitberger.Models.WorkstepAdditions;
using AndreasReitberger.Core.Utilities;
using Newtonsoft.Json;
//using SQLite;
using System;

namespace AndreasReitberger.Models
{
    public class Workstep : BaseModel
    {
        #region Properties
        //[PrimaryKey]
        public Guid Id
        { get; set; }

        [JsonProperty(nameof(Name))]
        string _name = string.Empty;
        [JsonIgnore]
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        [JsonProperty(nameof(Price))]
        double _price = 0;
        [JsonIgnore]
        public double Price
        {
            get { return _price; }
            set { 
                SetProperty(ref _price, value);
                TotalCosts = CalcualteTotalCosts();
            }
        }

        [JsonProperty(nameof(Quantity))]
        int _quantity = 1;
        [JsonIgnore]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                SetProperty(ref _quantity, value);
                TotalCosts = CalcualteTotalCosts();
            }
        }

        [JsonProperty(nameof(Category))]
        WorkstepCategory _category;
        [JsonIgnore]
        public WorkstepCategory Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        [JsonProperty(nameof(CalculationType))]
        CalculationType _calculationType;
        [JsonIgnore]
        public CalculationType CalculationType
        {
            get { return _calculationType; }
            set { SetProperty(ref _calculationType, value); }
        }

        [JsonProperty(nameof(Duration))]
        double _duration = 0;
        [JsonIgnore]
        public double Duration
        {
            get { return _duration; }
            set { 
                SetProperty(ref _duration, value);
                TotalCosts = CalcualteTotalCosts();
            }
        }

        [JsonProperty(nameof(Type))]
        WorkstepType _type;
        [JsonIgnore]
        public WorkstepType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        [JsonProperty(nameof(TotalCosts))]
        double _totalCosts = 0;
        [JsonIgnore]
        public double TotalCosts
        {
            get { return _totalCosts; }
            set { SetProperty(ref _totalCosts, value); }
        }
        #endregion

        #region Constructors
        public Workstep() { }
        #endregion

        #region Private
        double CalcualteTotalCosts()
        {
            try
            {
                if (Duration == 0)
                    return Price * Convert.ToDouble(Quantity);
                return Duration * Price * Convert.ToDouble(Quantity);
            }
            catch(Exception)
            {
                return 0;
            }
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("{0} ({1}) - {2:C2}", this.Name, this.Type, this.Price);
        }
        public override bool Equals(object obj)
        {
            if (obj is not Workstep item)
                return false;
            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
