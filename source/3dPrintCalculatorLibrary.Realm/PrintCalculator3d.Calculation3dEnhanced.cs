using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AndreasReitberger.Print3d.Realm
{
    public static partial class PrintCalculator3d
    {
        #region Public
        public static List<Calculation3dChartItem> GetCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var Costs = new List<Calculation3dChartItem>(
                calculation.Costs.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return Costs;
        }
        public static List<Calculation3dChartItem> GetMaterialUsage(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var MaterialUsage = new List<Calculation3dChartItem>(
                calculation.MaterialUsage.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MaterialUsage;
        }
        public static List<Calculation3dChartItem> GetMachineCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var MachineCosts = new List<Calculation3dChartItem>(calculation.OverallPrinterCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Machine ||
                    cost.Type == CalculationAttributeType.Energy ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition)
                )
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = calculation.DifferFileCosts ? $"{cost.Attribute} ({cost.FileName})" : cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MachineCosts;
        }
        public static List<Calculation3dChartItem> GetMaterialCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var MaterialCosts = new List<Calculation3dChartItem>(calculation.OverallMaterialCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Material ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition))
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = calculation.DifferFileCosts ? $"{cost.Attribute} ({cost.FileName})" : cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MaterialCosts;
        }
        public static List<Calculation3dChartItem> GetWorkstepCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var WorkstepCosts = new List<Calculation3dChartItem>(calculation.Costs
                .Where(cost =>
                    cost.Type == CalculationAttributeType.Workstep ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return WorkstepCosts;
        }
        public static List<Calculation3dChartItem> GetCustomAdditionsCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var WorkstepCosts = new List<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return WorkstepCosts;
        }
        public static List<Calculation3dChartItem> GetRatesCosts(Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            var RatesCosts = new List<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.Margin || cost.Type == CalculationAttributeType.Tax || cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return RatesCosts;
        }
        #endregion
    }
}
