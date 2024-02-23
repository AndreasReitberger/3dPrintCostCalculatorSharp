using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Realm;
using AndreasReitberger.Print3d.Realm.CalculationAdditions;
using AndreasReitberger.Print3d.Realm.MaterialAdditions;
using AndreasReitberger.Print3d.Realm.StorageAdditions;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using AndreasReitberger.Print3d.Realm.ProcedureAdditions;
using AndreasReitberger.Print3d.Realm.CustomerAdditions;
using NUnit.Framework;
using Realms;

namespace AndreasReitberger.NUnitTest
{
    public class TestsRealm
    {
        bool ApplyResinGlovesCosts = false;
        bool ApplyResinWashingCosts = false;
        bool ApplyResinFilterCosts = false;
        bool ApplyResinTankWearCosts = false;

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

            Customer3d costumer = new()
            {
                Name = "Max",
                LastName = "Mustermann",
                IsCompany = false,
            };
            costumer.EmailAddresses.Add(new Email()
            {
                EmailAddress = "max.mustermann@example.com"
            });
            costumer.Addresses.Add(new Address()
            {
                City = "Muserstadt",
                Zip = "93426",
                CountryCode = "de",
                Street = "Musterstraße 4",
            });

            // FDM job
            var pi1 = new Print3dInfo()
            {
                Name = "My awesome print job",
                File = new File3d()
                {
                    FileName = "my.gcode",
                    PrintTime = 5.263,
                    Volume = 35.54,
                    Quantity = 5,
                },
                Printer = printerFDM,
            };
            pi1.MaterialUsages.Add(new Material3dUsage()
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
            });
            pi1.MaterialUsages.Add(new Material3dUsage()
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
            });
            pi1.Items.Add(new Item3dUsage()
            {
                Item = new Item3d()
                {
                    Name = "M3x16 Screws",
                    PackagePrice = 20,
                    PackageSize = 100,
                },
                Quantity = 10,
            });
            
            // Resing job
            var pi2 = new Print3dInfo()
            {
                Name = "My first resin print job",
                File = new File3d()
                {
                    FileName = "Batman.dlp",
                    PrintTime = 2.65,
                    Volume = 65.546,
                    Quantity = 20,
                },
                Printer = printerDLP,
            };
            pi2.MaterialUsages.Add(new Material3dUsage()
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
            });
            
            var pi3 = new Print3dInfo()
            {
                Name = "My first resin print job",
                File = new File3d()
                {
                    FileName = "Superman.dlp",
                    PrintTime = 1.42,
                    Volume = 35.4536,
                    Quantity = 3,
                },
                Printer = printerDLP,
            };
            pi3.MaterialUsages.Add(new Material3dUsage()
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
            });

            calculation = new Calculation3dEnhanced()
            {
                Name = "Test",
                Customer = costumer,
                ApplyEnergyCost = true,
                PowerLevel = 75,
                EnergyCostsPerkWh = 0.4,
                FailRate = 5,
                State = CalculationState.Draft,
                ApplyProcedureSpecificAdditions = true,
            };
            calculation.PrintInfos.Add(pi1);
            calculation.PrintInfos.Add(pi2);
            calculation.PrintInfos.Add(pi3);

            calculation.WorkstepUsages.Add(new WorkstepUsage()
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
            });
            calculation.WorkstepUsages.Add(new WorkstepUsage()
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
            });

            calculation.AdditionalItems.Add(new Item3dUsage()
            {
                Item = new Item3d()
                {

                    Name = "Nuts M3",
                    PackageSize = 100,
                    PackagePrice = 9.99d,
                    Manufacturer = new Manufacturer()
                    {
                        Name = "Würth",
                        DebitorNumber = "DE26265126",
                        Website = "https://www.wuerth.de/",
                    },
                    SKU = "2302423-1223"
                },
                Quantity = 2,
            });
            calculation.AdditionalItems.Add(new Item3dUsage()
            {
                Item = new Item3d()
                {
                    Name = "Screws M3 20mm",
                    PackageSize = 50,
                    PackagePrice = 14.99d,
                    Manufacturer = new Manufacturer()
                    {
                        Name = "Würth",
                        DebitorNumber = "DE26265126",
                        Website = "https://www.wuerth.de/",
                    },
                    SKU = "2302423-6413"
                },
                Quantity = 2,
            });

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
                    var ad1 = new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.GloveCosts,
                        Value = 2d * (25d / 100d),
                    };
                    foreach (var add in additionalInfo)
                        ad1.Additions.Add(add);
                    parameters =
                    [
                        ad1
                    ];
                    var cpa1 = new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.GlovesCosts,
                        Family = Material3dFamily.Resin,
                        Level = CalculationLevel.Printer,
                        PerFile = false,
                        PerPiece = false,
                    };
                    // Causes an exception. Must be investigated
                    foreach(var para in parameters)
                        parameters.Add(para);
                    calculation.ProcedureAttributes.Add(cpa1);
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

                    var ad2 = new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.WashingCosts,
                        Value = 0.1d * (50d / 5d),
                    };
                    foreach (var add in additionalInfo)
                        ad2.Additions.Add(add);
                    parameters =
                    [
                        ad2
                    ];

                    var cpa2 = new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.WashingCosts,
                        Family = Material3dFamily.Resin,
                        Level = CalculationLevel.Printer,
                        PerPiece = true,
                        PerFile = true,
                    };

                    foreach (var para in parameters)
                        parameters.Add(para);
                    calculation.ProcedureAttributes.Add(cpa2);
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

                    var ad3 = new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.FilterCosts,
                        Value = 1d * (25d / 50d),
                    };
                    foreach (var add in additionalInfo)
                        ad3.Additions.Add(add);
                    parameters =
                    [
                        ad3
                    ];

                    var cpa3 = new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.FilterCosts,
                        Family = Material3dFamily.Resin,
                        Level = CalculationLevel.Printer,
                        PerFile = true,
                        PerPiece = false,
                    };
                    foreach (var para in parameters)
                        parameters.Add(para);
                    calculation.ProcedureAttributes.Add(cpa3);
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

                    var ad4 = new CalculationProcedureParameter()
                    {
                        Type = ProcedureParameter.ResinTankWearCosts,
                        Value = 120d * 0.01d,
                    };
                    foreach (var add in additionalInfo)
                        ad4.Additions.Add(add);
                    parameters =
                    [
                        ad4
                    ];
                    var cpa4 = new CalculationProcedureAttribute()
                    {
                        Attribute = ProcedureAttribute.ResinTankWear,
                        Family = Material3dFamily.Resin,
                        Level = CalculationLevel.Printer,
                        PerPiece = false,
                        PerFile = true,
                    };

                    foreach (var para in parameters)
                        parameters.Add(para);
                    calculation.ProcedureAttributes.Add(cpa4);
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
                string databasePath = "testdatabase.realm";

                // https://www.mongodb.com/docs/realm/sdk/dotnet/quick-start/
                var config = new RealmConfiguration(databasePath);
                // Start with a clear database
                if (File.Exists(config.DatabasePath))
                {
                    File.Delete(config.DatabasePath);
                }
                using var realm = await Realm.GetInstanceAsync(config);
                if (!realm.IsClosed)
                {
                    realm.Write(() => realm.RemoveAll());
                    // Manufacturers
                    Manufacturer prusa = new()
                    {
                        Name = "Prusa",
                        DebitorNumber = "CZ000001",
                        Website = "https://shop.prusa3d.com/en/#a_aid=AndreasReitberger",
                        IsActive = true,
                    };
                    realm.Write(() => realm.Add(prusa));
                    Manufacturer addedManufacturer = realm.Find<Manufacturer>(prusa.Id);
                    Assert.That(addedManufacturer is not null);
                    Assert.That(
                        prusa.Name == addedManufacturer.Name &&
                        prusa.DebitorNumber == addedManufacturer.DebitorNumber &&
                        prusa.Website == addedManufacturer.Website
                        );

                    List<Manufacturer> manufacturers = realm.All<Manufacturer>().ToList();
                    Assert.That(manufacturers?.Count > 0);

                    List<Material3dType> materialTypes = new()
                    {
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
                    };

                    List<Material3dType> types = realm.All<Material3dType>().ToList();
                    realm.Write(() => realm.Add(materialTypes));
                    types = realm.All<Material3dType>().ToList();
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
                    realm.Write(() => realm.Add(mhr));
                    HourlyMachineRate hourlyMachineRate = realm.Find<HourlyMachineRate>(mhr.Id);
                    List<HourlyMachineRate> hourlyMachineRates = realm.All<HourlyMachineRate>().ToList();
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
                    realm.Write(() => realm.Add(prusaXL));
                    Printer3d printer = realm.Find<Printer3d>(prusaXL.Id);
                    List<Printer3d> printers = realm.All<Printer3d>().ToList();
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
                    realm.Write(() => realm.Add(materialPETG));
                    Material3d material = realm.Find<Material3d>(materialPETG.Id);
                    List<Material3d> materials = realm.All<Material3d>().ToList();
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

                    realm.Write(() => realm.Add(new List<Item3d>() { item, item2 }));
                    List<Item3d> items = realm.All<Item3d>().ToList();
                    Assert.That(items?.Count == 2);

                    List<Item3dUsage> usages = new(items.Select(curItem => new Item3dUsage() { Item = curItem, Quantity = 10 }));
                    realm.Write(() => realm.Add(usages));

                    Calculation3d calculation = new()
                    {
                        Printer = printer,
                        Material = material,
                        //AdditionalItems = usages,
                    };
                    usages.ForEach(usage => calculation.AdditionalItems.Add(usage));
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

                    realm.Write(() => realm.Add(calculation));
                    Calculation3d calcFromDB = realm.Find<Calculation3d>(calculation.Id);
                    realm.Write(() => calcFromDB.CalculateCosts());
                    Assert.That(calculation.TotalCosts == calcFromDB.TotalCosts);

                    List<Calculation3d> calculations = realm.All<Calculation3d>().ToList();

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

                    realm.Write(() => realm.Add(calculation2));
                    Calculation3d calcFromDB2 = realm.Find<Calculation3d>(calculation2.Id);
                    Assert.That(calculation2.TotalCosts == calcFromDB2.TotalCosts);

                    calcFromDB = realm.Find<Calculation3d>(calculation.Id);
                    Assert.That(calculation.TotalCosts == calcFromDB.TotalCosts);

                    calculations = realm.All<Calculation3d>().ToList();

                    // Cleanup
                    realm.Write(() =>
                    {
                        realm.Remove(calculation);
                        //calculation = await DatabaseHandler.Instance.GetCalculationWithChildrenAsync(calculation.Id);
                        //Assert.IsNull(calculation);

                        realm.Remove(materialPETG);
                        //material = await DatabaseHandler.Instance.GetMaterialWithChildrenAsync(materialPETG.Id);
                        //Assert.IsNull(material);

                        realm.Remove(prusaXL);
                        //printer = await DatabaseHandler.Instance.GetPrinterWithChildrenAsync(prusaXL.Id);
                        //Assert.IsNull(printer);

                        realm.Remove(prusa);
                        //addedManufacturer = await DatabaseHandler.Instance.GetManufacturerWithChildrenAsync(prusa.Id);
                        //Assert.IsNull(addedManufacturer);
                    });
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
                    calculationObsolete.ItemsCosts,
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
                List<CalculationProcedureParameterAddition> additionalInfo = new()
                {
                    new CalculationProcedureParameterAddition("replacementcosts", 20),
                    new CalculationProcedureParameterAddition( "wearfactor", 0.5 ),
                };
                var nozzleWear = new CalculationProcedureParameter()
                {
                    Type = ProcedureParameter.NozzleWearCosts,
                    Value = 2,
                    //Additions = additionalInfo,
                };
                nozzleWear.AddRange(additionalInfo);
                List<CalculationProcedureParameter> parameters = new()
                {
                    nozzleWear
                };
                var attributes = new CalculationProcedureAttribute()
                {
                    Attribute = ProcedureAttribute.NozzleWear,
                    Family = Material3dFamily.Filament,
                    //Parameters = parameters,
                    Level = CalculationLevel.Printer,
                };
                attributes.AddRange(parameters);
                calculationObsolete.ProcedureAttributes.Add(attributes);

                calculationObsolete.CalculateCosts();

                CalculationAttribute wearCostAttribute = calculationObsolete.OverallPrinterCosts.FirstOrDefault(item =>
                item.LinkedId == calculationObsolete.Printer.Id &&
                item.Type == CalculationAttributeType.ProcedureSpecificAddition &&
                item.Attribute == "NozzleWearCosts"
                );
                Assert.That(wearCostAttribute is not null);

                // Updat calculation
                calculationObsolete.Printer = null;
                calculationObsolete.Printers.Add(printerSla);
                calculationObsolete.Printer = calculationObsolete.Printers[^1];

                calculationObsolete.CalculateCosts();
                wearCostAttribute = calculationObsolete.OverallPrinterCosts.FirstOrDefault(item =>
                item.LinkedId == calculationObsolete.Printer.Id &&
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

                calculationObsolete = new Calculation3d();
                // Add data
                calculationObsolete.Files.Add(file);
                calculationObsolete.Files.Add(file2);
                calculationObsolete.Files.Add(file3);
                calculationObsolete.Printers.Add(printer);
                calculationObsolete.Materials.Add(material);
                calculationObsolete.Materials.Add(material2);

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
        public async Task StorageDatabaseTest()
        {
            try
            {
                string databasePath = "testdatabase.realm";
                // https://www.mongodb.com/docs/realm/sdk/dotnet/quick-start/
                var config = new RealmConfiguration(databasePath);
                // Start with a clear database
                if (File.Exists(config.DatabasePath))
                {
                    File.Delete(config.DatabasePath);
                }
                using var realm = await Realm.GetInstanceAsync(config);
                if (!realm.IsClosed)
                {
                    realm.Write(() => realm.RemoveAll());
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
                    realm.Write(() => realm.Add(material));

                    Storage3dItem item = new()
                    {
                        Material = material,
                    };
                    realm.Write(() => realm.Add(item));
                    Storage3d storage = new()
                    {
                        Name = "Main material storage",
                    };
                    storage.Items.Add(item);
                    realm.Write(() =>
                    {
                        realm.Add(storage);
                        storage.AddToStock(material, 750, Unit.Gram);
                        var newItem = storage.Items.FirstOrDefault(curItem => curItem.Material.Id == material.Id);
                        // Check if the addition was successfully
                        Assert.That(newItem?.Amount == startAmount + 0.75);

                        // Just to check if the unit conversion is working
                        storage.TakeFromStock(material, 0.0005, Unit.MetricTons, false);
                        newItem = storage.Items.FirstOrDefault(curItem => curItem.Material.Id == material.Id);
                        // Check if the addition was successfully
                        Assert.That(newItem?.Amount == startAmount + 0.75 - 0.5);
                    });
                }
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
                MaterialFamily = Material3dFamily.Filament,
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
                MaterialFamily = Material3dFamily.Resin,
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

            calculationObsolete = new Calculation3d();
            // Add data
            calculationObsolete.AdditionalItems.Add(usage);
            calculationObsolete.Files.Add(file);
            calculationObsolete.Files.Add(file2);
            calculationObsolete.Printers.Add(printer);
            calculationObsolete.Printers.Add(printer2);
            calculationObsolete.Materials.Add(material);
            calculationObsolete.Materials.Add(material2);
            // Hardware replacement costs
            ProcedureAddition resinTank = new()
            {
                Name = "Resin Tank Replacement",
                Description = "Take the costs for the resin tank replacement into account?",
                Enabled = true,
                TargetFamily = Material3dFamily.Resin,
                Target = ProcedureAdditionTarget.Machine,
            };
            resinTank.Parameters.Add(
                new ProcedureCalculationParameter()
                {
                    Name = "Tank replacement costs",
                    Type = ProcedureCalculationType.ReplacementCosts,
                    Price = 50,
                    WearFactor = 1,
                    QuantityInPackage = 1,
                });
            double resinWearCosts = resinTank.CalculateCosts();
            Assert.That(resinWearCosts == 0.5d);
            // Consumable goods (like filters and gloves)
            ProcedureAddition gloves = new()
            {
                Name = "Gloves",
                Description = "Take the costs for the gloves?",
                Enabled = true,
                TargetFamily = Material3dFamily.Resin,
                Target = ProcedureAdditionTarget.General,
            };
            gloves.Parameters.Add(
                new ProcedureCalculationParameter()
                {
                    Name = "Gloves costs",
                    Type = ProcedureCalculationType.ConsumableGoods,
                    Price = 50,
                    AmountTakenForCalculation = 2,
                    QuantityInPackage = 100,
                });

            calculationObsolete.ProcedureAdditions.Add(resinTank);
            calculationObsolete.ProcedureAdditions.Add(gloves);

            // Add information
            calculationObsolete.FailRate = 25;
            calculationObsolete.EnergyCostsPerkWh = 0.30;
            calculationObsolete.ApplyEnergyCost = true;
            // Uses 75% of the max. power consumption set in the printer model (210 Watt)
            calculationObsolete.PowerLevel = 75;

            calculationObsolete.CalculateCosts();
            return calculationObsolete;
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
                    TargetFamily = Material3dFamily.Resin,
                    Target = ProcedureAdditionTarget.Machine,
                };
                resinTank.Parameters.Add(
                    new ProcedureCalculationParameter()
                    {
                        Name = "Tank replacement costs",
                        Type = ProcedureCalculationType.ReplacementCosts,
                        Price = 50,
                        WearFactor = 1,
                        QuantityInPackage = 1,
                    });
                double resinWearCosts = resinTank.CalculateCosts();
                Assert.That(resinWearCosts == 0.5d);
                // Consumable goods (like filters and gloves)
                ProcedureAddition gloves = new()
                {
                    Name = "Gloves",
                    Description = "Take the costs for the gloves?",
                    Enabled = true,
                    TargetFamily = Material3dFamily.Resin,
                };
                gloves.Parameters.Add(
                    new ProcedureCalculationParameter()
                    {
                        Name = "Gloves costs",
                        Type = ProcedureCalculationType.ConsumableGoods,
                        Price = 50,
                        AmountTakenForCalculation = 2,
                        QuantityInPackage = 100,
                    });
                double glovesCosts = gloves.CalculateCosts();
                Assert.That(glovesCosts == 1d);

                var calculation = GetTestCalculation();
                calculation.ProcedureAdditions.Add(resinTank);
                calculation.ProcedureAdditions.Add(gloves);
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
                        Name = "Würth",
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
                        Printer = printer,
                    };
                    info.MaterialUsages.Add(new() { Material = material, PercentageValue = 1 });
                    info.Items.Add(usage);

                    Print3dInfo info2 = new()
                    {
                        File = file2,
                        Printer = printer2,
                    };
                    info2.MaterialUsages.Add(new() { Material = material, PercentageValue = 0.5 });
                    info2.MaterialUsages.Add(new() { Material = material2, PercentageValue = 0.5 });

                    Calculation3dEnhanced calc = new()
                    {
                        Name = "Test Calculation",
                    };
                    calc.PrintInfos.Add(info);
                    calc.PrintInfos.Add(info2);

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
    }

}