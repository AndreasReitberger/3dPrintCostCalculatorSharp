using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.Core.Utilities;

#if SQL
namespace AndreasReitberger.Print3d.SQLite.Extension
{
#else
namespace AndreasReitberger.Print3d.Core.Extension
{
#endif
    public static class Calculation3dExtension
    {
        #region Extensions
        public static List<Calculation3dChartItem> GetAllCosts(this Calculation3d calculation)
        {
            if (!calculation.IsCalculated) return [];
            //List<Calculation3dChartItem> costs = calculation.GetCosts();
            List<Calculation3dChartItem> costs = calculation.GetMachineCosts();
            costs.AddRange(calculation.GetMaterialCosts());
            costs.AddRange(calculation.GetItemCosts());
            costs.AddRange(calculation.GetWorkstepCosts());
            costs.AddRange(calculation.GetCustomAdditionsCosts());
            costs.AddRange(calculation.GetRatesCosts());
            return costs;
        }
        public static List<Calculation3dChartItem> GetCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetMaterialUsage(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetMachineCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetMaterialCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetItemCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetWorkstepCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetCustomAdditionsCosts(this Calculation3d calculation)
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
        public static List<Calculation3dChartItem> GetRatesCosts(this Calculation3d calculation)
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
        public static bool SaveAsXml(this Calculation3d calc, string path)
        {
            /*
            XmlSerializer x = new(typeof(Calculation3d));
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
            */
            FileInfo fileInfo = new(path);
            if (fileInfo.Directory is not null)
            {
                Directory.CreateDirectory(fileInfo.Directory.FullName);
                using FileStream stream = File.Create(fileInfo.FullName);
                JsonSerializer.Serialize(stream, calc,
                    SourceGenerationContext.Default.Calculation3dEnhanced);
                return true;
            }
            return false;
        }
#endif
        #endregion
    }
}
