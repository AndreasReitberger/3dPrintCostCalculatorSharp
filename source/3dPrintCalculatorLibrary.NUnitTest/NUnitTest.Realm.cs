using AndreasReitberger.Print3d.Enums;
using AndreasReitberger.Print3d.Realm;
using AndreasReitberger.Print3d.Realm.CalculationAdditions;
using AndreasReitberger.Print3d.Realm.MaterialAdditions;
using AndreasReitberger.Print3d.Realm.StorageAdditions;
using AndreasReitberger.Print3d.Realm.WorkstepAdditions;
using AndreasReitberger.Print3d.Realm.ProcedureAdditions;
using AndreasReitberger.Print3d.Realm.CustomerAdditions;
using AndreasReitberger.Print3d.Realm.FileAdditions;
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
                Assert.That(calculation?.ProcedureAdditions?.Count > 0);
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

                    List<Material3dType> types = [.. realm.All<Material3dType>()];
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
                    Printer3d? printer = realm.Find<Printer3d>(prusaXL.Id);
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

                    // Cleanup
                    realm.Write(() =>
                    {
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

                if (calculation is not null)
                {
                    calculation.ProcedureAdditions.Add(resinTank);
                    calculation.ProcedureAdditions.Add(gloves);
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
                        FileUsage = fileUsage,
                        Printer = printer,
                    };
                    info.MaterialUsages.Add(new() { Material = material, PercentageValue = 1 });
                    info.Items.Add(usage);

                    Print3dInfo info2 = new()
                    {
                        FileUsage = fileUsage2,
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