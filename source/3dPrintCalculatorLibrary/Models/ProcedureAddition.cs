﻿using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AndreasReitberger.Print3d.Models
{
    public partial class ProcedureAddition : ObservableObject, ICloneable, IProcedureAddition
    {
        #region Clone
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion

        #region Properties 
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string toolTip;

        [ObservableProperty]
        bool enabled;

        [ObservableProperty]
        Material3dFamily targetFamily;

        #endregion

        #region Collections

        [ObservableProperty]
        List<IProcedureCalculationParameter> parameters = new();
        #endregion

        #region Constructor
        public ProcedureAddition() {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Methods

        public double CalculateCosts()
        {
            double costs = 0;
            Parameters?.ForEach(para =>
            {
                switch (para.Type)
                {
                    case ProcedureCalculationType.ReplacementCosts:
                        costs = (para.Price / para.QuantityInPackage) / para.WearFactor;
                        break;
                    case ProcedureCalculationType.ConsumableGoods:
                        costs = (para.Price / para.QuantityInPackage) * para.AmountTakenForCalculation;
                        break;
                    default:
                        break;
                }
            });
            return costs;
        }

        #endregion

        #region Override
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);

        public override bool Equals(object obj)
        {
            if (obj is not ProcedureAddition item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
