using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Utilities;

#if SQL
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using System.Collections.ObjectModel;

namespace AndreasReitberger.Print3d.SQLite
{
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public static class Calculation3dEnhancedExtension
    {
        #region Extensions
        public static List<Calculation3dChartItem> GetCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.Costs.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetMaterialUsage(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> usage = [.. calculation.MaterialUsages.Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return usage;
        }
        public static List<Calculation3dChartItem> GetMachineCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.OverallPrinterCosts
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
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetMaterialCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.OverallMaterialCosts
                .Where(cost => (
                    cost.Type == CalculationAttributeType.Material ||
                    cost.Type == CalculationAttributeType.ProcedureSpecificAddition))
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = calculation.DifferFileCosts ? $"{cost.Attribute} ({cost.FileName})" : cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetItemCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.Costs
                .Where(cost => (
                    cost.Type == CalculationAttributeType.AdditionalItem))
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetWorkstepCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.Costs
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
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetCustomAdditionsCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }
        public static List<Calculation3dChartItem> GetRatesCosts(this Calculation3dEnhanced calculation)
        {
            if (!calculation.IsCalculated) return [];
            List<Calculation3dChartItem> costs = [.. calculation.Costs
                .Where(cost => cost.Type == CalculationAttributeType.Margin || cost.Type == CalculationAttributeType.Tax || cost.Type == CalculationAttributeType.CustomAddition)
                .Select(cost => new Calculation3dChartItem()
                {
                    Name = cost.Attribute,
                    Value = cost.Value,
                    AttributeType = cost.Type,
                    AttributeItem = cost.Item,
                    FileId = cost.FileId,
                    FileName = cost.FileName,
                })];
            return costs;
        }

#if NETFRAMEWORK || NET6_0_OR_GREATER
        public static bool SaveAsXml(this Calculation3dEnhanced calc, string path)
        {
            XmlSerializer x = new(typeof(Calculation3dEnhanced));
            DirectoryInfo tempDir = new(path);
            if (tempDir.Parent is not null)
            {
                Directory.CreateDirectory(tempDir.Parent.FullName);
                TextWriter writer = new StreamWriter(tempDir.FullName);
                x.Serialize(writer, calc);
                writer.Close();
                return true;
            }
            return false;
        }
#endif
        #endregion
    }
}
