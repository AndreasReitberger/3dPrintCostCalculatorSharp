using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Models;
using AndreasReitberger.Print3d.Models.CalculationAdditions;
using AndreasReitberger.Print3d.Models.CustomerAdditions;
using AndreasReitberger.Print3d.Models.MaterialAdditions;
using AndreasReitberger.Print3d.Models.WorkstepAdditions;
using AndreasReitberger.Print3d.Models.FileAdditions;
using AndreasReitberger.Print3d.ProcedureAdditions;
using NUnit.Framework;

namespace AndreasReitberger.NUnitTest
{
    public class Tests
    {
        bool ApplyResinGlovesCosts = true;
        bool ApplyResinWashingCosts = true;
        bool ApplyResinFilterCosts = true;
        bool ApplyResinTankWearCosts = true;

        Calculation3dEnhanced? calculation;

        [SetUp]
        public void Setup()
        {
            Printer3d printerFDM = new()
            {
                Type = Printer3dType.FDM,
                Model = "i3 MK3S",
                PowerConsumption = 210,
                Price = 899,
                MaterialType = Material3dFamily.Filament,
            };
            Printer3d printerDLP = new()
            {
                Type = Printer3dType.DLP,
                Model = "Photon X",
                PowerConsumption = 160,
                Price = 599,
                MaterialType = Material3dFamily.Resin,
            };

            calculation = new Calculation3dEnhanced()
            {
                Name = "Test",
                Customer = new Customer3d()
                {
                    Name = "Max",
                    LastName = "Mustermann",
                    IsCompany = false,
                    EmailAddresses =
                    [
                        new Email()
                        {
                            EmailAddress = "max.mustermann@example.com"
                        },
                    ],
                    Addresses = [
                        new Address()
                        {
                            City = "Muserstadt",
                            Zip = "93426",
                            CountryCode = "de",
                            Street = "Musterstra�e 4",
                        }
                    ],
                },
                PrintInfos = [
                    // FDM job
                    new Print3dInfo()
                    {
                        Name = "My awesome print job",
                        FileUsage = new File3dUsage()
                        {
                            File = new File3d()
                            {
                                FileName = "my.gcode",
                                PrintTime = 5.263,
                                Volume = 35.54,
                            },
                            Quantity = 5,
                        },
                        MaterialUsages = [
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Prusament PETG 1kg JetBlack",
                                    Density = 1.24,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Filament,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Filament, Material = "PETG" },
                                    PackageSize = 1,
                                    Unit = Unit.Kilogram,
                                    UnitPrice = 30,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Prusa",
                                    }
                                },
                                PercentageValue = 0.5,
                            },
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Prusament PLA 1kg JetBlack",
                                    Density = 1.24,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Filament,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Filament, Material = "PLA" },
                                    PackageSize = 1,
                                    Unit = Unit.Kilogram,
                                    UnitPrice = 25,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Prusa",
                                    }
                                },
                                PercentageValue = 0.5,
                            }
                        ],
                        Printer = printerFDM,
                        Items = [
                            new Item3dUsage()
                            {
                                Item = new Item3d()
                                {
                                    Name = "M3x16 Screws",
                                    PackagePrice = 20,
                                    PackageSize = 100,
                                },
                                Quantity = 10,
                            },
                        ]
                    },
                    // Resing job
                    new Print3dInfo()
                    {
                        Name = "My first resin print job",
                        FileUsage = new File3dUsage()
                        {
                            File = new File3d()
                            {
                                FileName = "Batman.dlp",
                                PrintTime = 2.65,
                                Volume = 65.546,
                            },
                            Quantity = 20,
                        },
                        MaterialUsages = [
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Anycubic Tough Resin",
                                    Density = 1.11,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Resin,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Resin, Material = "TOUGH" },
                                    PackageSize = 1,
                                    Unit = Unit.Liters,
                                    UnitPrice = 65,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Anycubic",
                                    }
                                },
                                PercentageValue = 1,
                            }
                        ],
                        Printer = printerDLP,
                    },
                    new Print3dInfo()
                    {
                        Name = "My first resin print job",
                        FileUsage = new File3dUsage()
                        {
                            File = new File3d()
                            {
                                FileName = "Superman.dlp",
                                PrintTime = 1.42,
                                Volume = 35.4536,
                            },
                            Quantity = 3,
                        },
                        MaterialUsages = [
                            new Material3dUsage()
                            {
                                Material = new Material3d()
                                {
                                    Name = "Anycubic Tough Resin",
                                    Density = 1.11,
                                    ColorCode = "#000000",
                                    MaterialFamily = Material3dFamily.Resin,
                                    TypeOfMaterial = new Material3dType() { Family = Material3dFamily.Resin, Material = "TOUGH" },
                                    PackageSize = 1,
                                    Unit = Unit.Liters,
                                    UnitPrice = 65,
                                    Manufacturer = new Manufacturer()
                                    {
                                        Name = "Anycubic",
                                    }
                                },
                                PercentageValue = 1,
                            }
                        ],
                        Printer = printerDLP,
                    }
                ],
                WorkstepUsages = [
                    new WorkstepUsage()
                    {
                        Workstep = new Workstep()
                        {
                            Name = "Cleanup",
                            CalculationType = CalculationType.PerPiece,
                            Category = new WorkstepCategory() { Name = "Post-Processing" },
                            Price = 0.5,
                            Note = "Contains the removal of supports and general cleanup",
                            Type = WorkstepType.Post,
                        },
                        UsageParameter = new WorkstepUsageParameter()
                        {
                            ParameterType = WorkstepUsageParameterType.Quantity,
                            Value = 1,
                        }
                    },
                    new WorkstepUsage()
                    {
                        Workstep = new Workstep()
                        {
                            Name = "Packaging",
                            CalculationType = CalculationType.PerJob,
                            Category = new WorkstepCategory() { Name = "Post-Processing" },
                            Price = 1,
                            Note = "Contains the packaging of the prints",
                            Type = WorkstepType.Post,
                        },
                        UsageParameter = new WorkstepUsageParameter()
                        {
                            ParameterType = WorkstepUsageParameterType.Quantity,
                            Value = 1,
                        }
                    }
                    ],
                ApplyEnergyCost = true,
                PowerLevel = 75,
                EnergyCostsPerkWh = 0.4,
                AdditionalItems = [
                    new Item3dUsage()
                    {
                        Item = new Item3d()
                        {

                            Name = "Nuts M3",
                            PackageSize = 100,
                            PackagePrice = 9.99d,
                            Manufacturer = new Manufacturer()
                            {
                                Name = "W�rth",
                                DebitorNumber = "DE26265126",
                                Website = "https://www.wuerth.de/",
                            },
                            SKU = "2302423-1223"
                        },
                        Quantity = 2,
                    },
                    new Item3dUsage()
                    {
                        Item = new Item3d()
                        {
                            Name = "Screws M3 20mm",
                            PackageSize = 50,
                            PackagePrice = 14.99d,
                            Manufacturer = new Manufacturer()
                            {
                                Name = "W�rth",
                                DebitorNumber = "DE26265126",
                                Website = "https://www.wuerth.de/",
                            },
                            SKU = "2302423-6413"
                        },
                        Quantity = 2,
                    }
                    ],
                FailRate = 5,
                State = CalculationState.Draft,
                ApplyProcedureSpecificAdditions = true,
            };

            List<CalculationProcedureParameter> parameters = [];
            List<CalculationProcedureParameterAddition> additionalInfo = [];

            if (ApplyResinGlovesCosts)
            {
                if (true)
                {
                    // Needed if the calculation is reloaded later
                    additionalInfo =
                    [
                        new CalculationProcedureParameterAddition("amount", 100),
                        new CalculationProcedureParameterAddition("price", 25),
                        new CalculationProcedureParameterAddition("perjob", 2),
                    ];

                    parameters =
                    [
                        new CalculationProcedureParameter()
                        {
                            Type = ProcedureParameter.GloveCosts,
                            Value = 2d * (25d / 100d),
                            Additions = additionalInfo,
                        }
                    ];
                    calculation.ProcedureAttributes.Add(
                        new CalculationProcedureAttribute()
                        {
                            Attribute = ProcedureAttribute.GlovesCosts,
                            Family = Material3dFamily.Resin,
                            Parameters = parameters,
                            Level = CalculationLevel.Printer,
                            PerFile = false,
                            PerPiece = false,
                        });
                }
            }
            if (ApplyResinWashingCosts)
            {
                if (true)
                {
                    // Needed if the calculation is reloaded later
                    additionalInfo =
                    [
                        new CalculationProcedureParameterAddition("amount", 5),
                        new CalculationProcedureParameterAddition("price", 50),
                        new CalculationProcedureParameterAddition("perjob", 0.1d),
                    ];

                    parameters =
                    [
                        new CalculationProcedureParameter()
                        {
                            Type = ProcedureParameter.WashingCosts,
                            Value = 0.1d * (50d / 5d),
                            Additions = additionalInfo,
                        }
                    ];
                    calculation.ProcedureAttributes.Add(
                        new CalculationProcedureAttribute()
                        {
                            Attribute = ProcedureAttribute.WashingCosts,
                            Family = Material3dFamily.Resin,
                            Parameters = parameters,
                            Level = CalculationLevel.Printer,
                            PerPiece = true,
                            PerFile = true,
                        });
                }
            }
            if (ApplyResinFilterCosts)
            {
                if (true)
                {
                    // Needed if the calculation is reloaded later
                    additionalInfo =
                    [
                        new CalculationProcedureParameterAddition("amount", 50),
                        new CalculationProcedureParameterAddition("price", 25),
                        new CalculationProcedureParameterAddition("perjob", 1),
                    ];

                    parameters =
                    [
                        new CalculationProcedureParameter()
                        {
                            Type = ProcedureParameter.FilterCosts,
                            Value = 1d * (25d / 50d),
                            Additions = additionalInfo,
                        }
                    ];
                    calculation.ProcedureAttributes.Add(
                        new CalculationProcedureAttribute()
                        {
                            Attribute = ProcedureAttribute.FilterCosts,
                            Family = Material3dFamily.Resin,
                            Parameters = parameters,
                            Level = CalculationLevel.Printer,
                            PerFile = true,
                            PerPiece = false,
                        });
                }
            }
            if (ApplyResinTankWearCosts)
            {
                if (true)
                {
                    // Needed if the calculation is reloaded later
                    additionalInfo =
                    [
                            new CalculationProcedureParameterAddition("replacementcosts", 120),
                        new CalculationProcedureParameterAddition("wearfactor", 0.01d)
                    ];
                    parameters =
                    [
                        new CalculationProcedureParameter()
                        {
                            Type = ProcedureParameter.ResinTankWearCosts,
                            Value = 120d * 0.01d,
                            Additions = additionalInfo,

                        }
                    ];
                    calculation.ProcedureAttributes.Add(
                        new CalculationProcedureAttribute()
                        {
                            Attribute = ProcedureAttribute.ResinTankWear,
                            Family = Material3dFamily.Resin,
                            Parameters = parameters,
                            Level = CalculationLevel.Printer,
                            PerPiece = false,
                            PerFile = true,
                        });
                }
            }
        }

        [Test]
        public void TestProcedureSpecificAddition()
        {
            try
            {
                Assert.That(calculation is not null);
                Assert.That(calculation?.ProcedureAttributes?.Count > 0);
                calculation.ApplyProcedureSpecificAdditions = false;
                calculation.CalculateCosts();
                var total = calculation.TotalCosts;

                calculation.ApplyProcedureSpecificAdditions = true;
                calculation.CalculateCosts();
                var total2 = calculation.TotalCosts;
                Assert.That(total < total2);

                /*
                 * GloveCosts: Should be available only once per machine and print job
                 * FilterCosts: Should be available once per print job info
                 * WashingCosts: Are per print job info and file quantity
                 */

                var gloveCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "GloveCosts")?.ToList();
                Assert.That(gloveCosts?.Count == calculation.AvailablePrinters?.Where(printer => printer.MaterialType == Material3dFamily.Resin).Count());
                Assert.That(gloveCosts?.Sum(c => c.Value) == 0.5d);

                int fileCount = calculation.PrintInfos.Where(pi => pi.Printer?.MaterialType == Material3dFamily.Resin).Count();

                var filterCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "FilterCosts")?.ToList();
                Assert.That(filterCosts?.Count == fileCount);
                Assert.That(filterCosts?.Sum(c => c.Value) == 1d);

                var washingCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "WashingCosts")?.ToList();
                Assert.That(washingCosts?.Count == fileCount);
                foreach (var washCost in washingCosts)
                {
                    File3dUsage? f = calculation.PrintInfos.FirstOrDefault(pi => pi.FileUsage.File.Id == washCost.FileId)?.FileUsage;
                    Assert.That(washCost.Value / f.Quantity == 1d);
                }

                var tankWearCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "ResinTankWearCosts")?.ToList();
                Assert.That(tankWearCosts?.Count == fileCount);
                Assert.That(tankWearCosts?.Sum(c => c.Value) == 2.4d);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void Item3dTests()
        {
            try
            {
                Manufacturer wuerth = new()
                {
                    Name = "W�rth",
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
                double startAmount = 0;
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
                location.TakeFromStock(material, 0.0005, Unit.MetricTons, false);
                newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                // Check if the addition was successfully
                Assert.That(newItem?.Amount == startAmount + 0.75 - 0.5);
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
                    Parameters =
                    [
                        new ProcedureCalculationParameter()
                        {
                            Name = "Tank replacement costs",
                            Type = ProcedureCalculationType.ReplacementCosts,
                            Price = 50,
                            WearFactor = 1,
                            QuantityInPackage = 1,
                        }
                    ]
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
                    Parameters =
                    [
                        new ProcedureCalculationParameter()
                        {
                            Name = "Gloves costs",
                            Type = ProcedureCalculationType.ConsumableGoods,
                            Price = 50,
                            AmountTakenForCalculation = 2,
                            QuantityInPackage = 100,
                        }
                    ]
                };
                double glovesCosts = gloves.CalculateCosts();
                Assert.That(glovesCosts == 1d);

                if (calculation is not null)
                {
                    calculation.Procedure = Material3dFamily.Resin;
                    calculation.ProcedureAdditions = [
                            resinTank,
                        gloves,
                    ];
                    calculation.ApplyProcedureSpecificAdditions = true;

                    /*
                    Calculation3dEnhanced calculation = new()
                    {
                        Procedure = Material3dFamily.Resin,
                        ProcedureAdditions = [
                            resinTank,
                            gloves,
                        ],
                        ApplyProcedureSpecificAdditions = true
                    };
                    */
                    calculation.CalculateCosts();

                    Assert.That(calculation.OverallPrinterCosts?.FirstOrDefault(cost => cost.Attribute == "Resin Tank Replacement") is not null);
                    Assert.That(calculation.Costs?.FirstOrDefault(cost => cost.Attribute == "Gloves") is not null);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void PrintInfoCalculationTest()
        {
            try
            {
                if (true)
                {
                    Manufacturer manufacturer = new()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    };

                    Material3d material = new()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        Manufacturer = manufacturer,
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 29.99,
                        PriceIncludesTax = true,
                    };

                    Material3d material2 = new()
                    {
                        Name = "Test 2",
                        SKU = "Some other material number",
                        Manufacturer = manufacturer,
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 59.99,
                        PriceIncludesTax = true,
                    };

                    HourlyMachineRate hmr = new()
                    {
                        ReplacementCosts = 799,
                        MachineHours = 160,
                        PerYear = false,
                        EnergyCosts = 20,
                    };

                    Printer3d printer = new()
                    {
                        Id = Guid.NewGuid(),
                        Manufacturer = manufacturer,
                        Model = "XL MK1",
                        Price = 799,
                        MaterialType = Material3dFamily.Filament,
                        Type = Printer3dType.FDM,
                        PowerConsumption = 210,
                        HourlyMachineRate = hmr,
                    };

                    Printer3d printer2 = new()
                    {
                        Id = Guid.NewGuid(),
                        Manufacturer = manufacturer,
                        Model = "XL - Dual Head",
                        Price = 2499,
                        MaterialType = Material3dFamily.Filament,
                        Type = Printer3dType.FDM,
                        PowerConsumption = 400,
                        HourlyMachineRate = hmr,
                    };

                    File3d file = new()
                    {
                        Name = "My cool file",
                        Volume = 251.54,
                        PrintTime = 2.34,
                    };
                    File3dUsage fileUsage = new()
                    {
                        File = file,
                        Quantity = 1,
                    };
                    File3d file2 = new()
                    {
                        Name = "Another cool file",
                        Volume = 23.64,
                        PrintTime = 0.55,
                    };
                    File3dUsage fileUsage2 = new()
                    {
                        File = file2,
                        Quantity = 5,
                    };
                    Manufacturer wuerth = new()
                    {
                        Name = "W�rth",
                        DebitorNumber = "DE26265126",
                        Website = "https://www.wuerth.de/",
                    };

                    Item3d item = new()
                    {
                        Name = "Nuts M3",
                        PackageSize = 100,
                        PackagePrice = 9.99d,
                        Manufacturer = wuerth,
                        SKU = "2302423-1223"
                    };
                    Item3dUsage usage = new()
                    {
                        Item = item,
                        Quantity = 5,
                    };

                    Print3dInfo info = new()
                    {
                        FileUsage = fileUsage,
                        MaterialUsages = [new() { Material = material, PercentageValue = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    Print3dInfo info2 = new()
                    {
                        FileUsage = fileUsage2,
                        // Multi-Material for one file
                        MaterialUsages = [new() { Material = material, PercentageValue = 0.5 }, new() { Material = material2, PercentageValue = 0.5 }],
                        Printer = printer2,
                    };

                    Calculation3dEnhanced calc = new()
                    {
                        Name = "Test Calculation",
                        PrintInfos = [info, info2],
                    };

                    calc.CalculateCosts();
                    double total = calc.GetTotalCosts();
                    Assert.That(total > 0);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MarginCalculationTest()
        {
            try
            {
                Calculation3dEnhanced? marginTestCalculation = calculation;
                marginTestCalculation?.CalculateCosts();

                double margin = marginTestCalculation?.CalculatedMargin ?? 0;
                Assert.That(margin == 0);

                calculation.Rates.Add(new CalculationAttribute()
                {
                    Target = CalculationAttributeTarget.Fee,
                    Type = CalculationAttributeType.Margin,
                    Value = 100, // 100 % margin
                    IsPercentageValue = true,
                });
                marginTestCalculation?.CalculateCosts();
                margin = marginTestCalculation?.CalculatedMargin ?? 0;
                Assert.That(margin > 0);

                calculation.Rates.Add(new CalculationAttribute()
                {
                    Target = CalculationAttributeTarget.Fee,
                    Type = CalculationAttributeType.HandlingFee,
                    Value = 5, // 5� fee
                    IsPercentageValue = false,
                    ApplyPerFile = false,
                });

                marginTestCalculation?.CalculateCosts();
                var margin2 = marginTestCalculation?.CalculatedMargin ?? 0;
                Assert.That(margin2 > margin);

                calculation.Rates.Clear();
                calculation.Rates.Add(new CalculationAttribute()
                {
                    Target = CalculationAttributeTarget.Fee,
                    Type = CalculationAttributeType.Margin,
                    Value = 200, // 200 % margin
                    IsPercentageValue = true,
                });
                marginTestCalculation?.CalculateCosts();
                var margin3 = marginTestCalculation?.CalculatedMargin ?? 0;
                Assert.That(margin3 == margin * 2);

                calculation.Rates.Add(new CalculationAttribute()
                {
                    Target = CalculationAttributeTarget.Fee,
                    Type = CalculationAttributeType.HandlingFee,
                    Value = 5, // 5� fee
                    IsPercentageValue = false,
                    ApplyPerFile = true,
                });
                marginTestCalculation?.CalculateCosts();
                var margin4 = marginTestCalculation?.CalculatedMargin ?? 0;
                Assert.That(margin3 == margin * 2);

            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}