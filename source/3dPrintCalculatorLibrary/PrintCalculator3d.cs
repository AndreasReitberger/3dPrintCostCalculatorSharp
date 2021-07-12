using AndreasReitberger.Enums;
using AndreasReitberger.Models;
using AndreasReitberger.Utilities;
using System.Collections.ObjectModel;
using System.Linq;

namespace AndreasReitberger
{
    public static class PrintCalculator3d
    {
        #region Public
        public static ObservableCollection<Calculation3dChartItem> GetCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var Costs = new ObservableCollection<Calculation3dChartItem>(
                calculation.Costs.Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return Costs;          
        }
        public static ObservableCollection<Calculation3dChartItem> GetMaterialUsage(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var MaterialUsage = new ObservableCollection<Calculation3dChartItem>(
                calculation.MaterialUsage.Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
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
                .Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return MachineCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetMaterialCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var MaterialCosts = new ObservableCollection<Calculation3dChartItem>(calculation.OverallMaterialCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Material ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition) && 
                (cost.LinkedId == calculation.Material.Id || cost.Attribute == calculation.Material.Name))
                .Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return MaterialCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetWorkstepCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var WorkstepCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost => 
                    cost.Type == CalculationAttributeType.Workstep || 
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition)
                .Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return WorkstepCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetCustomAdditionsCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var WorkstepCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return WorkstepCosts;
        }
        public static ObservableCollection<Calculation3dChartItem> GetRatesCosts(Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return new ObservableCollection<Calculation3dChartItem>();
            var RatesCosts = new ObservableCollection<Calculation3dChartItem>(calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.Margin || cost.Type == CalculationAttributeType.Tax || cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem() { Name = cost.Attribute, Value = cost.Value, AttributeType = cost.Type }));
            return RatesCosts;
        }
        #endregion
    }
}
