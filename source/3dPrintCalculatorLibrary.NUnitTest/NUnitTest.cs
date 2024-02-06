using AndreasReitberger.Print3d;
using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using AndreasReitberger.Print3d.ProcedureAdditions;
using NUnit.Framework;

namespace AndreasReitberger.NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private Calculation3d _calculation;
        [Test]
        public void TestCalculation()
        {
            try
            {
                Material3d material = new()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogram,
                    PackageSize = 1,
                    UnitPrice = 30,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PETG",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    Width = 25,
                    Height = 21,
                    Depth = 21,
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                Item3d item = new()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = new()
                    {
                        Name = "Würth"
                    },
                    SKU = "2302423-1223"
                };
                Item3dUsage usage = new()
                {
                    Item = item,
                    Quantity = 30,
                    LinkedToFile = false,
                };

                _calculation = new();
                // Add data
                _calculation.AdditionalItems.Add(usage);
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);
                _calculation.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Tax,
                    IsPercentageValue = true,
                    Value = 19,
                });
                _calculation.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Margin,
                    IsPercentageValue = true,
                    Value = 100,
                });

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();
                double totalCosts = _calculation.TotalCosts;
                Assert.That(_calculation.IsCalculated);

                List<double> costsCalc = new()
                {
                    _calculation.MachineCosts,
                    _calculation.MaterialCosts,
                    _calculation.CalculatedMargin,
                    _calculation.CalculatedTax,
                    _calculation.ItemsCosts,
                };
                double summedCalc = costsCalc.Sum();
                Assert.That(Math.Round(summedCalc, 2) == Math.Round(_calculation.TotalCosts, 2));


                Calculation3d _calculation2 = _calculation?.Clone() as Calculation3d;
                _calculation2.CalculateCosts();
                Assert.That(_calculation2.IsCalculated);
                Assert.That(totalCosts == _calculation2.TotalCosts);

                costsCalc = new()
                {
                    _calculation2.MachineCosts,
                    _calculation2.MaterialCosts,
                    _calculation2.CalculatedMargin,
                    _calculation2.CalculatedTax,
                    _calculation2.ItemsCosts,
                };
                summedCalc = costsCalc.Sum();
                Assert.That(Math.Round(summedCalc, 2) == Math.Round(_calculation2.TotalCosts, 2));

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void ExportCalculation()
        {
            try
            {
                Material3d material = new Material3d
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogram,
                    PackageSize = 1,
                    UnitPrice = 30,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PETG",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    Height = 25,
                    Width = 21,
                    Depth = 21,
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();
#if NETFRAMEWORK
                Calculator3dExporter.Save(_calculation, @"mycalc.3dcx");
                Calculator3dExporter.Load(@"mycalc.3dcx", out Calculation3d calculation);
                Assert.That(calculation != null);
#endif
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void TestCalculationEdit()
        {
            try
            {
                Material3d material = new Material3d()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogram,
                    PackageSize = 1,
                    UnitPrice = 30,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PETG",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                Printer3d printerSla = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "SL1 Speed",
                    Price = 1799,
                    MaterialType = Material3dFamily.Resin,
                    Type = Printer3dType.SLA,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.ApplyProcedureSpecificAdditions = true;
                // Needed if the calculation is reloaded later
                List<CalculationProcedureParameterAddition> additionalInfo = new()
                {
                    new CalculationProcedureParameterAddition("replacementcosts", 20),
                    new CalculationProcedureParameterAddition( "wearfactor", 0.5 ),
                };

                //List<CalculationProcedureParameter> paramters = new List<CalculationProcedureParameter>();
                List<CalculationProcedureParameter> parameters = new List<CalculationProcedureParameter>
                {
                    new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.NozzleWearCosts,
                        Value = 2,
                        Additions = additionalInfo,
                    }
                };

                _calculation.ProcedureAttributes.Add(
                    new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.NozzleWear,
                        Family = Material3dFamily.Filament,
                        Parameters = parameters,
                        Level = CalculationLevel.Printer,
                    }
                );

                _calculation.CalculateCosts();

                CalculationAttribute wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.That(wearCostAttribute is not null);

                // Updat calculation
                _calculation.Printer = null;
                _calculation.Printers = new List<Printer3d>
                {
                    printerSla
                };

                _calculation.CalculateCosts();
                wearCostAttribute = _calculation.OverallPrinterCosts.FirstOrDefault(item =>
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.That(wearCostAttribute is null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        Calculation3d GetTestCalculation()
        {
            Material3d material = new()
            {
                Id = Guid.NewGuid(),
                Density = 1.24,
                Name = "Test Material",
                Unit = Unit.Kilogram,
                PackageSize = 1,
                UnitPrice = 30,
                TypeOfMaterial = new Material3dType()
                {
                    Id = Guid.NewGuid(),
                    Material = "PETG",
                    Polymer = "",
                    Family = Material3dFamily.Filament,
                }
            };
            Material3d material2 = new()
            {
                Id = Guid.NewGuid(),
                Density = 1.24,
                Name = "Test Material",
                Unit = Unit.Liters,
                PackageSize = 1,
                UnitPrice = 59,
                TypeOfMaterial = new Material3dType()
                {
                    Id = Guid.NewGuid(),
                    Material = "Tough",
                    Polymer = "",
                    Family = Material3dFamily.Resin,
                }
            };
            Printer3d printer = new()
            {
                Id = Guid.NewGuid(),
                Manufacturer = new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Name = "Prusa"
                },
                Model = "XL MK1",
                Price = 799,
                MaterialType = Material3dFamily.Filament,
                Type = Printer3dType.FDM,
                PowerConsumption = 210,
            };
            Printer3d printer2 = new()
            {
                Id = Guid.NewGuid(),
                Manufacturer = new Manufacturer()
                {
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Name = "Prusa"
                },
                Model = "SL1",
                Price = 1299,
                MaterialType = Material3dFamily.Resin,
                Type = Printer3dType.SLA,
                PowerConsumption = 164,
            };
            File3d file = new()
            {
                Id = Guid.NewGuid(),
                PrintTime = 10.25,
                Volume = 12.36,
                Quantity = 3,
            };
            File3d file2 = new()
            {
                Id = Guid.NewGuid(),
                PrintTime = 10.25,
                Volume = 12.36,
                Quantity = 3,
                MultiplyPrintTimeWithQuantity = false
            };

            Item3d item = new()
            {
                Name = "Nuts M3",
                PackageSize = 100,
                PackagePrice = 9.99d,
                Manufacturer = new()
                {
                    Name = "Würth"
                },
                SKU = "2302423-1223"
            };
            Item3dUsage usage = new()
            {
                Item = item,
                Quantity = 30,
                LinkedToFile = false,
            };

            _calculation = new Calculation3d();
            // Add data
            _calculation.AdditionalItems.Add(usage);
            _calculation.Files.Add(file);
            _calculation.Files.Add(file2);
            _calculation.Printers.Add(printer);
            _calculation.Printers.Add(printer2);
            _calculation.Materials.Add(material);
            _calculation.Materials.Add(material2);
            _calculation.ProcedureAdditions.Add(new ProcedureAddition()
            {
                Name = "Resin Tank Replacement",
                Description = "Take the costs for the resin tank replacement into account?",
                Enabled = true,
                Target = ProcedureAdditionTarget.Machine,
                TargetFamily = Material3dFamily.Resin,
                Parameters = new()
                {
                    new ProcedureCalculationParameter()
                    {
                        Name = "Tank replacement costs",
                        Type = ProcedureCalculationType.ReplacementCosts,
                        Price = 50,
                        WearFactor = 1000,
                        QuantityInPackage = 1,
                    }
                }
            });
            _calculation.ProcedureAdditions.Add(new ProcedureAddition()
            {
                Name = "Gloves",
                Description = "Take the costs for the gloves?",
                Enabled = true,
                Target = ProcedureAdditionTarget.General,
                TargetFamily = Material3dFamily.Resin,
                Parameters = new()
                {
                    new ProcedureCalculationParameter()
                    {
                        Name = "Gloves costs",
                        Type = ProcedureCalculationType.ConsumableGoods,
                        Price = 50,
                        AmountTakenForCalculation = 2,
                        QuantityInPackage = 100,
                    }
                }
            });

            // Add information
            _calculation.FailRate = 25;
            _calculation.EnergyCostsPerkWh = 0.30;
            _calculation.ApplyEnergyCost = true;
            // Uses 75% of the max. power consumption set in the printer model (210 Watt)
            _calculation.PowerLevel = 75;

            _calculation.CalculateCosts();
            return _calculation;
        }

        [Test]
        public void MultiFileDifferTest()
        {
            try
            {
                Material3d material = new Material3d()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.24,
                    Name = "Test Material",
                    Unit = Unit.Kilogram,
                    PackageSize = 1,
                    UnitPrice = 30,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PETG",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Material3d material2 = new Material3d()
                {
                    Id = Guid.NewGuid(),
                    Density = 1.14,
                    Name = "Test Material #2",
                    Unit = Unit.Kilogram,
                    PackageSize = 1,
                    UnitPrice = 25,
                    TypeOfMaterial = new Material3dType()
                    {
                        Id = Guid.NewGuid(),
                        Material = "PLA",
                        Polymer = "",
                        Family = Material3dFamily.Filament,
                    }
                };
                Printer3d printer = new Printer3d()
                {
                    Id = Guid.NewGuid(),
                    Manufacturer = new Manufacturer()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    },
                    Model = "XL MK1",
                    Price = 799,
                    MaterialType = Material3dFamily.Filament,
                    Type = Printer3dType.FDM,
                    PowerConsumption = 210,
                };
                File3d file = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyFirst.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MySecond.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };
                File3d file3 = new File3d()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyThird.gcode",
                    PrintTime = 2.25,
                    Volume = 3.36,
                    Quantity = 25,
                    MultiplyPrintTimeWithQuantity = true
                };

                _calculation = new Calculation3d();
                // Add data
                _calculation.Files.Add(file);
                _calculation.Files.Add(file2);
                _calculation.Files.Add(file3);
                _calculation.Printers.Add(printer);
                _calculation.Materials.Add(material);
                _calculation.Materials.Add(material2);

                // Add information
                _calculation.FailRate = 25;
                _calculation.EnergyCostsPerkWh = 0.30;
                _calculation.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                _calculation.PowerLevel = 75;

                _calculation.CalculateCosts();

                double total = _calculation.GetTotalCosts();

                var materialCosts = PrintCalculator3d.GetMaterialCosts(_calculation);
                var machineCosts = PrintCalculator3d.GetMachineCosts(_calculation);

                _calculation.DifferFileCosts = true;
                _calculation.CalculateCosts();

                double totalDiffer = _calculation.GetTotalCosts();

                Assert.That(_calculation.IsCalculated);
                Assert.That(total == totalDiffer);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void Item3dTests()
        {
            try
            {
                Manufacturer wuerth = new()
                {
                    Name = "Würth",
                    DebitorNumber = "DE26265126",
                    Website = "https://www.wuerth.de/",
                };

                Item3d item1 = new()
                {
                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-1223"
                };
                Item3d item2 = new()
                {
                    Name = "Screws M3 20mm",
                    PackageSize = 50,
                    PackagePrice = 14.99d,
                    Manufacturer = wuerth,
                    SKU = "2302423-6413"
                };

                Assert.That(item1 is not null);
                Assert.That(item2 is not null);
                Assert.That(item1?.Manufacturer is not null);
                Assert.That(item2?.Manufacturer is not null);

                Item3dUsage usage = new()
                {
                    Item = item1,
                    Quantity = 30,
                    LinkedToFile = false,
                };
                Assert.That(usage is not null);
                Assert.That(usage.Item is not null);

                var manufacturerLoaded = usage?.Item?.Manufacturer;
                Assert.That(manufacturerLoaded is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void WorkstepTests()
        {
            try
            {
                WorkstepCategory category = new()
                {
                    Name = "Construction"
                };
                Workstep ws = new()
                {
                    Name = "3D CAD",
                    Category = category,
                    Price = 30,
                };
                WorkstepUsage usage = new()
                {
                    Workstep = ws,
                    UsageParameter = new()
                    {
                        ParameterType = WorkstepUsageParameterType.Duration,
                        Value = 1.5d
                    }
                };

                Assert.That(usage is not null);
                Assert.That(usage?.UsageParameter is not null);
                Assert.That(usage?.Workstep is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void StorageTest()
        {
            try
            {
                double startAmount = 2.68;
                Print3d.Models.Material3d material = new()
                {
                    Name = "Test",
                    SKU = "Some material number",
                    PackageSize = 1,
                    Unit = Unit.Kilogram,
                    UnitPrice = 29.99,
                    PriceIncludesTax = true,
                };
                Print3d.Models.StorageAdditions.Storage3dItem item = new()
                {
                    Material = material,
                    Amount = startAmount,
                };

                Print3d.Models.StorageAdditions.Storage3dLocation location = new()
                {
                    Location = "LR-01-01-01",
                    Capacity = 32,
                };
                location.Items.Add(item);
                Print3d.Models.Storage3d storage = new()
                {
                    Name = "Main material storage",
                };
                storage.Locations.Add(location);

                location.AddToStock(material, 750, Unit.Gram);
                var newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                // Check if the addition was successfully
                Assert.That(newItem?.Amount == startAmount + 0.75);

                // Just to check if the unit conversion is working
                location.TakeFromStock(material, 0.001, Unit.MetricTons, false);
                newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                // Check if the addition was successfully
                Assert.That(newItem?.Amount == startAmount + 0.75 - 1);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void TakeCalculationAmountFromStorageTest()
        {
            try
            {
                Calculation3d calculation = GetTestCalculation();
                Assert.That(calculation is not null);

                foreach (var material in calculation.Materials)
                {
                    Print3d.Models.StorageAdditions.Storage3dItem item = new()
                    {
                        Material = material,
                    };
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void ProcedureSpecificAdditionsTest()
        {
            try
            {
                // Hardware replacement costs
                ProcedureAddition resinTank = new()
                {
                    Name = "Resin Tank Replacement",
                    Description = "Take the costs for the resin tank replacement into account?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.Machine,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters = new()
                    {
                        new ProcedureCalculationParameter()
                        {
                            Name = "Tank replacement costs",
                            Type = ProcedureCalculationType.ReplacementCosts,
                            Price = 50,
                            WearFactor = 1,
                            QuantityInPackage = 1,
                        }
                    }
                };
                double resinWearCosts = resinTank.CalculateCosts();
                Assert.That(resinWearCosts == 0.5d);
                // Consumable goods (like filters and gloves)
                ProcedureAddition gloves = new()
                {
                    Name = "Gloves",
                    Description = "Take the costs for the gloves?",
                    Enabled = true,
                    Target = ProcedureAdditionTarget.General,
                    TargetFamily = Material3dFamily.Resin,
                    Parameters = new()
                    {
                        new ProcedureCalculationParameter()
                        {
                            Name = "Gloves costs",
                            Type = ProcedureCalculationType.ConsumableGoods,
                            Price = 50,
                            AmountTakenForCalculation = 2,
                            QuantityInPackage = 100,
                        }
                    }
                };
                double glovesCosts = gloves.CalculateCosts();
                Assert.That(glovesCosts == 1d);

                var calculation = GetTestCalculation();
                calculation.Procedure = Material3dFamily.Resin;
                calculation.ApplyProcedureSpecificAdditions = true;
                calculation.CalculateCosts();

                Assert.That(calculation.OverallPrinterCosts?.FirstOrDefault(cost => cost.Attribute == "Resin Tank Replacement") is not null);
                Assert.That(calculation.Costs?.FirstOrDefault(cost => cost.Attribute == "Gloves") is not null);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}