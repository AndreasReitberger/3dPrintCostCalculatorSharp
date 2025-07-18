﻿using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.FileAdditions;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasReitberger.Print3d.Models
{
    public partial class Calculation3dEnhanced
    {
        #region Methods

        public void ClearCalculation()
        {
            PrintTimes ??= [];
            MaterialUsage ??= [];
            OverallMaterialCosts ??= [];
            OverallPrinterCosts ??= [];
            Costs ??= [];

            PrintTimes.Clear();
            MaterialUsage.Clear();
            OverallMaterialCosts.Clear();
            OverallPrinterCosts.Clear();
            Costs.Clear();
        }

        public void CalculateCosts()
        {
            ClearCalculation();

            int quantity = PrintInfos.Select(f => f.FileUsage).Select(file => file?.Quantity ?? 0).ToList().Sum();
            // Add the handling fee based on the file quantity
            CalculationAttribute? handlingsFee = Rates.FirstOrDefault(costs => costs.Attribute == "HandlingFee" || costs.Type == CalculationAttributeType.HandlingFee);
            CalculationAttribute? margin = Rates.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Margin);
            CalculationAttribute? tax = Rates.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Tax);

            // New approach
            foreach (Print3dInfo info in PrintInfos)
            {
                File3dUsage? fileUsage = info.FileUsage;
                File3d? file = fileUsage?.File;
                if (fileUsage is not null && file is not null)
                {
                    double printTime = file.PrintTime * (fileUsage.MultiplyPrintTimeWithQuantity ? (fileUsage.Quantity * fileUsage.PrintTimeQuantityFactor) : 1);
                    PrintTimes.Add(new CalculationAttribute()
                    {
                        Attribute = file.FileName,
                        Value = printTime,
                        Type = CalculationAttributeType.Machine,
                        Target = CalculationAttributeTarget.File,
                        FileId = file.Id,
                        FileName = file.FileName,
                    });

                    if (handlingsFee is not null && handlingsFee.Value > 0 && handlingsFee.ApplyPerFile)
                    {
                        Costs.Add(new CalculationAttribute()
                        {
                            Attribute = "HandlingFee",
                            Type = CalculationAttributeType.FixCost,
                            Target = CalculationAttributeTarget.File,
                            Value = Convert.ToDouble(handlingsFee?.Value * fileUsage.Quantity),
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                    if (FailRate > 0)
                    {
                        PrintTimes.Add(new CalculationAttribute()
                        {
                            Attribute = $"{file.FileName}_FailRate",
                            Type = CalculationAttributeType.Machine,
                            Item = CalculationAttributeItem.FailRate,
                            Target = CalculationAttributeTarget.File,
                            Value = printTime * FailRate / 100,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                    double percentageUsage = info.MaterialUsages.Select(mu => mu.PercentageValue).Sum();
                    if (percentageUsage > 1 || percentageUsage <= 0)
                        throw new ArgumentOutOfRangeException($"The overall percentage of the material usage is greater than 1 (=100%): {percentageUsage}");
                    foreach (Material3dUsage materialUsageInfo in info.MaterialUsages)
                    {
                        //Material3d material = info.Material;
                        Material3d? material = materialUsageInfo.Material;
                        if (material is not null)
                        {
                            double _weight = 0;
                            if (file.Volume > 0)
                            {
                                double _volume = file.Volume;
                                _weight = _volume * material.Density;
                            }
                            else if (file.Weight is not null)
                            {
                                _weight = file.Weight.Weight * Convert.ToDouble(UnitFactor.GetUnitFactor(file.Weight.Unit));
                            }
                            // Needed material in g
                            double _material = _weight * fileUsage.Quantity * materialUsageInfo.PercentageValue;
                            MaterialUsage.Add(new CalculationAttribute()
                            {
                                Attribute = material.Name,
                                Value = _material,
                                Type = CalculationAttributeType.Material,
                                Item = CalculationAttributeItem.Default,
                                Target = CalculationAttributeTarget.File,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                            if (FailRate > 0)
                            {
                                MaterialUsage.Add(new CalculationAttribute()
                                {
                                    Attribute = $"{material.Name}_FailRate",
                                    Value = _material * FailRate / 100,
                                    Type = CalculationAttributeType.Material,
                                    Item = CalculationAttributeItem.FailRate,
                                    Target = CalculationAttributeTarget.File,
                                    FileId = file.Id,
                                    FileName = file.FileName,
                                });
                            }

                            double refreshed = 0;
                            if (ApplyProcedureSpecificAdditions)
                            {
                                if (material.MaterialFamily == Material3dFamily.Powder)
                                {
                                    CalculationProcedureAttribute? attribute = ProcedureAttributes.FirstOrDefault(
                                        attr => attr.Attribute == ProcedureAttribute.MaterialRefreshingRatio && attr.Level == CalculationLevel.Material);
                                    if (attribute is not null)
                                    {
                                        CalculationProcedureParameter? minPowderNeeded = attribute.Parameters.FirstOrDefault(para => para.Type == ProcedureParameter.MinPowderNeeded);
                                        if (minPowderNeeded is not null)
                                        {
                                            double powderInBuildArea = minPowderNeeded.Value;
                                            Material3dProcedureAttribute? refreshRatio = material.ProcedureAttributes.FirstOrDefault(ratio => ratio.Attribute == ProcedureAttribute.MaterialRefreshingRatio);
                                            if (refreshRatio is not null)
                                            {
                                                // this value is in liter
                                                CalculationAttribute? materialPrintObject = MaterialUsage.FirstOrDefault(usage =>
                                                    usage.Attribute == material.Name);
                                                if (materialPrintObject is not null)
                                                {
                                                    double refreshedMaterial = (powderInBuildArea -
                                                        (materialPrintObject.Value * material.FactorLToKg / UnitFactor.GetUnitFactor(Unit.Kilogram))) * refreshRatio.Value / 100f;
                                                    refreshed = (refreshedMaterial / material.FactorLToKg * UnitFactor.GetUnitFactor(Unit.Kilogram));
                                                }
                                                else
                                                    refreshed = 0;
                                            }
                                        }
                                    }
                                }

                                // Custom procedure additions
                                if (ProcedureAdditions?.Count > 0)
                                {
                                    IEnumerable<ProcedureAddition> procedureAdditions = ProcedureAdditions
                                        .Where(addition => addition.TargetFamily == material.MaterialFamily
                                            && addition.Target == ProcedureAdditionTarget.Material
                                            && addition.Enabled
                                            );
                                    foreach (ProcedureAddition add in procedureAdditions)
                                    {
                                        double costs = add.CalculateCosts();
                                        OverallMaterialCosts.Add(new CalculationAttribute()
                                        {
                                            LinkedId = material.Id,
                                            Attribute = add.Name,
                                            Type = CalculationAttributeType.ProcedureSpecificAddition,
                                            Target = CalculationAttributeTarget.File,
                                            Value = costs,
                                            FileId = file.Id,
                                            FileName = file.FileName,
                                        });
                                    }
                                }
                            }

                            double pricePerGramm = Convert.ToDouble(material.UnitPrice) /
                                Convert.ToDouble(Convert.ToDouble(material.PackageSize) * Convert.ToDouble(UnitFactor.GetUnitFactor(material.Unit)));

                            // Calculate the cost for each material usage of the current file
                            foreach (CalculationAttribute materialUsage in MaterialUsage.Where(mu => mu.FileId == file.Id))
                            {
                                if (materialUsage is null) continue;
                                double totalCosts = Convert.ToDouble(materialUsage.Value * pricePerGramm);
                                OverallMaterialCosts.Add(new CalculationAttribute()
                                {
                                    LinkedId = material.Id,
                                    // Keep the linking to the currently used material
                                    Attribute = materialUsage.Item == CalculationAttributeItem.FailRate ? $"{material.Name}_FailRate" : material.Name,
                                    Type = CalculationAttributeType.Material,
                                    Target = CalculationAttributeTarget.File,
                                    Item = materialUsage.Item,
                                    Value = totalCosts,
                                    FileId = materialUsage.FileId,
                                    FileName = materialUsage.FileName,
                                });
                            }
                            // If the material is refreshed, add this to the material costs as well
                            if (refreshed > 0)
                            {
                                double refreshCosts = Convert.ToDouble(
                                refreshed * pricePerGramm);
                                OverallMaterialCosts.Add(new CalculationAttribute()
                                {
                                    LinkedId = material.Id,
                                    Attribute = $"{material.Name} (Refreshed)",
                                    Type = CalculationAttributeType.Material,
                                    Item = CalculationAttributeItem.PowderRefresh,
                                    Target = CalculationAttributeTarget.File,
                                    Value = refreshCosts,
                                    FileId = file.Id,
                                    FileName = file.FileName,
                                });
                            }
                        }
                    }
                    Printer3d? printer = info.Printer;
                    if (printer is not null)
                    {
                        foreach (CalculationAttribute pt in PrintTimes.Where(pt => pt?.FileId == file?.Id))
                        {
                            // Calculate the machine costs based on the hourly machine rate
                            if (printer.HourlyMachineRate is not null)
                            {
                                double machineHourRate = Convert.ToDouble(printer.HourlyMachineRate.CalcMachineHourRate) * pt.Value;
                                if (machineHourRate > 0)
                                {
                                    OverallPrinterCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Machine,
                                        Target = CalculationAttributeTarget.File,
                                        Item = pt.Item,
                                        Value = machineHourRate,
                                        FileId = pt.FileId,
                                        FileName = pt.FileName,
                                    });
                                }
                            }
                            // Add energy costs if applied
                            if (ApplyEnergyCost)
                            {
                                double consumption = Convert.ToDouble((pt.Value * Convert.ToDouble(printer.PowerConsumption)) / 1000.0) / 100.0 * Convert.ToDouble(PowerLevel);
                                double totalEnergyCost = consumption * EnergyCostsPerkWh;
                                if (totalEnergyCost > 0)
                                {
                                    OverallPrinterCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Energy,
                                        Target = CalculationAttributeTarget.File,
                                        Item = pt.Item,
                                        Value = totalEnergyCost,
                                        FileId = pt.FileId,
                                        FileName = pt.FileName,
                                    });
                                }
                            }
                        }
                        if (ApplyProcedureSpecificAdditions)
                        {
                            // Filter for the current printer procedure
                            List<CalculationProcedureAttribute> attributes = ProcedureAttributes.Where(
                                attr => attr.Family == printer.MaterialType && attr.Level == CalculationLevel.Printer).ToList();
                            foreach (CalculationProcedureAttribute attribute in attributes)
                            {
                                foreach (CalculationProcedureParameter parameter in attribute.Parameters)
                                {
                                    if (attribute.PerFile || (OverallPrinterCosts?
                                            .FirstOrDefault(attr => attr.Attribute == parameter.Type.ToString() && attr.LinkedId == printer.Id) is null))
                                    {
                                        OverallPrinterCosts?.Add(new CalculationAttribute()
                                        {
                                            LinkedId = printer.Id,
                                            Attribute = parameter.Type.ToString(),
                                            Type = CalculationAttributeType.ProcedureSpecificAddition,
                                            Target = CalculationAttributeTarget.File,
                                            Value = attribute.PerPiece ? parameter.Value * fileUsage.Quantity : parameter.Value,
                                            FileId = file.Id,
                                            FileName = file.FileName,
                                        });
                                    }
                                }
                            }

                            // Custom procedure additions
                            if (ProcedureAdditions?.Count > 0)
                            {
                                IEnumerable<ProcedureAddition> procedureAdditions = ProcedureAdditions
                                    .Where(addition => addition.TargetFamily == printer.MaterialType
                                        && addition.Target == ProcedureAdditionTarget.Machine
                                        && addition.Enabled
                                        );
                                foreach (ProcedureAddition add in procedureAdditions)
                                {
                                    double costs = add.CalculateCosts();
                                    OverallPrinterCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = add.Name,
                                        Type = CalculationAttributeType.ProcedureSpecificAddition,
                                        Target = CalculationAttributeTarget.File,
                                        Value = costs,
                                        FileId = file.Id,
                                        FileName = file.FileName,
                                    });
                                }
                            }
                        }
                    }

                    if (WorkstepUsages?.Count > 0)
                    {
                        // Only take the worksteps, which are set as `PerPiece` here
                        foreach (WorkstepUsage wsu in WorkstepUsages.Where(wsu => wsu?.Workstep?.CalculationType == CalculationType.PerPiece))
                        {
                            Workstep? ws = wsu.Workstep;
                            if (ws is null) continue;
                            double totalPerPiece = wsu.TotalCosts * fileUsage.Quantity;
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = ws.Id,
                                Attribute = ws.Name,
                                Type = CalculationAttributeType.Workstep,
                                Target = CalculationAttributeTarget.File,
                                Value = totalPerPiece,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }

                    // Additional items
                    if (AdditionalItems?.Count > 0)
                    {
                        foreach (Item3dUsage item in AdditionalItems.Where(usage => usage.LinkedToFile))
                        {
                            // If the item is not for the current file, continue
                            if (item.Item is null || file.Id != item.File?.Id) continue;

                            double totalPerPiece = (item.Item?.PricePerPiece ?? 0) * item.Quantity * fileUsage.Quantity;
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = item.Id,
                                Attribute = item.Item?.Name ?? string.Empty,
                                Type = CalculationAttributeType.AdditionalItem,
                                Target = CalculationAttributeTarget.File,
                                Value = totalPerPiece,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }
                    // If items are set to a file
                    if (info.Items?.Count > 0)
                    {
                        foreach (Item3dUsage item in info.Items)
                        {
                            // If the item is not for the current file, continue
                            if (item?.Item == null) continue;

                            double totalPerPiece = (item.Item?.PricePerPiece ?? 0) * item.Quantity * fileUsage.Quantity;
                            Costs.Add(new CalculationAttribute()
                            {
                                LinkedId = item.Id,
                                Attribute = item.Item?.Name ?? string.Empty,
                                Type = CalculationAttributeType.AdditionalItem,
                                Target = CalculationAttributeTarget.File,
                                Value = totalPerPiece,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }
                    // Custom additions before adding the margin
                    List<CustomAddition> customAdditionsBeforeMargin = [.. CustomAdditions.Where(addition => addition.CalculationType == CustomAdditionCalculationType.BeforeApplingMargin)];

                    if (customAdditionsBeforeMargin?.Count > 0)
                    {
                        SortedDictionary<int, double> additions = [];
                        foreach (CustomAddition ca in customAdditionsBeforeMargin)
                        {
                            if (additions.ContainsKey(ca.Order))
                                additions[ca.Order] += ca.Percentage;
                            else
                                additions.Add(ca.Order, ca.Percentage);
                        }
                        foreach (KeyValuePair<int, double> pairs in additions)
                        {
                            double costsSoFar = GetTotalCosts(file.Id);
                            Costs.Add(new CalculationAttribute()
                            {
                                Attribute = string.Format("CustomAdditionPreMargin_Order{0}", pairs.Key),
                                Type = CalculationAttributeType.CustomAddition,
                                Target = CalculationAttributeTarget.File,
                                Value = pairs.Value * costsSoFar / 100.0,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }

                    //Margin
                    if (margin is not null && !margin.SkipForCalculation)
                    {
                        double costsSoFar = GetTotalCosts(file.Id);
                        if (ApplyEnhancedMarginSettings)
                        {
                            double excludedCosts = 0;
                            if (ExcludePrinterCostsFromMarginCalculation)
                            {
                                excludedCosts += GetTotalCosts(file.Id, CalculationAttributeType.Machine);
                            }
                            if (ExcludeMaterialCostsFromMarginCalculation)
                            {
                                excludedCosts += GetTotalCosts(file.Id, CalculationAttributeType.Material);
                            }
                            if (ExcludeWorkstepsFromMarginCalculation)
                            {
                                excludedCosts += GetTotalCosts(file.Id, CalculationAttributeType.Workstep);
                            }
                            // Subtract costs 
                            if (excludedCosts > 0)
                            {
                                costsSoFar -= excludedCosts;
                            }
                        }

                        // Get all items where margin calculation is disabled.
                        List<CalculationAttribute> skipMarginCalculation = Rates.Where(rate => rate.SkipForMargin && rate.ApplyPerFile).ToList();
                        skipMarginCalculation.ForEach((item) =>
                        {
                            costsSoFar -= item.Value;
                        });

                        double marginValue = costsSoFar * margin.Value / (margin.IsPercentageValue ? 100.0 : 1.0);
                        if (marginValue > 0)
                        {
                            Costs.Add(new CalculationAttribute()
                            {
                                Attribute = "Margin",
                                Type = CalculationAttributeType.Margin,
                                Target = CalculationAttributeTarget.File,
                                Value = marginValue,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }

                    // Custom additions before margin
                    List<CustomAddition> customAdditionsAfterMargin =
                        CustomAdditions.Where(addition => addition.CalculationType == CustomAdditionCalculationType.AfterApplingMargin).ToList();
                    if (customAdditionsAfterMargin.Count > 0)
                    {
                        SortedDictionary<int, double> additions = [];
                        foreach (CustomAddition ca in customAdditionsAfterMargin)
                        {
                            if (additions.ContainsKey(ca.Order))
                                additions[ca.Order] += ca.Percentage;
                            else
                                additions.Add(ca.Order, ca.Percentage);
                        }
                        foreach (KeyValuePair<int, double> pairs in additions)
                        {
                            double costsSoFar = GetTotalCosts(file.Id);
                            if (costsSoFar > 0)
                            {
                                Costs.Add(new CalculationAttribute()
                                {
                                    Attribute = $"CustomAdditionPostMargin_Order{pairs.Key}",
                                    Type = CalculationAttributeType.CustomAddition,
                                    Target = CalculationAttributeTarget.File,
                                    Value = pairs.Value * costsSoFar / 100.0,
                                    FileId = file.Id,
                                    FileName = file.FileName,
                                });
                            }
                        }
                    }

                    //Tax
                    //CalculationAttribute? Tax = Rates.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Tax);
                    if (tax is not null && !tax.SkipForCalculation)
                    {
                        double costsSoFar = GetTotalCosts(file.Id);
                        double taxValue = costsSoFar * tax.Value / (tax.IsPercentageValue ? 100.0 : 1.0);
                        if (taxValue > 0)
                        {
                            Costs.Add(new CalculationAttribute()
                            {
                                Attribute = "Tax",
                                Type = CalculationAttributeType.Tax,
                                Target = CalculationAttributeTarget.File,
                                Value = taxValue,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }
                }
            }

            // If the handling fee is not set per file, add it once afterwards
            if (handlingsFee is not null && handlingsFee.Value > 0 && !handlingsFee.ApplyPerFile)
            {
                Costs.Add(new CalculationAttribute()
                {
                    Attribute = "HandlingFee",
                    Type = CalculationAttributeType.FixCost,
                    Target = CalculationAttributeTarget.Project,
                    Value = handlingsFee.Value,
                    FileId = Guid.Empty,
                    FileName = string.Empty,
                });
                if (handlingsFee?.SkipForMargin == false && margin is not null)
                {
                    double marginValue = handlingsFee.Value * margin.Value / (margin.IsPercentageValue ? 100.0 : 1.0);
                    if (marginValue > 0)
                    {
                        Costs.Add(new CalculationAttribute()
                        {
                            Attribute = "Margin",
                            Type = CalculationAttributeType.Margin,
                            Target = CalculationAttributeTarget.Project,
                            Value = marginValue,
                            FileId = Guid.Empty,
                            FileName = string.Empty,
                        });
                    }
                }
            }

            // Worksteps
            if (WorkstepUsages?.Count > 0)
            {
                foreach (WorkstepUsage wsu in WorkstepUsages.Where(wsu => wsu?.Workstep?.CalculationType != CalculationType.PerPiece))
                {
                    Workstep? ws = wsu.Workstep;
                    if (ws is null) continue;
                    double totalPerJob = wsu.TotalCosts;
                    Costs.Add(new CalculationAttribute()
                    {
                        LinkedId = ws.Id,
                        Attribute = ws.Name,
                        Type = CalculationAttributeType.Workstep,
                        Target = CalculationAttributeTarget.Project,
                        Value = totalPerJob,
                    });
                }
            }

            // Additional items
            if (AdditionalItems?.Count > 0)
            {
                foreach (Item3dUsage item in AdditionalItems.Where(usage => !usage.LinkedToFile))
                {
                    // If the item is not for the current file, continue
                    if (item?.Item == null) continue;

                    double totalPerPiece = (item.Item?.PricePerPiece ?? 0) * item.Quantity;
                    Costs.Add(new CalculationAttribute()
                    {
                        LinkedId = item.Id,
                        Attribute = item.Item?.Name ?? string.Empty,
                        Type = CalculationAttributeType.AdditionalItem,
                        Target = CalculationAttributeTarget.Project,
                        Value = totalPerPiece,
                    });
                }
            }

            if (ApplyProcedureSpecificAdditions)
            {
                List<CalculationProcedureAttribute> multiMaterialAttributes = [.. ProcedureAttributes.Where(attr => attr.Family == Procedure && attr.Level == CalculationLevel.Calculation)];
                for (int i = 0; i < multiMaterialAttributes?.Count; i++)
                {
                    CalculationProcedureAttribute attribute = multiMaterialAttributes[i];
                    for (int j = 0; j < attribute.Parameters.Count; j++)
                    {
                        CalculationProcedureParameter parameter = attribute.Parameters[j];
                        switch (parameter.Type)
                        {
                            case ProcedureParameter.MultiMaterialCalculation:
                                /*
                                if (Printer?.MaterialType == Material3dFamily.Filament)
                                {
                                    CombineMaterialCosts = parameter.Value > 0;
                                }*/
                                break;
                            default:
                                break;
                        }
                    }
                }

                // Custom additions
                if (ProcedureAdditions?.Count > 0)
                {
                    IEnumerable<ProcedureAddition> procedureAdditions = ProcedureAdditions
                        .Where(addition => addition.TargetFamily == Procedure
                            && addition.Target == ProcedureAdditionTarget.General
                            && addition.Enabled
                            );
                    foreach (ProcedureAddition add in procedureAdditions)
                    {
                        double costs = add.CalculateCosts();
                        Costs.Add(new CalculationAttribute()
                        {
                            LinkedId = Guid.Empty,
                            Attribute = add.Name,
                            Type = CalculationAttributeType.ProcedureSpecificAddition,
                            Target = CalculationAttributeTarget.Project,
                            Value = costs,
                        });
                    }
                }
            }

            // Add the tax for the unlinked file costs
            if (tax is not null && !tax.SkipForCalculation)
            {
                IEnumerable<CalculationAttribute>? unlinkedCosts = Costs.Where(c => c.FileId == Guid.Empty);
                double costsSoFar = unlinkedCosts?.Sum(cost => cost.Value) ?? 0;
                double taxValue = costsSoFar * tax.Value / (tax.IsPercentageValue ? 100.0 : 1.0);
                if (taxValue > 0)
                {
                    Costs.Add(new CalculationAttribute()
                    {
                        Attribute = "Tax",
                        Type = CalculationAttributeType.Tax,
                        Target = CalculationAttributeTarget.Project,
                        Value = taxValue,
                        FileId = Guid.Empty,
                        FileName = string.Empty,
                    });
                }
            }

            TotalCosts = GetTotalCosts(CalculationAttributeType.All);
            IsCalculated = true;
            RecalculationRequired = false;
        }

        public Task CalculateCostsAsync() => Task.Run(CalculateCosts);

        public double GetTotalCosts(CalculationAttributeType calculationAttributeType = CalculationAttributeType.All)
        {
            try
            {
                IEnumerable<double> costs = (calculationAttributeType == CalculationAttributeType.All ?
                        Costs :
                        Costs
                            .Where(cost => cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value));

                IEnumerable<double> costsMachine = (calculationAttributeType == CalculationAttributeType.All ?
                        OverallPrinterCosts :
                        OverallPrinterCosts
                            .Where(cost => cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value));

                IEnumerable<double> costsMaterial = (calculationAttributeType == CalculationAttributeType.All ?
                        OverallMaterialCosts :
                        // If a specific type is requested
                        OverallMaterialCosts
                            .Where(cost => cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value));

                double total = 0;
                foreach (var cost in costs)
                    total += cost;
                foreach (var cost in costsMachine)
                    total += cost;
                foreach (var cost in costsMaterial)
                    total += cost;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double GetTotalCosts(Guid fileId, CalculationAttributeType calculationAttributeType = CalculationAttributeType.All)
        {
            try
            {
                IEnumerable<double> costs = fileId == Guid.Empty
                    ? (calculationAttributeType == CalculationAttributeType.All ?
                        Costs :
                        Costs
                            .Where(cost => cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value))
                    : (calculationAttributeType == CalculationAttributeType.All ?
                        Costs
                            .Where(cost => cost.FileId == fileId) :
                        Costs
                            .Where(cost => cost.Type == calculationAttributeType && cost.FileId == fileId))
                            .Select(value => Convert.ToDouble(value.Value));

                IEnumerable<double> costsMachine = fileId == Guid.Empty
                    // If file id is empty
                    // If all types are requested
                    ? (calculationAttributeType == CalculationAttributeType.All ?
                        OverallPrinterCosts
                            .Select(a => a)
                        :
                        // If a specific type is requested
                        OverallPrinterCosts
                            .Where(cost =>
                                cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value))
                    // If file id is not empty
                    // If all types are requested
                    : (calculationAttributeType == CalculationAttributeType.All ?
                        OverallPrinterCosts
                            .Where(cost =>
                            cost.FileId == fileId)
                        // If a specific type is requested
                        :
                        OverallPrinterCosts
                            .Where(cost =>
                            cost.Type == calculationAttributeType && cost.FileId == fileId))
                    .Select(value => Convert.ToDouble(value.Value));

                IEnumerable<double> costsMaterial = fileId == Guid.Empty
                   // If file id is empty
                   // If all types are requested
                   ? (calculationAttributeType == CalculationAttributeType.All ?
                        OverallMaterialCosts
                            .Select(a => a)
                        :
                        // If a specific type is requested
                        OverallMaterialCosts
                            .Where(cost =>
                                cost.Type == calculationAttributeType))
                            .Select(value => Convert.ToDouble(value.Value))
                    // If file id is not empty
                    // If all types are requested
                    : (calculationAttributeType == CalculationAttributeType.All ?
                        OverallMaterialCosts
                            .Where(cost =>
                            cost.FileId == fileId)
                        // If a specific type is requested
                        :
                        OverallMaterialCosts
                            .Where(cost =>
                            cost.Type == calculationAttributeType && cost.FileId == fileId))
                    .Select(value => Convert.ToDouble(value.Value));


                double total = 0;
                foreach (var cost in costs)
                    total += cost;
                foreach (var cost in costsMachine)
                    total += cost;
                foreach (var cost in costsMaterial)
                    total += cost;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetTotalQuantity()
        {
            try
            {
                int quantity = PrintInfos
                    .Select(pi => pi.FileUsage)
                    .Select(file => file?.Quantity ?? 0)
                    .ToList()
                    .Sum();
                return quantity;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalPrintTime()
        {
            try
            {
                IEnumerable<double> times = PrintTimes.Select(value => Convert.ToDouble(value?.Value ?? 0));
                double total = 0;
                if (times?.Count() > 0)
                {
                    foreach (double time in times)
                    {
                        total += time;
                    }
                }
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double GetTotalVolume()
        {
            try
            {
                IEnumerable<double> volumes = PrintInfos
                    .Select(f => f.FileUsage?.File)
                    .Select(value => Convert.ToDouble(value?.Volume ?? 0));
                double total = 0;
                if (volumes?.Count() > 0)
                {
                    foreach (double vol in volumes)
                    {
                        total += vol;
                    }
                }
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public double GetTotalMaterialUsed()
        {
            try
            {
                IEnumerable<double> materials = MaterialUsage.Select(value => Convert.ToDouble(value?.Value ?? 0));
                double total = 0;
                if (materials?.Count() > 0)
                {
                    foreach (double material in materials)
                    {
                        total += material;
                    }
                }
                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
    }
}
