using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.SQLite;
using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.CustomerAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.ProcedureAdditions;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using NUnit.Framework;
using SQLite;

namespace AndreasReitberger.NUnitTest
{
    public class TestsSqlite
    {
        bool ApplyResinGlovesCosts = true;
        bool ApplyResinWashingCosts = true;
        bool ApplyResinFilterCosts = true;
        bool ApplyResinTankWearCosts = true;

        Calculation3dEnhanced? calculation;

        [SetUp]
        public void Setup()
        {
            Printer3d printerFDM = new Printer3d()
            {
                Type = Printer3dType.FDM,
                Model = "i3 MK3S",
                PowerConsumption = 210,
                Price = 899,
                MaterialType = Material3dFamily.Filament,
            };
            Printer3d printerDLP = new Printer3d()
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
                        File = new File3d()
                        {
                            FileName = "my.gcode",
                            PrintTime = 5.263,
                            Volume = 35.54,
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
                        File = new File3d()
                        {
                            FileName = "Batman.dlp",
                            PrintTime = 2.65,
                            Volume = 65.546,
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
                        File = new File3d()
                        {
                            FileName = "Superman.dlp",
                            PrintTime = 1.42,
                            Volume = 35.4536,
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
                            Value = 120d  * 0.01d,
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
                    File3d f = calculation.PrintInfos.FirstOrDefault(pi => pi.File.Id == washCost.FileId).File;
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

                    await DatabaseHandler.Instance.SetItemsWithChildrenAsync(new() { item, item2 });
                    List<Item3d>? items = await DatabaseHandler.Instance.GetItemsWithChildrenAsync();
                    Assert.That(items?.Count == 2);

                    List<Item3dUsage> usages = new(items.Select(curItem => new Item3dUsage() { Item = curItem, Quantity = 10 }));
                    await DatabaseHandler.Instance.SetItemUsagesWithChildrenAsync(usages);

                    Calculation3d calculation = new()
                    {
                        Printer = printer,
                        Material = material,
                        AdditionalItems = usages,
                    };
                    calculation.Printers.Add(printer);
                    calculation.Materials.Add(material);
                    calculation.Files.Add(new File3d()
                    {
                        FileName = "TestFile",
                        PrintTime = 10.5,
                        Volume = 5.25,
                        Quantity = 3,
                    });
                    calculation.AdditionalItems.Add(new Item3dUsage()
                    {
                        Item = item,
                        LinkedToFile = true,
                        File = calculation.Files.First()
                    });

                    calculation.CalculateCosts();

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation);
                    Calculation3d calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.That(calculation.TotalCosts == calcFromDB.TotalCosts);

                    List<Calculation3d> calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

                    Calculation3d calculation2 = new()
                    {
                        Printer = printer,
                        Material = material,
                    };
                    calculation2.Printers.Add(printer);
                    calculation2.Materials.Add(material);
                    calculation2.Files.Add(new File3d()
                    {
                        FileName = "TestFile",
                        PrintTime = 25.5,
                        Volume = 10.25,
                        Quantity = 30,
                    });
                    calculation2.CalculateCosts();

                    await DatabaseHandler.Instance.SetCalculationWithChildrenAsync(calculation2);
                    Calculation3d calcFromDB2 = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation2.Id);
                    Assert.That(calculation2.TotalCosts == calcFromDB2.TotalCosts);

                    calcFromDB = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    Assert.That(calculation.TotalCosts == calcFromDB.TotalCosts);

                    calculations = await DatabaseHandler.Instance.GetCalculationsWithChildrenAsync();

                    // Cleanup
                    await DatabaseHandler.Instance.DeleteCalculationAsync(calculation);
                    //calculation = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                    //Assert.IsNull(calculation);

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
        public void DatabaseBuilderTest()
        {
            string databasePath = "testdatabase.db";
            using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                .WithDatabasePath(databasePath)
                .WithTable(typeof(Manufacturer))
                .WithTables(new List<Type> { typeof(Material3dType), typeof(Material3d) })
                .Build();

            List<TableMapping> mappings = handler.GetTableMappings();
            Assert.That(mappings?.Count > 0);
        }

        private Calculation3d calculationObsolete;
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

                calculationObsolete = new();
                // Add data
                calculationObsolete.AdditionalItems.Add(usage);
                calculationObsolete.Files.Add(file);
                calculationObsolete.Files.Add(file2);
                calculationObsolete.Printers.Add(printer);
                calculationObsolete.Materials.Add(material);
                calculationObsolete.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Tax,
                    IsPercentageValue = true,
                    Value = 19,
                });
                calculationObsolete.Rates.Add(new()
                {
                    Type = CalculationAttributeType.Margin,
                    IsPercentageValue = true,
                    Value = 100,
                });

                // Add information
                calculationObsolete.FailRate = 25;
                calculationObsolete.EnergyCostsPerkWh = 0.30;
                calculationObsolete.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                calculationObsolete.PowerLevel = 75;

                calculationObsolete.CalculateCosts();
                double totalCosts = calculationObsolete.TotalCosts;
                Assert.That(calculationObsolete.IsCalculated);

                List<double> costsCalc = new()
                {
                    calculationObsolete.MachineCosts,
                    calculationObsolete.MaterialCosts,
                    calculationObsolete.CalculatedMargin,
                    calculationObsolete.CalculatedTax,
                    calculationObsolete.ItemsCost,
                };
                double summedCalc = costsCalc.Sum();
                Assert.That(Math.Round(summedCalc, 2) == Math.Round(calculationObsolete.TotalCosts, 2));


                Calculation3d _calculation2 = calculationObsolete?.Clone() as Calculation3d;
                _calculation2.CalculateCosts();
                Assert.That(_calculation2.IsCalculated);
                Assert.That(totalCosts == _calculation2.TotalCosts);

                costsCalc = new()
                {
                    _calculation2.MachineCosts,
                    _calculation2.MaterialCosts,
                    _calculation2.CalculatedMargin,
                    _calculation2.CalculatedTax,
                    _calculation2.ItemsCost,
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

                calculationObsolete = new Calculation3d();
                // Add data
                calculationObsolete.Files.Add(file);
                calculationObsolete.Files.Add(file2);
                calculationObsolete.Printers.Add(printer);
                calculationObsolete.Materials.Add(material);

                // Add information
                calculationObsolete.FailRate = 25;
                calculationObsolete.EnergyCostsPerkWh = 0.30;
                calculationObsolete.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                calculationObsolete.PowerLevel = 75;

                calculationObsolete.CalculateCosts();
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

                calculationObsolete = new Calculation3d();
                // Add data
                calculationObsolete.Files.Add(file);
                calculationObsolete.Files.Add(file2);
                calculationObsolete.Printers.Add(printer);
                calculationObsolete.Materials.Add(material);

                // Add information
                calculationObsolete.FailRate = 25;
                calculationObsolete.EnergyCostsPerkWh = 0.30;
                calculationObsolete.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                calculationObsolete.PowerLevel = 75;

                calculationObsolete.ApplyProcedureSpecificAdditions = true;
                // Needed if the calculation is reloaded later
                List<CalculationProcedureParameterAddition> additionalInfo =
                [
                    new CalculationProcedureParameterAddition("replacementcosts", 20),
                    new CalculationProcedureParameterAddition("wearfactor", 0.5),
                ];

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

                calculationObsolete.ProcedureAttributes.Add(
                    new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.NozzleWear,
                        Family = Material3dFamily.Filament,
                        Parameters = parameters,
                        Level = CalculationLevel.Printer,
                    }
                );

                calculationObsolete.CalculateCosts();

                CalculationAttribute? wearCostAttribute = calculationObsolete.OverallPrinterCosts.FirstOrDefault(item =>
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.That(wearCostAttribute is not null);

                // Updat calculation
                calculationObsolete.Printer = null;
                calculationObsolete.Printers =
                [
                    printerSla
                ];

                calculationObsolete.CalculateCosts();
                wearCostAttribute = calculationObsolete.OverallPrinterCosts.FirstOrDefault(item =>
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
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables(new List<Type> {
                        typeof(Manufacturer),
                        typeof(Item3d),
                        typeof(Item3dUsage),
                    })
                    .Build();

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
                await handler.SetItemsWithChildrenAsync(new() { item1, item2 });

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
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithTables(new List<Type> {
                        typeof(Workstep),
                        typeof(WorkstepCategory),
                        typeof(WorkstepUsage),
                        typeof(WorkstepUsageParameter),
                    })
                    .Build();
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
        public async Task DatabaseSaveAndLoadTest()
        {
            try
            {
                string databasePath = "testdatabase.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithDefaultTables()
                    .Build();

                await Task.Delay(250);

                Calculation3d calc = GetTestCalculation();
                calc.Material = calc.Materials.First();
                calc.Printer = calc.Printers.First();

                await calc.CalculateCostsAsync();

                await handler.SetItemUsagesWithChildrenAsync(calc.AdditionalItems);
                await handler.SetProcedureAdditionsWithChildrenAsync(calc.ProcedureAdditions.ToList());
                await handler.SetMaterialsWithChildrenAsync(calc.Materials);
                await handler.SetPrintersWithChildrenAsync(calc.Printers);
                await handler.SetCalculationWithChildrenAsync(calc);
                Calculation3d calc2 = await handler.GetCalculationWithChildrenAsync(calc.Id);

                await Task.Delay(250);
                Assert.That(calc2 is not null);

                var item = await handler.GetItemWithChildrenAsync(calc.AdditionalItems.FirstOrDefault().ItemId);
                calc2.Material = calc.Materials.First();
                calc2.Printer = calc.Printers.First();

                await calc2.CalculateCostsAsync();
                await Task.Delay(250);
                if (calc2 is not null)
                {
                    Assert.That(calc.MachineCosts == calc2.MachineCosts, "Machine costs differ");
                    Assert.That(calc.MaterialCosts == calc2.MaterialCosts, "Material costs differ");
                    Assert.That(calc.CalculatedMargin == calc2.CalculatedMargin, "Margin differs");
                    Assert.That(calc.CalculatedTax == calc2.CalculatedTax, "Tax differs");
                    Assert.That(calc.EnergyCosts == calc2.EnergyCosts, "Energy costs differ");
                    Assert.That(calc.CustomAdditionCosts == calc2.CustomAdditionCosts, "Custom addition costs differ");
                    Assert.That(calc.ItemsCost == calc2.ItemsCost, "Item costs differ");
                }
                else
                    Assert.Fail("Calculation could not be loaded from the database!");
                Assert.That(Math.Round(calc.TotalCosts, 5) == Math.Round(calc2.TotalCosts, 5), $"Total costs differ: {calc?.TotalCosts} != {calc2?.TotalCosts}");
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

            Print3dInfo info = new()
            {

            };

            calculationObsolete = new Calculation3d()
            {
                Name = "My awesome calculation"
            };
            // Add data
            calculationObsolete.AdditionalItems.Add(usage);
            calculationObsolete.Files.Add(file);
            calculationObsolete.Files.Add(file2);
            calculationObsolete.Printers.Add(printer);
            calculationObsolete.Printers.Add(printer2);
            calculationObsolete.Materials.Add(material);
            calculationObsolete.Materials.Add(material2);
            calculationObsolete.ProcedureAdditions.Add(new ProcedureAddition()
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
            calculationObsolete.ProcedureAdditions.Add(new ProcedureAddition()
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
            calculationObsolete.FailRate = 25;
            calculationObsolete.EnergyCostsPerkWh = 0.30;
            calculationObsolete.ApplyEnergyCost = true;
            // Uses 75% of the max. power consumption set in the printer model (210 Watt)
            calculationObsolete.PowerLevel = 75;

            calculationObsolete.CalculateCosts();
            return calculationObsolete;
        }

        public void MultiFileDifferTest()
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
                Material3d material2 = new()
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

                File3d file = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyFirst.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                };
                File3d file2 = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MySecond.gcode",
                    PrintTime = 10.25,
                    Volume = 12.36,
                    Quantity = 3,
                    MultiplyPrintTimeWithQuantity = false
                };
                File3d file3 = new()
                {
                    Id = Guid.NewGuid(),
                    FileName = "MyThird.gcode",
                    PrintTime = 2.25,
                    Volume = 3.36,
                    Quantity = 25,
                    MultiplyPrintTimeWithQuantity = true
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

                calculationObsolete = new Calculation3d();
                // Add data
                calculationObsolete.Files.Add(file);
                calculationObsolete.Files.Add(file2);
                calculationObsolete.Files.Add(file3);
                calculationObsolete.Printers.Add(printer);
                calculationObsolete.Materials.Add(material);
                calculationObsolete.Materials.Add(material2);

                calculationObsolete.AdditionalItems.Add(usage);

                // Add information
                calculationObsolete.FailRate = 25;
                calculationObsolete.EnergyCostsPerkWh = 0.30;
                calculationObsolete.ApplyEnergyCost = true;
                // Uses 75% of the max. power consumption set in the printer model (210 Watt)
                calculationObsolete.PowerLevel = 75;

                calculationObsolete.CalculateCosts();

                double total = calculationObsolete.GetTotalCosts();

                var materialCosts = PrintCalculator3d.GetMaterialCosts(calculationObsolete);
                var machineCosts = PrintCalculator3d.GetMachineCosts(calculationObsolete);

                calculationObsolete.DifferFileCosts = true;
                calculationObsolete.CalculateCosts();

                double totalDiffer = calculationObsolete.GetTotalCosts();

                Assert.That(calculationObsolete.IsCalculated);
                Assert.That(total == totalDiffer);
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
                        Items = new() { item },
                    };

                    Storage3d storage = new()
                    {
                        Name = "Main material storage",
                        Locations = new() { location },
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
                    Storage3dTransaction transaction1 = location.AddToStock(material, 750, Unit.Gram, null);
                    Storage3dItem? newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.That(newItem?.Amount == startAmount + 0.75);
                    transactions.Add(transaction1);

                    // Just to check if the unit conversion is working
                    Storage3dTransaction transaction2 = location.TakeFromStock(material, 0.0005, Unit.MetricTons, null, false);
                    newItem = location.Items.FirstOrDefault(curItem => curItem.Material == material);
                    // Check if the addition was successfully
                    Assert.That(newItem?.Amount == startAmount + 0.75 - 0.5);
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

        [Test]
        public void TakeCalculationAmountFromStorageTest()
        {
            try
            {
                Calculation3d calculation = GetTestCalculation();
                Assert.That(calculation is not null);

                foreach (var material in calculation.Materials)
                {
                    Storage3dItem item = new()
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
                        Quantity = 1,
                    };
                    await DatabaseHandler.Instance.SetFileWithChildrenAsync(file);
                    File3d file2 = new()
                    {
                        Name = "Another cool file",
                        Volume = 23.64,
                        PrintTime = 0.55,
                        Quantity = 5,
                    };
                    await DatabaseHandler.Instance.SetFileWithChildrenAsync(file2);

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
                        File = file,
                        MaterialUsages = [new() { Material = material, PercentageValue = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    await DatabaseHandler.Instance.SetPrintInfoWithChildrenAsync(info);
                    Print3dInfo info2 = new()
                    {
                        File = file2,
                        // Multi-Material for one file
                        MaterialUsages = [new() { Material = material, PercentageValue = 0.5 }, new() { Material = material2, PercentageValue = 0.5 }],
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
                        Quantity = 1,
                    };
                    File3d file2 = new()
                    {
                        Name = "Another cool file",
                        Volume = 23.64,
                        PrintTime = 0.55,
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
                        File = file,
                        MaterialUsages = [new() { Material = material, PercentageValue = 1 }],
                        Printer = printer,
                        Items = [usage],
                    };
                    Print3dInfo info2 = new()
                    {
                        File = file2,
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
        public async Task RecurringDatabaseCalculationSaveAndLoadTest()
        {
            try
            {
                string databasePath = "testdatabase_recurring.db";
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }
                using DatabaseHandler handler = new DatabaseHandler.DatabaseHandlerBuilder()
                    .WithDatabasePath(databasePath)
                    .WithDefaultTables()
                    .Build();

                await Task.Delay(250);

                Calculation3d calc = GetTestCalculation();
                calc.Material = calc.Materials.First();
                calc.Printer = calc.Printers.First();

                await calc.CalculateCostsAsync();
                await handler.SetCalculationWithChildrenAsync(calc);
                Calculation3d calc2 = await handler.GetCalculationWithChildrenAsync(calc.Id);

                await Task.Delay(250);
                Assert.That(calc2 is not null);

                List<Item3d> items = await handler.GetItemsWithChildrenAsync();
                Assert.That(items?.Count > 0);

                Guid itemId = calc.AdditionalItems.FirstOrDefault().Item.Id;
                var item = await handler.GetItemWithChildrenAsync(itemId);
                calc2.Material = calc.Materials.First();
                calc2.Printer = calc.Printers.First();

                await calc2.CalculateCostsAsync();
                await Task.Delay(250);
                if (calc2 is not null)
                {
                    Assert.That(calc.MachineCosts == calc2.MachineCosts, "Machine costs differ");
                    Assert.That(calc.MaterialCosts == calc2.MaterialCosts, "Material costs differ");
                    Assert.That(calc.CalculatedMargin == calc2.CalculatedMargin, "Margin differs");
                    Assert.That(calc.CalculatedTax == calc2.CalculatedTax, "Tax differs");
                    Assert.That(calc.EnergyCosts == calc2.EnergyCosts, "Energy costs differ");
                    Assert.That(calc.CustomAdditionCosts == calc2.CustomAdditionCosts, "Custom addition costs differ");
                    Assert.That(calc.ItemsCost == calc2.ItemsCost, "Item costs differ");
                }
                else
                    Assert.Fail("Calculation could not be loaded from the database!");
                Assert.That(Math.Round(calc.TotalCosts, 5) == Math.Round(calc2.TotalCosts, 5), $"Total costs differ: {calc?.TotalCosts} != {calc2?.TotalCosts}");
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }
    }
}