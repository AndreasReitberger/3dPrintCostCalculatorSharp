using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.CustomerAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.ProcedureAdditions;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using SQLiteNetExtensionsAsync.Extensions;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler
    {
        #region Methods

        #region Public

        #region Manufacturers
        public Task<List<Manufacturer>> GetManufacturersWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Manufacturer>(recursive: true);

        public Task<Manufacturer> GetManufacturerWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Manufacturer>(id, recursive: true);

        public Task SetManufacturerWithChildrenAsync(Manufacturer manufacturer) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(manufacturer, recursive: true);

        public async Task SetManufacturersWithChildrenAsync(List<Manufacturer> manufacturers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(manufacturers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(manufacturers);
        }

        public Task<int> DeleteManufacturerAsync(Manufacturer manufacturer) => DatabaseAsync.DeleteAsync<Manufacturer>(manufacturer?.Id);

        #endregion

        #region Suppliers
        public Task<List<Supplier>> GetSuppliersWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Supplier>(recursive: true);

        public Task<Supplier> GetSupplierWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Supplier>(id, recursive: true);

        public Task SetSupplierWithChildrenAsync(Supplier supplier) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(supplier, recursive: true);

        public async Task SetSuppliersWithChildrenAsync(List<Supplier> suppliers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(suppliers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(suppliers);
        }

        public Task<int> DeleteSupplierAsync(Supplier supplier) => DatabaseAsync.DeleteAsync<Supplier>(supplier?.Id);

        #endregion

        #region MaterialTypes
        public Task<List<Material3dType>> GetMaterialTypesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Material3dType>(recursive: true);

        public List<Material3dType> GetMaterialTypes() => Database.Table<Material3dType>().ToList();

        public Task<Material3dType> GetMaterialTypeWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Material3dType>(id, recursive: true);

        public Material3dType GetMaterialType(Guid id) => Database.Table<Material3dType>().Where(i => i.Id == id).FirstOrDefault();

        public Task SetMaterialTypeWithChildrenAsync(Material3dType materialType) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(materialType, recursive: true);

        public int? SetMaterialType(Material3dType materialType)
        {
            Material3dType item = GetMaterialType(materialType.Id);
            return item != null ?
                Database?.Update(materialType) :
                Database?.Insert(materialType);
        }

        public async Task SetMaterialTypesWithChildrenAsync(List<Material3dType> materialTypes, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(materialTypes);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(materialTypes);
        }

        public Task<int> DeleteMaterialTypeAsync(Material3dType materialType) => DatabaseAsync.DeleteAsync<Material3dType>(materialType?.Id);

        public int? DeleteMaterialType(Material3dType materialType) => Database?.Delete<Material3dType>(materialType?.Id);

        #endregion

        #region Materials
        public async Task<List<Material3d>> GetMaterialsWithChildrenAsync()
        {
            // To trigger event
            Materials = await DatabaseAsync
                .GetAllWithChildrenAsync<Material3d>(recursive: true)
                ;
            return Materials;
        }

        public Task<Material3d> GetMaterialWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Material3d>(id, recursive: true);

        public Task SetMaterialWithChildrenAsync(Material3d material) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(material, recursive: true);

        public async Task SetMaterialsWithChildrenAsync(List<Material3d> materials, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(materials);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(materials);
        }

        public Task<int> DeleteMaterialAsync(Material3d material) => DatabaseAsync.DeleteAsync<Material3d>(material?.Id);

        #endregion

        #region Printers
        public async Task<List<Printer3d>> GetPrintersWithChildrenAsync()
        {
            Printers = await DatabaseAsync
                .GetAllWithChildrenAsync<Printer3d>(recursive: true)
                ;
            return Printers;
        }

        public Task<Printer3d> GetPrinterWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Printer3d>(id, recursive: true);

        public async Task SetPrintersWithChildrenAsync(List<Printer3d> printers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(printers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(printers);
        }

        public async Task SetPrinterWithChildrenAsync(Printer3d printer)
        {
            if (printer?.SlicerConfig != null)
            {
                await SetSlicerConfigWithChildrenAsync(printer.SlicerConfig).ConfigureAwait(false);
            }
            await DatabaseAsync
                .InsertOrReplaceWithChildrenAsync(printer, recursive: true)
                ;
        }

        public Task<int> DeletePrinterAsync(Printer3d printer) => DatabaseAsync.DeleteAsync<Printer3d>(printer?.Id);

        #endregion

        #region Maintenance
        public Task<List<Maintenance3d>> GetMaintenancesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Maintenance3d>(recursive: true);

        public Task<Maintenance3d> GetMaintenanceWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Maintenance3d>(id, recursive: true);

        public Task SetMaintenanceWithChildrenAsync(Maintenance3d maintenance) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(maintenance, recursive: true);

        public async Task SetMaintenancesWithChildrenAsync(List<Maintenance3d> maintenances, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(maintenances);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(maintenances);
        }

        public Task<int> DeleteMaintenanceAsync(Maintenance3d maintenance) => DatabaseAsync.DeleteAsync<Maintenance3d>(maintenance.Id);

        public async Task<int[]> DeleteMaintenancesAsync(List<Maintenance3d> maintenances)
        {
            Stack<int> results = new();
            for (int i = 0; i < maintenances?.Count; i++)
            {
                results.Push(await DatabaseAsync.DeleteAsync<Maintenance3d>(maintenances[i]?.Id));
            }
            return [.. results];
        }

        #endregion

        #region Customers
        public async Task<List<Customer3d>> GetCustomersWithChildrenAsync()
        {
            Customers = await DatabaseAsync
                .GetAllWithChildrenAsync<Customer3d>(recursive: true)
                ;
            return Customers;
        }

        public Task<Customer3d> GetCustomerWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Customer3d>(id, recursive: true);

        public async Task SetCustomerWithChildrenAsync(Customer3d customer) => await DatabaseAsync.InsertOrReplaceWithChildrenAsync(customer, recursive: true);

        public async Task SetCustomersWithChildrenAsync(List<Customer3d> customers, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(customers);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(customers);
        }

        public Task<int> DeleteCustomerAsync(Customer3d customer) => DatabaseAsync.DeleteAsync<Customer3d>(customer.Id);

        public async Task<int[]> DeleteCustomersAsync(List<Customer3d> customers)
        {
            Stack<int> results = new();
            for (int i = 0; i < customers?.Count; i++)
            {
                results.Push(await DatabaseAsync.DeleteAsync<Customer3d>(customers[i]?.Id));
            }
            return [.. results];
        }

        #endregion

        #region Files
        public async Task<List<File3d>> GetFilesWithChildrenAsync()
        {
            Files = await DatabaseAsync
                .GetAllWithChildrenAsync<File3d>(recursive: true)
                ;
            return Files;
        }

        public Task<File3d> GetFileWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<File3d>(id, recursive: true);

        public Task SetFileWithChildrenAsync(File3d file) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(file, recursive: true);

        public async Task SetFilesWithChildrenAsync(List<File3d> files, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(files);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(files);
        }

        public Task<int> DeleteFileAsync(File3d file) => DatabaseAsync.DeleteAsync<File3d>(file.Id);

        public async Task<int[]> DeleteFilesAsync(List<File3d> files)
        {
            Stack<int> results = new();
            for (int i = 0; i < files?.Count; i++)
            {
                results.Push(await DatabaseAsync.DeleteAsync<File3d>(files[i]?.Id));
            }
            return [.. results];
        }

        #endregion

        #region PrintInfos
        public Task<List<Print3dInfo>> GetPrintInfosWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Print3dInfo>(recursive: true);

        public Task<Print3dInfo> GetPrintInfoWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Print3dInfo>(id, recursive: true);

        public Task SetPrintInfoWithChildrenAsync(Print3dInfo file) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(file, recursive: true);

        public async Task SetPrintInfosWithChildrenAsync(List<Print3dInfo> files, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(files);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(files);
        }

        public Task<int> DeletePrintInfoAsync(Print3dInfo file) => DatabaseAsync.DeleteAsync<File3d>(file.Id);

        public async Task<int[]> DeletePrintInfosAsync(List<Print3dInfo> files)
        {
            Stack<int> results = new();
            for (int i = 0; i < files?.Count; i++)
            {
                results.Push(await DatabaseAsync.DeleteAsync<Print3dInfo>(files[i]?.Id));
            }
            return [.. results];
        }

        #endregion

        #region Addresses
        public Task<List<Address>> GetAddressesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Address>(recursive: true);
        
        public Task<Address> GetAddressWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Address>(id, recursive: true);

        public Task SetAddressWithChildrenAsync(Address address) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(address, recursive: true);

        public async Task SetAddressesWithChildrenAsync(List<Address> addresses, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(addresses);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(addresses);
        }

        public Task<int> DeleteAddressAsync(Address address) => DatabaseAsync.DeleteAsync<Address>(address?.Id);

        public async Task<int[]> DeleteAddressesAsync(List<Address> addresses)
        {
            Stack<int> results = new();
            for (int i = 0; i < addresses?.Count; i++)
            {
                int rowId = await DatabaseAsync.DeleteAsync<Address>(addresses[i]?.Id);
                results.Push(rowId);
            }
            return [.. results];
        }

        public Task<int> DeleteAllAddressesAsync() => DatabaseAsync.DeleteAllAsync<Address>();
        

        #endregion

        #region Calculations
        public async Task<List<Calculation3d>> GetCalculationsWithChildrenAsync()
        {
            List<Calculation3d> calculations = await DatabaseAsync.GetAllWithChildrenAsync<Calculation3d>(recursive: true);
            #if Workaround_96
            // Workaround, because the foreign keys from the printer / materials are not loaded somehow...
            /**/
            for (int i = 0; i < calculations?.Count; i++)
            {
                /**/
                Calculation3d calculation = calculations[i];
                for (int j = 0; j < calculation.Printers?.Count; j++)
                {
                    Printer3d printer = calculation.Printers[j];
                    calculation.Printers[j] = await GetPrinterWithChildrenAsync(printer.Id);
                }
                calculations[i].Printer =
                    calculations[i].Printers.FirstOrDefault(item => item.Id == calculations[i].Printer?.Id) ??
                    calculations[i].Printers.FirstOrDefault();

                for (int j = 0; j < calculation.Materials?.Count; j++)
                {
                    Material3d material = calculation.Materials[j];
                    calculation.Materials[j] = await GetMaterialWithChildrenAsync(material.Id);
                }
                calculations[i].Material =
                    calculations[i].Materials.FirstOrDefault(item => item.Id == calculations[i].Material?.Id) ??
                    calculations[i].Materials.FirstOrDefault();

                calculations[i]?.CalculateCosts();
            }
#endif
            return calculations;
        }

        public Task<Calculation3d> GetCalculationWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Calculation3d>(id, recursive: true);

        public async Task RerfreshCalculationsAsync()
        {
            Calculations = await GetCalculationsWithChildrenAsync();
            OnCalculationsChangedEvent(new CalculationChangedDatabaseEventArgs()
            {
                Calculations = Calculations,
            });
        }

        public async Task SetCalculationWithChildrenAsync(Calculation3d calculation, bool updateList = true)
        {
#if Workaround_96
            List<WorkstepUsage> workstepCollection = calculation.WorkstepUsages
                .Where(i => i is not null)
                .ToList()
                ;
            if (workstepCollection?.Count > 0)
                await SetWorkstepUsagesWithChildrenAsync(workstepCollection, replaceExisting: true);
            List<Item3dUsage> itemCollection = calculation.AdditionalItems
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetItemUsagesWithChildrenAsync(itemCollection, replaceExisting: true);
#endif
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(calculation, recursive: true);
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        public async Task SetCalculationsWithChildrenAsync(List<Calculation3d> calculations, bool replaceExisting = true, bool updateList = true)
        {
#if Workaround_96
            List<WorkstepUsage> itemCollection = calculations
                .SelectMany(i => i.WorkstepUsages)
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetWorkstepUsagesWithChildrenAsync(itemCollection, replaceExisting);
#endif
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(calculations);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(calculations);
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
        }

        public async Task<int> DeleteCalculationAsync(Calculation3d calculation, bool updateList = true)
        {
            int id = await DatabaseAsync.DeleteAsync<Calculation3d>(calculation?.Id);
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
            return id;
        }

        public async Task<int[]> DeleteCalculationsAsync(List<Calculation3d> calculations, bool updateList = true)
        {
            Stack<int> results = new();
            for (int i = 0; i < calculations?.Count; i++)
            {
                int rowId = await DatabaseAsync.DeleteAsync<Calculation3d>(calculations[i]?.Id);
                results.Push(rowId);
            }
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
            return [.. results];
        }

        public async Task<int> DeleteAllCalculationsAsync(bool updateList = true)
        {
            int id = await DatabaseAsync.DeleteAllAsync<Calculation3d>();
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
            return id;
        }

#endregion

        #region Calculations (Enhanced)
        public Task<List<Calculation3dEnhanced>> GetEnhancedCalculationsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Calculation3dEnhanced>(recursive: true);

        public Task<Calculation3dEnhanced> GetEnhancedCalculationWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Calculation3dEnhanced>(id, recursive: true);

        public async Task RerfreshEnhancedCalculationsAsync()
        {
            EnhancedCalculations = await GetEnhancedCalculationsWithChildrenAsync();
            OnCalculationsChangedEvent(new Calculation3dEnhancedChangedDatabaseEventArgs()
            {
                Calculations = EnhancedCalculations,
            });
        }

        public async Task SetEnhancedCalculationWithChildrenAsync(Calculation3dEnhanced calculation, bool updateList = true)
        {
#if Workaround_96
            List<WorkstepUsage> workstepCollection = calculation.WorkstepUsages
                .Where(i => i is not null)
                .ToList()
                ;
            if (workstepCollection?.Count > 0)
                await SetWorkstepUsagesWithChildrenAsync(workstepCollection, replaceExisting: true);
            List<Item3dUsage> itemCollection = calculation.AdditionalItems
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetItemUsagesWithChildrenAsync(itemCollection, replaceExisting: true);
#endif
            await DatabaseAsync.InsertOrReplaceWithChildrenAsync(calculation, recursive: true);
            if (updateList)
            {
                await RerfreshEnhancedCalculationsAsync();
            }
        }

        public async Task SetCalculationsWithChildrenAsync(List<Calculation3dEnhanced> calculations, bool replaceExisting = true, bool updateList = true)
        {
#if Workaround_96
            List<WorkstepUsage> itemCollection = calculations
                .SelectMany(i => i.WorkstepUsages)
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetWorkstepUsagesWithChildrenAsync(itemCollection, replaceExisting);
#endif
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(calculations);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(calculations);
            if (updateList)
            {
                await RerfreshEnhancedCalculationsAsync();
            }
        }

        public async Task<int> DeleteEnhancedCalculationAsync(Calculation3dEnhanced calculation, bool updateList = true)
        {
            int result = await DatabaseAsync.DeleteAsync<Calculation3dEnhanced>(calculation?.Id);
            if (updateList)
            {
                await RerfreshEnhancedCalculationsAsync();
            }
            return result;
        }

        public async Task<int[]> DeleteEnhancedCalculationsAsync(List<Calculation3dEnhanced> calculations, bool updateList = true)
        {
            Stack<int> results = new();
            for (int i = 0; i < calculations?.Count; i++)
            {
                int rowId = await DatabaseAsync.DeleteAsync<Calculation3dEnhanced>(calculations[i]?.Id);
                results.Push(rowId);
            }
            if (updateList)
            {
                await RerfreshCalculationsAsync();
            }
            return [.. results];
        }

        public async Task<int> DeleteAllEnhancedCalculationsAsync(bool updateList = true)
        {
            int result = await DatabaseAsync.DeleteAllAsync<Calculation3dEnhanced>();
            if (updateList)
            {
                await RerfreshEnhancedCalculationsAsync();
            }
            return result;
        }

        #endregion

        #region CalculationProfiles
        public Task<List<Calculation3dProfile>> GetCalculationProfilesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Calculation3dProfile>(recursive: true);

        public Task<Calculation3dProfile> GetCalculationProfileWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Calculation3dProfile>(id, recursive: true);

        public Task SetCalculationProfileWithChildrenAsync(Calculation3dProfile profile) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(profile, recursive: true);

        public async Task SetCalculationProfilesWithChildrenAsync(List<Calculation3dProfile> profiles, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(profiles);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(profiles);
        }

        public Task<int> DeleteCalculationProfileAsync(Calculation3dProfile profile) => DatabaseAsync.DeleteAsync<Calculation3dProfile>(profile?.Id);

        public async Task<int[]> DeleteCalculationProfilesAsync(List<Calculation3dProfile> profiles)
        {
            Stack<int> results = new();
            for (int i = 0; i < profiles?.Count; i++)
            {
                int rowId = await DatabaseAsync.DeleteAsync<Calculation3dProfile>(profiles[i]?.Id);
                results.Push(rowId);
            }
            return results.ToArray();
        }

        public async Task<int> DeleteAllCalculationProfilesAsync()
        {
            return await DatabaseAsync
                .DeleteAllAsync<Calculation3dProfile>()
                ;
        }

        #endregion

        #region Worksteps
        public async Task<List<Workstep>> GetWorkstepsWithChildrenAsync()
        {
            Worksteps = await DatabaseAsync
                .GetAllWithChildrenAsync<Workstep>(recursive: true)
                ;
            return Worksteps;
        }

        public Task<Workstep> GetWorkstepWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Workstep>(id, recursive: true);

        public Task SetWorkstepWithChildrenAsync(Workstep workstep) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(workstep, recursive: true);

        public async Task SetWorkstepsWithChildrenAsync(List<Workstep> worksteps, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(worksteps);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(worksteps);
        }

        public Task<int> DeleteWorkstepAsync(Workstep workstep) => DatabaseAsync.DeleteAsync<Workstep>(workstep?.Id);

        #endregion

        #region WorkstepsUsages
        public async Task<List<WorkstepUsage>> GetWorkstepUsagesWithChildrenAsync()
        {
            WorkstepUsages = await DatabaseAsync
                .GetAllWithChildrenAsync<WorkstepUsage>(recursive: true)
                ;
            return WorkstepUsages;
        }

        public Task<WorkstepUsage> GetWorkstepUsageWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<WorkstepUsage>(id, recursive: true);

        public Task SetWorkstepUsageWithChildrenAsync(WorkstepUsage workstepUsage) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(workstepUsage, recursive: true);

        public async Task SetWorkstepUsagesWithChildrenAsync(List<WorkstepUsage> workstepUsages, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(workstepUsages);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(workstepUsages);
        }

        public Task<int> DeleteWorkstepUsageAsync(WorkstepUsage workstepUsage) => DatabaseAsync.DeleteAsync<WorkstepUsage>(workstepUsage?.Id);

        #endregion

        #region WorkstepsUsageParameters
        public async Task<List<WorkstepUsageParameter>> GetWorkstepUsageParametersWithChildrenAsync()
        {
            WorkstepUsageParameters = await DatabaseAsync
                .GetAllWithChildrenAsync<WorkstepUsageParameter>(recursive: true)
                ;
            return WorkstepUsageParameters;
        }

        public Task<WorkstepUsageParameter> GetWorkstepUsageParameterWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<WorkstepUsageParameter>(id, recursive: true);

        public Task SetWorkstepUsageParametersWithChildrenAsync(WorkstepUsageParameter workstepUsageParameter) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(workstepUsageParameter, recursive: true);

        public async Task SeWorkstepUsageParametersWithChildrenAsync(List<WorkstepUsageParameter> workstepUsageParameters, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(workstepUsageParameters);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(workstepUsageParameters);
        }

        public Task<int> DeleteWorkstepUsageParameterAsync(WorkstepUsageParameter workstepUsageParameter) => DatabaseAsync.DeleteAsync<WorkstepUsageParameter>(workstepUsageParameter?.Id);

        #endregion

        #region Workstep Categories
        public Task<List<WorkstepCategory>> GetWorkstepCategoriesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<WorkstepCategory>(recursive: true);

        public Task<WorkstepCategory> GetWorkstepCategoryWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<WorkstepCategory>(id, recursive: true);
        public Task SetWorkstepCategoryWithChildrenAsync(WorkstepCategory workstepCategory) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(workstepCategory, recursive: true);

        public async Task SetWorkstepCategoriesWithChildrenAsync(List<WorkstepCategory> categories, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(categories);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(categories);
        }

        public Task<int> DeleteWorkstepCategoryAsync(WorkstepCategory workstepCategory) => DatabaseAsync.DeleteAsync<WorkstepCategory>(workstepCategory?.Id);

        #endregion

        #region Hourly Machine Rates

        public async Task<List<HourlyMachineRate>> GetHourlyMachineRatesWithChildrenAsync()
        {
            HourlyMachineRates = await DatabaseAsync
                .GetAllWithChildrenAsync<HourlyMachineRate>(recursive: true);
            return HourlyMachineRates;
        }

        public Task<HourlyMachineRate> GetHourlyMachineRateWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<HourlyMachineRate>(id, recursive: true);

        public async Task SetHourlyMachineRateWithChildrenAsync(HourlyMachineRate hourlyMachineRate, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceWithChildrenAsync(hourlyMachineRate, recursive: true);
            else
                await DatabaseAsync.InsertWithChildrenAsync(hourlyMachineRate);
        }

        public async Task SetHourlyMachineRatesWithChildrenAsync(List<HourlyMachineRate> hourlyMachineRates, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(hourlyMachineRates);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(hourlyMachineRates);
        }

        public Task<int> DeleteHourlyMachineRateAsync(HourlyMachineRate hourlyMachineRate) => DatabaseAsync.DeleteAsync<HourlyMachineRate>(hourlyMachineRate?.Id);

        #endregion

        #region Custom Addition

        public Task<List<CustomAddition>> GetCustomAdditionsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<CustomAddition>(recursive: true);

        public Task<CustomAddition> GetCustomAdditionWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<CustomAddition>(id, recursive: true);

        public Task SetCustomAdditionWithChildrenAsync(CustomAddition customAddition) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(customAddition, recursive: true);

        public async Task SetCustomAdditionsWithChildrenAsync(List<CustomAddition> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public Task<int> DeleteCustomAdditionAsync(CustomAddition customAddition) => DatabaseAsync.DeleteAsync<CustomAddition>(customAddition?.Id);

        #endregion

        #region Items
        public Task<List<Item3d>> GetItemsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Item3d>(recursive: true);

        public Task<Item3d> GetItemWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Item3d>(id, recursive: true);

        public Task SetItemWithChildrenAsync(Item3d item) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);

        public async Task SetItemsWithChildrenAsync(List<Item3d> items, bool replaceExisting = true)
        {
#if Workaround_96
            List<Manufacturer> itemCollection = items
                .Select(i => i.Manufacturer)
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetManufacturersWithChildrenAsync(itemCollection, replaceExisting);
#endif
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public Task<int> DeleteItemAsync(Item3d item) => DatabaseAsync.DeleteAsync<CustomAddition>(item?.Id);

        #endregion

        #region Item Usages
        public Task<List<Item3dUsage>> GetItemUsagesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Item3dUsage>(recursive: true);

        public Task<Item3dUsage> GetItemUsageWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Item3dUsage>(id, recursive: true);

        public Task SetItemUsageWithChildrenAsync(Item3dUsage item) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);

        public async Task SetItemUsagesWithChildrenAsync(List<Item3dUsage> items, bool replaceExisting = true)
        {
#if Workaround_96
            List<Item3d> itemCollection = items
                .Select(i => i.Item)
                .Where(i => i is not null)
                .ToList()
                ;
            if (itemCollection?.Count > 0)
                await SetItemsWithChildrenAsync(itemCollection, replaceExisting);
#endif
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public Task<int> DeleteItemUsageAsync(Item3dUsage itemUsage) => DatabaseAsync.DeleteAsync<Item3dUsage>(itemUsage?.Id);

        #endregion

        #region Storage Items
        public Task<List<Storage3dItem>> GetStorageItemsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Storage3dItem>(recursive: true);

        public Task<Storage3dItem> GetStorageItemWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Storage3dItem>(id, recursive: true);

        public Task SetStorageItemWithChildrenAsync(Storage3dItem item) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(item, recursive: true);

        public async Task SetStorageItemsWithChildrenAsync(List<Storage3dItem> items, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(items);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(items);
        }

        public Task<int> DeleteStorageItemAsync(Storage3dItem item) => DatabaseAsync.DeleteAsync<Storage3dItem>(item?.Id);

        #endregion

        #region Storage Transaction
        public Task<List<Storage3dTransaction>> GetStorageTransactionsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Storage3dTransaction>(recursive: true);
        public Task<List<Storage3dTransaction>> GetStorageTransactionsWithChildrenAsync(Storage3dItem item) => DatabaseAsync.GetAllWithChildrenAsync<Storage3dTransaction>(filter: transaction => transaction.StorageItemId == item.Id, recursive: true);

        public Task<Storage3dTransaction> GetStorageTransactionWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Storage3dTransaction>(id, recursive: true);

        public Task SetStorageTransactionWithChildrenAsync(Storage3dTransaction transaction) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(transaction, recursive: true);

        public async Task SetStorageTransactionsWithChildrenAsync(List<Storage3dTransaction> transactions, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(transactions);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(transactions);
        }

        public Task<int> DeleteStorageTransactionAsync(Storage3dTransaction transaction) => DatabaseAsync.DeleteAsync<Storage3dTransaction>(transaction?.Id);

        #endregion

        #region Storage Locations
        public Task<List<Storage3dLocation>> GetStorageLocationsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Storage3dLocation>(recursive: true);

        public Task<Storage3dLocation> GetStorageLocationWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Storage3dLocation>(id, recursive: true);

        public Task SetStorageLocationWithChildrenAsync(Storage3dLocation location) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(location, recursive: true);

        public async Task SetStorageLocationsWithChildrenAsync(List<Storage3dLocation> locations, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(locations);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(locations);
        }

        public Task<int> DeleteStorageLocationAsync(Storage3dLocation location) => DatabaseAsync.DeleteAsync<Storage3dLocation>(location?.Id);

        #endregion

        #region Storage
        public Task<List<Storage3d>> GetStoragesWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Storage3d>(recursive: true);

        public Task<Storage3d> GetStorageWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Storage3d>(id, recursive: true);

        public Task SetStorageWithChildrenAsync(Storage3d storage) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(storage, recursive: true);

        public async Task SetStoragesWithChildrenAsync(List<Storage3d> storages, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(storages);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(storages);
        }

        public Task<int> DeleteStorageAsync(Storage3d storage) => DatabaseAsync.DeleteAsync<Storage3d>(storage?.Id);

        #endregion

        #region SlicerConfigs

        public Task<List<Printer3dSlicerConfig>> GetSlicerConfigWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<Printer3dSlicerConfig>(recursive: true);

        public Task<Printer3dSlicerConfig> GetSlicerConfigWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<Printer3dSlicerConfig>(id, recursive: true);

        public Task SetSlicerConfigWithChildrenAsync(Printer3dSlicerConfig config) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(config, recursive: true);

        public async Task SetSlicerConfigsWithChildrenAsync(List<Printer3dSlicerConfig> configs, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(configs);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(configs);
        }

        public Task<int> DeleteSlicerConfigAsync(Printer3dSlicerConfig configs) => DatabaseAsync.DeleteAsync<Printer3dSlicerConfig>(configs?.Id);

        #endregion

        #region ProcedureAdditions

        public Task<List<ProcedureAddition>> GetProcedureAdditionsWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<ProcedureAddition>(recursive: true);

        public Task<ProcedureAddition> GetProcedureAdditionWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<ProcedureAddition>(id, recursive: true);

        public Task SetProcedureAdditionWithChildrenAsync(ProcedureAddition addition) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(addition, recursive: true);

        public async Task SetProcedureAdditionsWithChildrenAsync(List<ProcedureAddition> additions, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(additions);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(additions);
        }

        public Task<int> DeleteProcedureAdditionAsync(ProcedureAddition addition) => DatabaseAsync.DeleteAsync<ProcedureAddition>(addition?.Id);

        #endregion

        #region ProcedureAdditions

        public Task<List<ProcedureCalculationParameter>> GetCalculationProcedureParametersWithChildrenAsync() => DatabaseAsync.GetAllWithChildrenAsync<ProcedureCalculationParameter>(recursive: true);

        public Task<ProcedureCalculationParameter> GetCalculationProcedureParameterWithChildrenAsync(Guid id) => DatabaseAsync.GetWithChildrenAsync<ProcedureCalculationParameter>(id, recursive: true);

        public Task SetCalculationProcedureParameterWithChildrenAsync(ProcedureCalculationParameter parameter) => DatabaseAsync.InsertOrReplaceWithChildrenAsync(parameter, recursive: true);

        public async Task SetCalculationProcedureParametersWithChildrenAsync(List<ProcedureCalculationParameter> parameters, bool replaceExisting = true)
        {
            if (replaceExisting)
                await DatabaseAsync.InsertOrReplaceAllWithChildrenAsync(parameters);
            else
                await DatabaseAsync.InsertAllWithChildrenAsync(parameters);
        }

        public Task<int> DeleteCalculationProcedureParameterAsync(ProcedureCalculationParameter parameter) => DatabaseAsync.DeleteAsync<ProcedureCalculationParameter>(parameter?.Id);

        #endregion

        #endregion

        #endregion
    }
}
