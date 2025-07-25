using AndreasReitberger.Print3d.Core.Enums;
using AndreasReitberger.Print3d.SQLite;
using AndreasReitberger.Shared.Core.Utilities;
using NUnit.Framework;
using SQLite;
using System.Diagnostics;

namespace AndreasReitberger.NUnitTest
{
    public class TestsSqlite
    {
        bool ApplyResinGlovesCosts = true;
        bool ApplyResinWashingCosts = true;
        bool ApplyResinFilterCosts = true;
        bool ApplyResinTankWearCosts = true;

        string key = "K4eO9Qq4GTO9D0g0aBPzYGp0KsBoYYyFT9S3SX1VgOg=";

        Calculation3dEnhanced? calculation;

        [SetUp]
        public void Setup()
        {
            // Example for key generation
            //string t = EncryptionManager.GenerateBase64Key();

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
                        FileUsage = new()
                        {
                            File = new File3d()
                            {
                                FileName = "my.gcode",
                                PrintTime = 5.263,
                                Volume = 35.54,
                            },
                            Quantity = 5,
                        },
                        Materials = [
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
                        FileUsage = new()
                        {
                            File = new File3d()
                            {
                                FileName = "Batman.dlp",
                                PrintTime = 2.65,
                                Volume = 65.546,
                            },
                            Quantity = 20,
                        },
                        Materials = [
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
                        FileUsage = new()
                        {
                            File = new File3d()
                            {
                                FileName = "Superman.dlp",
                                PrintTime = 1.42,
                                Volume = 35.4536,
                            },
                            Quantity = 3,
                        },
                        Materials = [
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
                Assert.That(gloveCosts?.Count == calculation.AvailablePrinters.Where(printer => printer.MaterialType == Material3dFamily.Resin).Count());
                Assert.That(gloveCosts?.Sum(c => c.Value) == 0.5d);

                int fileCount = calculation.PrintInfos.Where(pi => pi.Printer.MaterialType == Material3dFamily.Resin).Count();

                var filterCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "FilterCosts")?.ToList();
                Assert.That(filterCosts?.Count == fileCount);
                Assert.That(filterCosts?.Sum(c => c.Value) == 1d);

                var washingCosts = calculation.OverallPrinterCosts.Where(c => c.Attribute == "WashingCosts")?.ToList();
                Assert.That(washingCosts?.Count == fileCount);
                foreach (var washCost in washingCosts)
                {
                    File3dUsage f = calculation.PrintInfos.FirstOrDefault(pi => pi.FileUsage.File.Id == washCost.FileId).FileUsage;
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
        public async Task DatabaseTestsAsync()
        {
            try
            {
                string databasePath = "testdatabase.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();

                    // Manufacturers
                    Manufacturer prusa = new()
                    {
                        Name = "Prusa",
                        DebitorNumber = "CZ000001",
                        Website = "https://shop.prusa3d.com/en/#a_aid=AndreasReitberger",
                        IsActive = true,
                    };
                    await DatabaseHandler.Instance.SetManufacturerWithChildrenAsync(prusa);
                    Manufacturer addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                    Assert.That(addedManufacturer is not null);
                    Assert.That(
                        prusa.Name == addedManufacturer.Name &&
                        prusa.DebitorNumber == addedManufacturer.DebitorNumber &&
                        prusa.Website == addedManufacturer.Website
                        );
                    List<Manufacturer> manufacturers = await DatabaseHandler.Instance.GetManufacturersWithChildrenAsync();
                    Assert.That(manufacturers?.Count > 0);

                    List<Material3dType> materialTypes =
                    [
                        new Material3dType()
                        {
                            Material = "ABS",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "ASA",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PLA",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PETG",
                            Family = Material3dFamily.Filament,
                        },
                        new Material3dType()
                        {
                            Material = "PET",
                            Family = Material3dFamily.Filament,
                        },
                    ];
                    await DatabaseHandler.Instance.SetMaterialTypesWithChildrenAsync(materialTypes);
                    List<Material3dType> types = await DatabaseHandler.Instance.GetMaterialTypesWithChildrenAsync();
                    Assert.That(materialTypes.Count == types?.Count);

                    // Hourly Machine Rate
                    HourlyMachineRate mhr = new()
                    {
                        MachineHours = 200,
                        PerYear = false,
                        EnergyCosts = 50,
                        LocationCosts = 20,
                        MaintenanceCosts = 120,
                    };
                    await DatabaseHandler.Instance.SetHourlyMachineRateWithChildrenAsync(mhr);
                    HourlyMachineRate hourlyMachineRate = await DatabaseHandler.Instance.GetHourlyMachineRateWithChildrenAsync(mhr.Id);
                    List<HourlyMachineRate> hourlyMachineRates = await DatabaseHandler.Instance.GetHourlyMachineRatesWithChildrenAsync();
                    Assert.That(hourlyMachineRates?.Count > 0);

                    // Printers
                    Printer3d prusaXL = new()
                    {
                        Model = "XL",
                        Manufacturer = new()
                        {
                            Name = "Prusa",
                        },
                        Type = Printer3dType.FDM,
                        MaterialType = Material3dFamily.Filament,
                        Uri = "https://www.prusa3d.com/de/produkt/original-prusa-xl-3/#a_aid=AndreasReitberger",
                        Price = 2600,
                        PriceIncludesTax = true,
                        PowerConsumption = 300,
                        HourlyMachineRate = hourlyMachineRate,
                    };
                    await DatabaseHandler.Instance.SetPrinterWithChildrenAsync(prusaXL);
                    Printer3d printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    List<Printer3d> printers = await DatabaseHandler.Instance.GetPrintersWithChildrenAsync();
                    Assert.That(printers?.Count > 0);

                    // Materials
                    Material3d materialPETG = new()
                    {
                        Name = "Prusament PETG 1kg",
                        Density = 1.24,
                        Manufacturer = prusa,
                        MaterialFamily = Material3dFamily.Filament,
                        TypeOfMaterial = new Material3dType()
                        {
                            Material = "PETG",
                            Family = Material3dFamily.Filament,
                        },
                        UnitPrice = 30,
                        PriceIncludesTax = true,
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        Uri = "https://www.prusa3d.com/product/prusament-petg-anthracite-grey-1kg/#a_aid=AndreasReitberger"
                    };
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(materialPETG);
                    Material3d material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    List<Material3d> materials = await DatabaseHandler.Instance.GetMaterialsWithChildrenAsync();
                    Assert.That(materials?.Count > 0);

                    // Additional items
                    Item3d item = new()
                    {
                        Name = "70x4mm Screws",
                        SKU = "SC2565263189",
                        PackagePrice = 30,
                        PackageSize = 100,
                    };
                    Item3d item2 = new()
                    {
                        Name = "50x3mm Screws",
                        SKU = "SC23425435345",
                        PackagePrice = 35,
                        PackageSize = 200,
                    };

                    await DatabaseHandler.Instance.SetItemsWithChildrenAsync([item, item2]);
                    List<Item3d>? items = await DatabaseHandler.Instance.GetItemsWithChildrenAsync();
                    Assert.That(items?.Count == 2);

                    List<Item3dUsage> usages = new(items.Select(curItem => new Item3dUsage() { Item = curItem, Quantity = 10 }));
                    await DatabaseHandler.Instance.SetItemUsagesWithChildrenAsync(usages);

                    await DatabaseHandler.Instance.DeleteMaterialAsync(materialPETG);
                    //material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                    //Assert.IsNull(material);

                    await DatabaseHandler.Instance.DeletePrinterAsync(prusaXL);
                    //printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                    //Assert.IsNull(printer);

                    await DatabaseHandler.Instance.DeleteManufacturerAsync(prusa);
                    //addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                    //Assert.IsNull(addedManufacturer);
                }
                else
                {
                    Assert.Fail($"Database not initialized: {databasePath}");
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task DatabaseBuilderTest()
        {
            try
            {
                string databasePath = "testdatabase.db";
                using DatabaseHandler handler = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTable(typeof(Manufacturer))
                    .WithTables([typeof(Material3dType), typeof(Material3d)])
                    .BuildAsync();

                List<TableMapping>? mappings = handler.GetTableMappings();
                Assert.That(mappings?.Count > 0);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task Item3dTests()
        {
            try
            {
                string databasePath = "testdatabase_items.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables([
                        typeof(Manufacturer),
                        typeof(File3d),
                        typeof(Item3d),
                        typeof(Item3dUsage),
                    ])
                    .BuildAsync();

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
                await handler.SetItemsWithChildrenAsync([item1, item2]);

                var loadedItem1 = await handler.GetItemWithChildrenAsync(item1.Id);
                Assert.That(loadedItem1 is not null);
                Assert.That(loadedItem1?.Manufacturer is not null);

                var loadedItems = await handler.GetItemsWithChildrenAsync();
                Assert.That(loadedItems?.Count == 2);

                Item3dUsage usage = new()
                {
                    Item = item1,
                    Quantity = 30,
                    LinkedToFile = false,
                };
                await handler.SetItemUsageWithChildrenAsync(usage);

                Item3dUsage loadedUsage = await handler.GetItemUsageWithChildrenAsync(usage.Id);
                Assert.That(loadedUsage is not null);
                Assert.That(loadedUsage.Item is not null);

                var manufacturerLoaded = loadedUsage?.Item?.Manufacturer;
                Assert.That(manufacturerLoaded is not null);

                var usages = await handler.GetItemUsagesWithChildrenAsync();
                Assert.That(usages?.Count == 1);

                await handler.DeleteItemUsageAsync(loadedUsage);

                usages = await handler.GetItemUsagesWithChildrenAsync();
                Assert.That(usages?.Count == 0);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task WorkstepTests()
        {
            try
            {
                string databasePath = "testdatabase_worksteps.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables([
                        typeof(Workstep),
                        typeof(WorkstepCategory),
                        typeof(WorkstepUsage),
                        typeof(WorkstepUsageParameter),
                    ])
                    .BuildAsync();
                WorkstepCategory category = new()
                {
                    Name = "Construction"
                };
                await handler.SetWorkstepCategoryWithChildrenAsync(category);

                Workstep ws = new()
                {
                    Name = "3D CAD",
                    Category = category,
                    Price = 30,
                };
                await handler.SetWorkstepWithChildrenAsync(ws);

                WorkstepUsage usage = new()
                {
                    Workstep = ws,
                    UsageParameter = new()
                    {
                        ParameterType = WorkstepUsageParameterType.Duration,
                        Value = 1.5d
                    }
                };
                await handler.SetWorkstepUsageWithChildrenAsync(usage);


                WorkstepUsage loadedUsage = await handler.GetWorkstepUsageWithChildrenAsync(usage.Id);
                Assert.That(loadedUsage is not null);
                Assert.That(loadedUsage?.UsageParameter is not null);
                Assert.That(loadedUsage?.Workstep is not null);

                var usages = await handler.GetWorkstepUsagesWithChildrenAsync();
                Assert.That(usages?.Count == 1);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task StorageDatabaseTest()
        {
            try
            {
                string databasePath = "testdatabase_storage.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();
                    await Task.Delay(500);

                    double startAmount = 0;
                    Material3d material = new()
                    {
                        Name = "Test",
                        SKU = "Some material number",
                        PackageSize = 1,
                        Unit = Unit.Kilogram,
                        UnitPrice = 29.99,
                        PriceIncludesTax = true,
                    };

                    Storage3dItem item = new()
                    {
                        Material = material,
                    };

                    Storage3dLocation location = new()
                    {
                        Location = "LS-01-01-01",
                        Capacity = 20,
                        Items = [item],
                    };

                    Storage3d storage = new()
                    {
                        Name = "Main material storage",
                        Locations = [location],
                    };
                    await Task.Delay(250);
                    await DatabaseHandler.Instance.SetStorageWithChildrenAsync(storage);
                    var storageLoaded = await DatabaseHandler.Instance.GetStorageWithChildrenAsync(storage.Id);

                    Assert.That(storage is not null);
                    Assert.That(storage.Locations?.Count == 1);
                    Assert.That(storage.Locations?.FirstOrDefault()?.Items?.Count == 1);
                    Assert.That(storage.Locations?.FirstOrDefault()?.Items?.FirstOrDefault()?.Material is not null);

                    List<Storage3dTransaction> transactions = [];
                    /**/
                    Storage3dTransaction? transaction1 = location.AddToStock(material, 750, Unit.Gram, null);
                    Storage3dItem? newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.That(newItem?.Amount == startAmount + 0.75);
                    Assert.That(transaction1 is not null);
                    if (transaction1 is not null)
                        transactions.Add(transaction1);

                    // Just to check if the unit conversion is working
                    Storage3dTransaction? transaction2 = location.TakeFromStock(material, 0.0005, Unit.MetricTons, null, false);
                    newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.That(newItem?.Amount == startAmount + 0.75 - 0.5);
                    Assert.That(transaction2 is not null);
                    if (transaction2 is not null)
                        transactions.Add(transaction2);
                    await DatabaseHandler.Instance.SetStorageTransactionsWithChildrenAsync(transactions);

                    List<Storage3dTransaction>? transactionsLoaded = await DatabaseHandler.Instance.GetStorageTransactionsWithChildrenAsync();
                    Assert.That(transactionsLoaded?.Count == transactions?.Count);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
        /*
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
                    calculation.ApplyProcedureSpecificAdditions = true;
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
        */
        [Test]
        public async Task PrintInfoCalculationTestAsync()
        {
            try
            {
                string databasePath = "testdatabase_info.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();
                    await Task.Delay(500);

                    Manufacturer manufacturer = new()
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        Name = "Prusa"
                    };
                    await DatabaseHandler.Instance.SetManufacturerWithChildrenAsync(manufacturer);

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
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(material);

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
                    await DatabaseHandler.Instance.SetMaterialWithChildrenAsync(material2);

                    HourlyMachineRate hmr = new()
                    {
                        ReplacementCosts = 799,
                        MachineHours = 160,
                        PerYear = false,
                        EnergyCosts = 20,
                    };
                    await DatabaseHandler.Instance.SetHourlyMachineRateWithChildrenAsync(hmr);

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
                    await DatabaseHandler.Instance.SetPrinterWithChildrenAsync(printer);
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
                    await DatabaseHandler.Instance.SetPrinterWithChildrenAsync(printer2);

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
                    await DatabaseHandler.Instance.SetFileUsageWithChildrenAsync(fileUsage);
                    File3d file2 = new()
                    {
                        Name = "Another cool file",
                        Volume = 23.64,
                        PrintTime = 0.55,
                    };
                    File3dUsage fileUsage1 = new()
                    {
                        File = file2,
                        Quantity = 5,
                    };
                    await DatabaseHandler.Instance.SetFileUsageWithChildrenAsync(fileUsage1);

                    Manufacturer wuerth = new()
                    {
                        Name = "W�rth",
                        DebitorNumber = "DE26265126",
                        Website = "https://www.wuerth.de/",
                    };
                    await DatabaseHandler.Instance.SetManufacturerWithChildrenAsync(wuerth);

                    Item3d item = new()
                    {
                        Name = "Nuts M3",
                        PackageSize = 100,
                        PackagePrice = 9.99d,
                        Manufacturer = wuerth,
                        SKU = "2302423-1223"
                    };
                    await DatabaseHandler.Instance.SetItemWithChildrenAsync(item);
                    Item3dUsage usage = new()
                    {
                        Item = item,
                        Quantity = 5,
                    };
                    await DatabaseHandler.Instance.SetItemUsageWithChildrenAsync(usage);

                    Print3dInfo info = new()
                    {
                        FileUsage = fileUsage,
                        Materials = [new() { Material = material, PercentageValue = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    await DatabaseHandler.Instance.SetPrintInfoWithChildrenAsync(info);
                    Print3dInfo info2 = new()
                    {
                        FileUsage = fileUsage1,
                        // Multi-Material for one file
                        Materials = [new() { Material = material, PercentageValue = 0.5 }, new() { Material = material2, PercentageValue = 0.5 }],
                        Printer = printer2,
                    };
                    await DatabaseHandler.Instance.SetPrintInfoWithChildrenAsync(info2);

                    Calculation3dEnhanced calc = new()
                    {
                        Name = "Test Calculation",
                        PrintInfos = [info, info2],
                    };

                    calc.CalculateCosts();
                    double total = calc.GetTotalCosts();

                    await DatabaseHandler.Instance.SetEnhancedCalculationWithChildrenAsync(calc);
                    Calculation3dEnhanced loadedCalc = await DatabaseHandler.Instance.GetEnhancedCalculationWithChildrenAsync(calc.Id);
                    loadedCalc.CalculateCosts();
                    double totalLoaded = loadedCalc.GetTotalCosts();
                    Assert.That(total == totalLoaded);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task RecurringDatabaseCalculationEnhancedSaveAndLoadTest()
        {
            try
            {
                string databasePath = "testdatabase_enhanced.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                DatabaseHandler.Instance = new DatabaseHandler(databasePath);
                if (DatabaseHandler.Instance.IsInitialized)
                {
                    // Clear all tables
                    await DatabaseHandler.Instance.TryClearAllTableAsync();
                    await DatabaseHandler.Instance.TryDropAllTableAsync();

                    // Recreate tables
                    await DatabaseHandler.Instance.RebuildAllTableAsync();
                    await Task.Delay(500);

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
                        Materials = [new() { Material = material, PercentageValue = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    Print3dInfo info2 = new()
                    {
                        FileUsage = fileUsage2,
                        // Multi-Material for one file
                        Materials = [new() { Material = material, PercentageValue = 0.5 }, new() { Material = material2, PercentageValue = 0.5 }],
                        Printer = printer2,
                    };

                    Calculation3dEnhanced calc = new()
                    {
                        Name = "Test Calculation",
                        PrintInfos = [info, info2],
                    };

                    calc.CalculateCosts();
                    double total = calc.GetTotalCosts();

                    await DatabaseHandler.Instance.SetEnhancedCalculationWithChildrenAsync(calc);
                    Calculation3dEnhanced loadedCalc = await DatabaseHandler.Instance.GetEnhancedCalculationWithChildrenAsync(calc.Id);
                    loadedCalc.CalculateCosts();
                    double totalLoaded = loadedCalc.GetTotalCosts();
                    Assert.That(Math.Round(total, 5) == Math.Round(totalLoaded, 5));

                    Assert.That(loadedCalc?.AvailableMaterials?.Count == 2);
                    Assert.That(loadedCalc?.AvailablePrinters?.Count == 2);
                    Assert.That(loadedCalc?.PrintInfos?.Count == 2);

                    List<Printer3d> printers = await DatabaseHandler.Instance.GetPrintersWithChildrenAsync();
                    Assert.That(printers?.Count == 2);

                    List<Material3d> materials = await DatabaseHandler.Instance.GetMaterialsWithChildrenAsync();
                    Assert.That(materials?.Count == 2);

                    List<File3d> files = await DatabaseHandler.Instance.GetFilesWithChildrenAsync();
                    Assert.That(files?.Count == 2);

                    List<Manufacturer> manufacturers = await DatabaseHandler.Instance.GetManufacturersWithChildrenAsync();
                    Assert.That(manufacturers?.Count == 2);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public async Task DatabaseEncrytpionTestAsync()
        {
            try
            {
                string databasePath = "testdatabase_secure.db";
                if (File.Exists(databasePath))
                    File.Delete(databasePath);

                using DatabaseHandler handler = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTable(typeof(Manufacturer))
                    .WithTables([typeof(Material3dType), typeof(Material3d)])
                    .WithPassphrase(key)
                    .BuildAsync();

                List<TableMapping>? mappings = handler.GetTableMappings();
                Assert.That(mappings?.Count > 0);

                await handler.SetMaterialTypeWithChildrenAsync(new()
                {
                    Id = Guid.NewGuid(),
                    Family = Material3dFamily.Filament,
                    Material = "PETG",
                });
                List<Material3dType> types = await handler.GetMaterialTypesWithChildrenAsync();
                Assert.That(types?.Count > 0);

                await handler.CloseDatabaseAsync();
                handler.Dispose();

                try
                {
                    using DatabaseHandler handlerUnseure = await new DatabaseHandler.DatabaseHandlerBuilder()
                        .WithDatabasePath(databasePath)
                        .WithTable(typeof(Manufacturer))
                        .WithTables([typeof(Material3dType), typeof(Material3d)])
                        //.WithPassphrase(key)
                        .BuildAsync();
                    mappings = handler.GetTableMappings();
                    types = await handler.GetMaterialTypesWithChildrenAsync();

                    Assert.Fail("Building without the key should throw an exception");
                }
                catch (SQLiteException sqlite_exc)
                {
                    Debug.WriteLine($"SQlite-Exception: {sqlite_exc.Message}");
                }
                catch (Exception exc)
                {
                    Assert.Fail($"{exc.Message}");
                }

                try
                {
                    using DatabaseHandler handlerUnseure = await new DatabaseHandler.DatabaseHandlerBuilder()
                        .WithDatabasePath(databasePath)
                        // Different key also should throw
                        .WithTable(typeof(Manufacturer))
                        .WithTables([typeof(Material3dType), typeof(Material3d)])
                        .WithPassphrase(EncryptionManager.GenerateBase64Key())
                        .BuildAsync();

                    mappings = handlerUnseure.GetTableMappings();
                    types = await handlerUnseure.GetMaterialTypesWithChildrenAsync();

                    Assert.Fail("Building with a different key should throw an exception");
                }
                catch (SQLiteException sqlite_exc)
                {
                    Debug.WriteLine($"SQlite-Exception: {sqlite_exc.Message}");
                }
                catch (Exception exc)
                {
                    Assert.Fail($"{exc.Message}");
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
        [Test]
        public async Task DatabaseEncrytpionRekeyTestAsync()
        {
            try
            {
                string databasePath = "testdatabase_secure.db";
                if (File.Exists(databasePath))
                    File.Delete(databasePath);

                using DatabaseHandler handler = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTable(typeof(Manufacturer))
                    .WithTables([typeof(Material3dType), typeof(Material3d)])
                    .WithPassphrase(key)
                    .BuildAsync();

                List<TableMapping>? mappings = handler.GetTableMappings();
                Assert.That(mappings?.Count > 0);

                await handler.SetMaterialTypeWithChildrenAsync(new()
                {
                    Id = Guid.NewGuid(),
                    Family = Material3dFamily.Filament,
                    Material = "PETG",
                });
                List<Material3dType> types = await handler.GetMaterialTypesWithChildrenAsync();
                Assert.That(types?.Count > 0);

                // try to rekey
                string newKey = EncryptionManager.GenerateBase64Key();
                await handler.RekeyDatabaseAsync(newKey);

                await handler.CloseDatabaseAsync();
                using DatabaseHandler handler2 = await new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTable(typeof(Manufacturer))
                    .WithTables([typeof(Material3dType), typeof(Material3d)])
                    .WithPassphrase(newKey)
                    .BuildAsync();

                types = await handler2.GetMaterialTypesWithChildrenAsync();
                Assert.That(types?.Count > 0);
                await handler2.CloseDatabaseAsync();
                try
                {
                    using DatabaseHandler handler3 = await new DatabaseHandler.DatabaseHandlerBuilder()
                        .WithDatabasePath(databasePath)
                        .WithTable(typeof(Manufacturer))
                        .WithTables([typeof(Material3dType), typeof(Material3d)])
                        .WithPassphrase(key)
                        .BuildAsync();
                    types = await handler2.GetMaterialTypesWithChildrenAsync();
                    await handler2.CloseDatabaseAsync();
                    Assert.Fail("Should throw on wrong key");
                }
                catch (SQLiteException sqlite_exc)
                {
                    Debug.WriteLine($"SQlite-Exception: {sqlite_exc.Message}");
                }
                catch (Exception exc)
                {
                    Assert.Fail($"{exc.Message}");
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