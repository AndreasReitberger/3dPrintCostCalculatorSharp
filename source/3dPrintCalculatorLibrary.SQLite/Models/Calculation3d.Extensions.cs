using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using AndreasReitberger.Print3d.Utilities;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class Calculation3d
    {
        #region Methods
        public void CalculateCosts()
        {
            PrintTimes?.Clear();
            MaterialUsage?.Clear();
            OverallMaterialCosts?.Clear();
            OverallPrinterCosts?.Clear();
            Costs?.Clear();
            CombineMaterialCosts = false;

            int quantity = Files.Select(file => file.Quantity).ToList().Sum();
            // Add the handling fee based on the file quantity
            CalculationAttribute? handlingsFee = Rates?.FirstOrDefault(costs => costs.Attribute == "HandlingFee");
            CalculationAttribute? margin = Rates?.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Margin);
            CalculationAttribute? tax = Rates?.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Tax);
            foreach (File3d file in Files)
            {
                double printTime = file.PrintTime * (file.MultiplyPrintTimeWithQuantity ? (file.Quantity * file.PrintTimeQuantityFactor) : 1);
                PrintTimes?.Add(new CalculationAttribute()
                {
                    Attribute = file.FileName,
                    Value = printTime,
                    Type = CalculationAttributeType.Machine,
                    Item = CalculationAttributeItem.Default,
                    FileId = file.Id,
                    FileName = file.FileName,
                });

                if (handlingsFee != null && handlingsFee.Value > 0 && handlingsFee.ApplyPerFile)
                {
                    Costs?.Add(new CalculationAttribute()
                    {
                        Attribute = "HandlingFee",
                        Type = CalculationAttributeType.FixCost,
                        Value = Convert.ToDouble(handlingsFee?.Value * file.Quantity),
                        FileId = file.Id,
                        FileName = file.FileName,
                    });
                }

                if (FailRate > 0)
                {
                    PrintTimes?.Add(new CalculationAttribute()
                    {
                        Attribute = $"{file.FileName}_FailRate",
                        Type = CalculationAttributeType.Machine,
                        Item = CalculationAttributeItem.FailRate,
                        Value = printTime * FailRate / 100,
                        FileId = file.Id,
                        FileName = file.FileName,
                    });
                }

                // Calculate all material costs
                if (Materials?.Count > 0)
                {
                    //Material ??= Materials[0];
                    Material = Materials.FirstOrDefault(material => material.Id == Material?.Id) ?? Materials.FirstOrDefault();
                    //((CONSUMED_MATERIAL[g] x PRICE[$/kg]) / 1000) x QUANTITY x (FAILRATE / 100)

                    // Calculate the total weight for the current file
                    double _weight = 0;
                    if (file.Volume > 0)
                    {
                        double _volume = file.Volume;
                        _weight = _volume * Convert.ToDouble(Material?.Density ?? 1);
                    }
                    else if (file.Weight != null)
                    {
                        _weight = file.Weight.Weight * Convert.ToDouble(UnitFactor.GetUnitFactor(file.Weight.Unit));
                    }
                    // Needed material in g
                    double _material = _weight * file.Quantity;
                    MaterialUsage?.Add(new CalculationAttribute()
                    {
                        Attribute = Material.Name,
                        Value = _material,
                        Type = CalculationAttributeType.Material,
                        Item = CalculationAttributeItem.Default,
                        FileId = file.Id,
                        FileName = file.FileName,
                    });
                    if (FailRate > 0)
                    {
                        MaterialUsage?.Add(new CalculationAttribute()
                        {
                            Attribute = $"{Material.Name}_FailRate",
                            Value = _material * FailRate / 100,
                            Type = CalculationAttributeType.Material,
                            Item = CalculationAttributeItem.FailRate,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }

                    foreach (Material3d material in Materials)
                    {
                        // Set first materials as default
                        Material ??= material;

                        double refreshed = 0;
                        if (ApplyProcedureSpecificAdditions)
                        {
                            if (material.MaterialFamily == Material3dFamily.Powder)
                            {
                                CalculationProcedureAttribute? attribute = ProcedureAttributes.FirstOrDefault(
                                    attr => attr.Attribute == ProcedureAttribute.MaterialRefreshingRatio && attr.Level == CalculationLevel.Material);
                                if (attribute != null)
                                {
                                    CalculationProcedureParameter minPowderNeeded = attribute.Parameters.FirstOrDefault(para => para.Type == ProcedureParameter.MinPowderNeeded);
                                    if (minPowderNeeded != null)
                                    {
                                        double powderInBuildArea = minPowderNeeded.Value;
                                        MaterialAdditions.Material3dProcedureAttribute refreshRatio = material.ProcedureAttributes.FirstOrDefault(ratio => ratio.Attribute == ProcedureAttribute.MaterialRefreshingRatio);
                                        if (refreshRatio != null)
                                        {
                                            // this value is in liter
                                            CalculationAttribute materialPrintObject = MaterialUsage.FirstOrDefault(usage =>
                                                usage.Attribute == material.Name);
                                            if (materialPrintObject != null)
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
                                IEnumerable<ProcedureAddition>? procedureAdditions = ProcedureAdditions?
                                    .Where(addition => addition.TargetFamily == material.MaterialFamily
                                        && addition.Target == ProcedureAdditionTarget.Material
                                        && addition.Enabled
                                        );
                                foreach (ProcedureAddition add in procedureAdditions)
                                {
                                    double costs = add.CalculateCosts();
                                    OverallMaterialCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = material.Id,
                                        Attribute = add.Name,
                                        Type = CalculationAttributeType.ProcedureSpecificAddition,
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
                        foreach (CalculationAttribute materialUsage in MaterialUsage?.Where(mu => mu.FileId == file.Id))
                        {
                            double totalCosts = Convert.ToDouble(materialUsage?.Value * pricePerGramm);
                            OverallMaterialCosts.Add(new CalculationAttribute()
                            {
                                LinkedId = material.Id,
                                // Keep the linking to the currently used material
                                Attribute = materialUsage.Item == CalculationAttributeItem.FailRate ? $"{material.Name}_FailRate" : material.Name,
                                Type = CalculationAttributeType.Material,
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
                                Value = refreshCosts,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }
                }

                // Calculate all machine costs (print time and energy costs)
                if (Printers?.Count > 0)
                {
                    // Check if selected material is still in the collection
                    Printer = Printers.FirstOrDefault(printer => printer.Id == Printer?.Id) ?? Printers.FirstOrDefault();
                    foreach (Printer3d printer in Printers)
                    {
                        Printer ??= printer;
                        foreach (CalculationAttribute pt in PrintTimes?.Where(pt => pt.FileId == file.Id))
                        {
                            // Calculate the machine costs based on the hourly machine rate
                            if (printer?.HourlyMachineRate != null)
                            {
                                double machineHourRate = Convert.ToDouble(printer.HourlyMachineRate.CalcMachineHourRate) * pt.Value;
                                if (machineHourRate > 0)
                                {
                                    OverallPrinterCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Machine,
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
                                double consumption = Convert.ToDouble(((pt?.Value * Convert.ToDouble(printer.PowerConsumption)) / 1000.0)) / 100.0 * Convert.ToDouble(PowerLevel);
                                double totalEnergyCost = consumption * EnergyCostsPerkWh;
                                if (totalEnergyCost > 0)
                                {
                                    OverallPrinterCosts?.Add(new CalculationAttribute()
                                    {
                                        LinkedId = printer.Id,
                                        Attribute = printer.Name,
                                        Type = CalculationAttributeType.Energy,
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
                                            Value = attribute.PerPiece ? parameter.Value * file.Quantity : parameter.Value,
                                            FileId = file.Id,
                                            FileName = file.FileName,
                                        });
                                    }
                                }
                            }

                            // Custom procedure additions
                            if (ProcedureAdditions?.Count > 0)
                            {
                                IEnumerable<ProcedureAddition>? procedureAdditions = ProcedureAdditions?
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
                                        Value = costs,
                                        FileId = file.Id,
                                        FileName = file.FileName,
                                    });
                                }
                            }
                        }
                    }
                }

                // Worksteps
                // Delete once migrated to `WorkstepUsage` instead
                if (WorkSteps?.Count > 0)
                {
                    // Only take the worksteps, which are set as `PerPiece` here
                    foreach (Workstep ws in WorkSteps.Where(ws => ws.CalculationType == CalculationType.PerPiece))
                    {
                        double totalPerPiece = ws.TotalCosts * file.Quantity;
                        Costs?.Add(new CalculationAttribute()
                        {
                            LinkedId = ws.Id,
                            Attribute = ws.Name,
                            Type = CalculationAttributeType.Workstep,
                            Value = totalPerPiece,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                }
                if (WorkstepUsages?.Count > 0)
                {
                    // Only take the worksteps, which are set as `PerPiece` here
                    foreach (WorkstepUsage wsu in WorkstepUsages.Where(wsu => wsu?.Workstep?.CalculationType == CalculationType.PerPiece))
                    {
                        Workstep ws = wsu.Workstep;
                        if (ws is null) continue;
                        double totalPerPiece = wsu.TotalCosts * file.Quantity;
                        Costs?.Add(new CalculationAttribute()
                        {
                            LinkedId = ws.Id,
                            Attribute = ws.Name,
                            Type = CalculationAttributeType.Workstep,
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
                        if (item?.Item == null || file.Id != item.File.Id) continue;

                        double totalPerPiece = (item?.Item?.PricePerPiece ?? 0) * item.Quantity * file.Quantity;
                        Costs?.Add(new CalculationAttribute()
                        {
                            LinkedId = item.Id,
                            Attribute = item.Item.Name,
                            Type = CalculationAttributeType.AdditionalItem,
                            Value = totalPerPiece,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                }

                // Custom additions before adding the margin
                List<CustomAddition> customAdditionsBeforeMargin = CustomAdditions
                    .Where(addition => addition.CalculationType == CustomAdditionCalculationType.BeforeApplingMargin)
                    .ToList();

                if (customAdditionsBeforeMargin?.Count > 0)
                {
                    SortedDictionary<int, double> additions = new();
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
                            Value = pairs.Value * costsSoFar / 100.0,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                }

                //Margin
                if (margin != null && !margin.SkipForCalculation)
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
                    List<CalculationAttribute> skipMarginCalculation = Rates.Where(rate => rate.SkipForMargin).ToList();
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
                    SortedDictionary<int, double> additions = new();
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
                                Value = pairs.Value * costsSoFar / 100.0,
                                FileId = file.Id,
                                FileName = file.FileName,
                            });
                        }
                    }
                }

                //Tax
                //CalculationAttribute? Tax = Rates?.FirstOrDefault(costs => costs.Type == CalculationAttributeType.Tax);
                if (tax != null && !tax.SkipForCalculation)
                {
                    double costsSoFar = GetTotalCosts(file.Id);
                    double taxValue = costsSoFar * tax.Value / (tax.IsPercentageValue ? 100.0 : 1.0);
                    if (taxValue > 0)
                    {
                        Costs.Add(new CalculationAttribute()
                        {
                            Attribute = "Tax",
                            Type = CalculationAttributeType.Tax,
                            Value = taxValue,
                            FileId = file.Id,
                            FileName = file.FileName,
                        });
                    }
                }
            }

            // If the handling fee is not set per file, add it once afterwards
            if (handlingsFee != null && handlingsFee.Value > 0 && !handlingsFee.ApplyPerFile)
            {
                Costs?.Add(new CalculationAttribute()
                {
                    Attribute = "HandlingFee",
                    Type = CalculationAttributeType.FixCost,
                    Value = handlingsFee.Value,
                    FileId = Guid.Empty,
                    FileName = string.Empty,
                });
                if (handlingsFee?.SkipForMargin == false)
                {
                    double marginValue = handlingsFee.Value * margin.Value / (margin.IsPercentageValue ? 100.0 : 1.0);
                    if (marginValue > 0)
                    {
                        Costs?.Add(new CalculationAttribute()
                        {
                            Attribute = "Margin",
                            Type = CalculationAttributeType.Margin,
                            Value = marginValue,
                            FileId = Guid.Empty,
                            FileName = string.Empty,
                        });
                    }
                }
            }

            // Worksteps
            // Delete once migrated to `WorkstepUsage` instead
            if (WorkSteps?.Count > 0)
            {
                foreach (Workstep ws in WorkSteps.Where(ws => ws.CalculationType != CalculationType.PerPiece))
                {
                    switch (ws.CalculationType)
                    {
                        case CalculationType.PerHour:
                            WorkstepDuration workstepDuration = WorkStepDurations.FirstOrDefault(wsd => wsd.WorkstepId == ws.Id);
                            if (workstepDuration != null)
                            {
                                ws.Duration = workstepDuration.Duration;
                            }
                            double totalPerHour = ws.TotalCosts;
                            //double totalPerHour = Convert.ToDouble(ws.Duration) * Convert.ToDouble(ws.Price);
                            if (totalPerHour > 0)
                            {
                                Costs?.Add(new CalculationAttribute()
                                {
                                    LinkedId = ws.Id,
                                    Attribute = ws.Name,
                                    Type = CalculationAttributeType.Workstep,
                                    Value = totalPerHour,
                                });
                            }
                            break;
                        case CalculationType.PerJob:
                            double totalPerJob = ws.TotalCosts;
                            //double totalPerJob = Convert.ToDouble(ws.Price);
                            Costs?.Add(new CalculationAttribute()
                            {
                                LinkedId = ws.Id,
                                Attribute = ws.Name,
                                Type = CalculationAttributeType.Workstep,
                                Value = totalPerJob,
                            });
                            break;
                        case CalculationType.PerPiece:
                            // Nothing to do here
                            break;
                    }
                }
            }
            if (WorkstepUsages?.Count > 0)
            {
                foreach (WorkstepUsage wsu in WorkstepUsages.Where(wsu => wsu?.Workstep?.CalculationType != CalculationType.PerPiece))
                {
                    Workstep ws = wsu.Workstep;
                    if (ws is null) continue;
                    double totalPerJob = wsu.TotalCosts;
                    Costs?.Add(new CalculationAttribute()
                    {
                        LinkedId = ws.Id,
                        Attribute = ws.Name,
                        Type = CalculationAttributeType.Workstep,
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

                    double totalPerPiece = (item?.Item?.PricePerPiece ?? 0) * item.Quantity;
                    Costs?.Add(new CalculationAttribute()
                    {
                        LinkedId = item.Id,
                        Attribute = item.Item.Name,
                        Type = CalculationAttributeType.AdditionalItem,
                        Value = totalPerPiece,
                    });
                }
            }

            if (ApplyProcedureSpecificAdditions)
            {
                List<CalculationProcedureAttribute> multiMaterialAttributes = ProcedureAttributes
                    .Where(attr => attr.Family == Procedure && attr.Level == CalculationLevel.Calculation).ToList();
                for (int i = 0; i < multiMaterialAttributes?.Count; i++)
                {
                    CalculationProcedureAttribute attribute = multiMaterialAttributes[i];
                    for (int j = 0; j < attribute.Parameters.Count; j++)
                    {
                        CalculationProcedureParameter parameter = attribute.Parameters[j];
                        switch (parameter.Type)
                        {
                            case ProcedureParameter.MultiMaterialCalculation:
                                if (Printer?.MaterialType == Material3dFamily.Filament)
                                {
                                    CombineMaterialCosts = parameter.Value > 0;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

                // Custom additions
                if (ProcedureAdditions?.Count > 0)
                {
                    IEnumerable<ProcedureAddition>? procedureAdditions = ProcedureAdditions?
                        .Where(addition => addition.TargetFamily == Procedure
                            && addition.Target == ProcedureAdditionTarget.General
                            && addition.Enabled
                            );
                    foreach (ProcedureAddition add in procedureAdditions)
                    {
                        double costs = add.CalculateCosts();
                        Costs?.Add(new CalculationAttribute()
                        {
                            LinkedId = Guid.Empty,
                            Attribute = add.Name,
                            Type = CalculationAttributeType.ProcedureSpecificAddition,
                            Value = costs,
                        });
                    }
                }
            }

            // Add the tax for the unlinked file costs
            if (tax != null && !tax.SkipForCalculation)
            {
                IEnumerable<CalculationAttribute> unlinkedCosts = Costs.Where(c => c.FileId == Guid.Empty);
                double costsSoFar = unlinkedCosts?.Sum(cost => cost.Value) ?? 0;

                double taxValue = costsSoFar * tax.Value / (tax.IsPercentageValue ? 100.0 : 1.0);
                if (taxValue > 0)
                {
                    Costs.Add(new CalculationAttribute()
                    {
                        Attribute = "Tax",
                        Type = CalculationAttributeType.Tax,
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
                            .Where(cost =>
                                cost.Attribute == Printer.Name ||
                                cost.LinkedId == Printer.Id)
                        :
                        // If a specific type is requested
                        OverallPrinterCosts
                            .Where(cost =>
                                cost.Type == calculationAttributeType &&
                                (cost.Attribute == Printer.Name ||
                                cost.LinkedId == Printer.Id)))
                            .Select(value => Convert.ToDouble(value.Value))
                    // If file id is not empty
                    // If all types are requested
                    : (calculationAttributeType == CalculationAttributeType.All ?
                        OverallPrinterCosts
                            .Where(cost =>
                            cost.FileId == fileId &&
                            (cost.Attribute == Printer.Name ||
                            cost.LinkedId == Printer.Id))

                        // If a specific type is requested
                        :
                        OverallPrinterCosts
                            .Where(cost =>
                            cost.Type == calculationAttributeType && cost.FileId == fileId &&
                            (cost.Attribute == Printer.Name || cost.LinkedId == Printer.Id)
                            ))
                    .Select(value => Convert.ToDouble(value.Value));

                IEnumerable<double> costsMaterial = fileId == Guid.Empty
                   // If file id is empty
                   // If all types are requested
                   ? (calculationAttributeType == CalculationAttributeType.All ?
                        OverallMaterialCosts
                            .Where(cost =>
                                cost.Attribute == Material.Name ||
                                (CombineMaterialCosts || (cost.LinkedId == Material.Id)))
                        :
                        // If a specific type is requested
                        OverallMaterialCosts
                            .Where(cost =>
                                cost.Type == calculationAttributeType && (
                                cost.Attribute == Material.Name ||
                                (CombineMaterialCosts || (cost.LinkedId == Material.Id)))))
                            .Select(value => Convert.ToDouble(value.Value))
                    // If file id is not empty
                    // If all types are requested
                    : (calculationAttributeType == CalculationAttributeType.All ?
                        OverallMaterialCosts
                            .Where(cost =>
                            cost.FileId == fileId &&
                            (cost.Attribute == Material.Name ||
                            (CombineMaterialCosts || (cost.LinkedId == Material.Id))))

                        // If a specific type is requested
                        :
                        OverallMaterialCosts
                            .Where(cost =>
                            cost.Type == calculationAttributeType && cost.FileId == fileId &&
                            (cost.Attribute == Material.Name ||
                            (CombineMaterialCosts || (cost.LinkedId == Material.Id)))))
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

        public double GetTotalCosts(CalculationAttributeType calculationAttributeType = CalculationAttributeType.All) => GetTotalCosts(Guid.Empty, calculationAttributeType);

        public int GetTotalQuantity()
        {
            try
            {
                int quantity = Files.Select(file => file.Quantity).ToList().Sum();
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
                IEnumerable<double> times = PrintTimes?.Select(value => Convert.ToDouble(value?.Value ?? 0));
                double total = 0;
                foreach (double time in times)
                {
                    total += time;
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
                IEnumerable<double> volumes = Files?.Select(value => Convert.ToDouble(value?.Volume ?? 0));
                double total = 0;
                foreach (double vol in volumes)
                {
                    total += vol;
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
                IEnumerable<double> materials = MaterialUsage?.Select(value => Convert.ToDouble(value?.Value ?? 0));
                double total = 0;
                foreach (double material in materials)
                {
                    total += material;
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
