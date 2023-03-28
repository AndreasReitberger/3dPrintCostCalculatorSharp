using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite;
using AndreasReitberger.Print3d.Utilities;
using System.Collections.ObjectModel;
using System.Linq;

namespace AndreasReitberger.Print3d.SQLite
{
    public static class PrintCalculator3d
    {
        #region Public
        public static ObservableCollection<Calculation3dChartItem> GetCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var Costs = new ObservableCollection<Calculation3dChartItem>(
                calculation.Costs.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return Costs;
        }
        public static ObservableCollection<Calculation3dChartItem> GetMaterialUsage(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var MaterialUsage = new ObservableCollection<Calculation3dChartItem>(
                calculation.MaterialUsage.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MaterialUsage;
        }
        public static ObservableCollection<Calculation3dChartItem> GetMachineCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var MachineCosts = new ObservableCollection<Calculation3dChartItem>(calculation.OverallPrinterCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Machine ||
                    cost.Type == CalculationAttributeType.Energy ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition) &&
                (cost.LinkedId == calculation.Printer.Id || cost.Attribute == calculation.Printer.Name)
                )
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = calculation.DifferFileCosts ? $"{cost.Attribute} ({cost.FileName})" : cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MachineCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetMaterialCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var MaterialCosts = new ObservableCollection<Calculation3dChartItem>(calculation.OverallMaterialCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Material ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition) &&
                (calculation.CombineMaterialCosts || (cost.LinkedId == calculation.Material.Id || cost.Attribute == calculation.Material.Name)))
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = calculation.DifferFileCosts ? $"{cost.Attribute} ({cost.FileName})" : cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return MaterialCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetWorkstepCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var WorkstepCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost =>
                    cost.Type == CalculationAttributeType.Workstep ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return WorkstepCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetCustomAdditionsCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var WorkstepCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return WorkstepCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetRatesCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var RatesCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.Margin || cost.Type == CalculationAttributeType.Tax || cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                }));
            return RatesCosts;
        }
        #endregion
    }
}
