﻿using AndreasReitberger.Print3d.SQLite.CalculationAdditions;
using AndreasReitberger.Print3d.SQLite.CustomerAdditions;
using AndreasReitberger.Print3d.SQLite.Events;
using AndreasReitberger.Print3d.SQLite.FileAdditions;
using AndreasReitberger.Print3d.SQLite.Interfaces;
using AndreasReitberger.Print3d.SQLite.MaintenanceAdditions;
using AndreasReitberger.Print3d.SQLite.MaterialAdditions;
using AndreasReitberger.Print3d.SQLite.PrinterAdditions;
using AndreasReitberger.Print3d.SQLite.ProcedureAdditions;
using AndreasReitberger.Print3d.SQLite.Settings;
using AndreasReitberger.Print3d.SQLite.StorageAdditions;
using AndreasReitberger.Print3d.SQLite.WorkstepAdditions;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler : ObservableObject, IDatabaseHandler
    {
        #region Instance
        static DatabaseHandler? _instance;
        static readonly object Lock = new();
        public static DatabaseHandler Instance
        {
            get
            {
                lock (Lock)
                {
                    _instance ??= new DatabaseHandler();
                }
                return _instance;
            }

            set
            {
                if (_instance == value) return;
                lock (Lock)
                {
                    _instance = value;
                }
            }

        }
        #endregion

        #region Properties
        [ObservableProperty]
        bool isInitialized = false;

        [ObservableProperty]
        string databasePath = "";

        [ObservableProperty]
        SQLiteConnection database;

        [ObservableProperty]
        SQLiteAsyncConnection databaseAsync;

        [ObservableProperty]
        List<Action> delegates = [];

        [ObservableProperty]
        List<Type> tables = [];

        [ObservableProperty]
        List<Type> defaultTables = [
            typeof(Manufacturer),
            typeof(Supplier),
            typeof(Printer3d),
            typeof(Printer3dAttribute),
            typeof(Printer3dSlicerConfig),
            typeof(Maintenance3d),
            typeof(Sparepart),
            typeof(Material3dColor),
            typeof(Material3dType),
            typeof(Material3dAttribute),
            typeof(Material3dProcedureAttribute),
            typeof(Material3dUsage),
            typeof(Material3d),
            typeof(WorkstepCategory),
            typeof(Workstep),
            typeof(HourlyMachineRate),
            typeof(Customer3d),
            typeof(CustomAddition),
            typeof(CalculationAttribute),
            typeof(CalculationProcedureAttribute),
            typeof(CalculationProcedureParameter),
            typeof(CalculationProcedureParameterAddition),
            typeof(Calculation3dEnhanced),
            typeof(CustomAdditionCalculation3dEnhanced),
            typeof(WorkstepUsageCalculation3dEnhanced),
            typeof(File3d),
            typeof(File3dUsage),
            typeof(ModelWeight),
            typeof(Address),
            typeof(Email),
            typeof(PhoneNumber),
            typeof(ContactPerson),
            typeof(Calculation3dProfile),
            typeof(WorkstepUsage),
            typeof(WorkstepUsageParameter),
            typeof(Item3d),
            typeof(Item3dUsage),
            typeof(Storage3dLocation),
            typeof(Storage3dTransaction),
            typeof(Storage3dItem),
            typeof(Storage3d),
            typeof(Storage3dLocationStorage3d),
            typeof(Storage3dItemStorage3dLocation),
            typeof(Storage3dItemStorage3dTransaction),
            typeof(ProcedureAddition),
            typeof(ProcedureCalculationParameter),
            typeof(Print3dInfo),

            typeof(DatabaseSettingsKeyValuePair)
        ];

        [ObservableProperty]
        Func<Type, Task> onDatabaseOpertions;
        #endregion

        #region Collections

        [ObservableProperty]
        List<Calculation3dEnhanced> enhancedCalculations = [];

        [ObservableProperty]
        List<Printer3d> printers = [];
        partial void OnPrintersChanged(List<Printer3d> value)
        {
            OnPrintersChangedEvent(new PrintersChangedDatabaseEventArgs()
            {
                Printers = value,
            });
        }

        [ObservableProperty]
        List<Material3d> materials = [];
        partial void OnMaterialsChanged(List<Material3d> value)
        {
            OnMaterialsChangedEvent(new MaterialsChangedDatabaseEventArgs()
            {
                Materials = value,
            });
        }

        [ObservableProperty]
        List<Customer3d> customers = [];
        partial void OnCustomersChanged(List<Customer3d> value)
        {
            OnCustomersChangedEvent(new CustomersChangedDatabaseEventArgs()
            {
                Customers = value,
            });
        }

        [ObservableProperty]
        List<File3d> files = [];
        partial void OnFilesChanged(List<File3d> value)
        {
            OnFilesChangedEvent(new FilesChangedDatabaseEventArgs()
            {
                Files = value,
            });
        }

        [ObservableProperty]
        List<Workstep> worksteps = [];
        partial void OnWorkstepsChanged(List<Workstep> value)
        {
            OnWorkstepsChangedEvent(new WorkstepsChangedDatabaseEventArgs()
            {
                Worksteps = value,
            });
        }

        [ObservableProperty]
        List<WorkstepUsage> workstepUsages = [];
        partial void OnWorkstepUsagesChanged(List<WorkstepUsage> value)
        {
            /*
            OnWorkstepsChangedEvent(new WorkstepsChangedDatabaseEventArgs()
            {
                Worksteps = value,
            });
            */
        }

        [ObservableProperty]
        List<WorkstepUsageParameter> workstepUsageParameters = [];
        partial void OnWorkstepUsageParametersChanged(List<WorkstepUsageParameter> value)
        {
            /*
            OnWorkstepsChangedEvent(new WorkstepsChangedDatabaseEventArgs()
            {
                Worksteps = value,
            });
            */
        }

        [ObservableProperty]
        List<HourlyMachineRate> hourlyMachineRates = [];
        partial void OnHourlyMachineRatesChanged(List<HourlyMachineRate> value)
        {
            OnHourlyMachineRatesChangedEvent(new HourlyMachineRatesChangedDatabaseEventArgs()
            {
                HourlyMachineRates = value,
            });
        }
        #endregion

        #region Constructor
        public DatabaseHandler() { }
        public DatabaseHandler(string databasePath, bool updateInstance = true, string? passphrase = null)
        {
            // Docs: https://github.com/praeclarum/sqlite-net?tab=readme-ov-file#using-sqlcipher
            // Some examples: https://github.com/praeclarum/sqlite-net/blob/master/tests/SQLite.Tests/SQLCipherTest.cs
            SQLiteConnectionString connection = new(databasePath, true, key: passphrase);
            DatabaseAsync = new SQLiteAsyncConnection(connection);
            Database = new SQLiteConnection(connection);
            
            InitTables();
            IsInitialized = true;
            if (updateInstance) Instance = this;
        }
        #endregion

        #region Methods

        #region Public

        #region Init
        public void InitTables() => DefaultTables?.ForEach(type => Database?.CreateTable(type));

        public async Task InitTablesAsync() => DefaultTables?.ForEach(async type => await DatabaseAsync.CreateTableAsync(type));

        public void CreateTable(Type table) => Database?.CreateTable(table);

        public void CreateTables(List<Type> tables) => Database?.CreateTables(CreateFlags.None, tables?.ToArray());

        #endregion

        #region Delegates
        public async Task UpdateAllDelegatesAsync()
        {
            //var actions = Delegates.Select(task => new Task(task));
            List<Task> tasks = new(Delegates.Select(task => new Task(task)));
            await Task.WhenAll(tasks);
        }
        #endregion

        #region Database
        public void InitDatabase(string databasePath, string? passphrase = null)
        {
            SQLiteConnectionString connection = new(databasePath, true, key: passphrase);
            DatabaseAsync = new SQLiteAsyncConnection(connection);
            Database = new SQLiteConnection(connection);
            InitTables();
            IsInitialized = true;
            Instance = this;
        }

        public async Task InitDatabaseAsync(string databasePath, string? passphrase = null)
        {
            SQLiteConnectionString connection = new(databasePath, true, key: passphrase);
            DatabaseAsync = new SQLiteAsyncConnection(connection);
            Database = new SQLiteConnection(connection);
            await InitTablesAsync();
            IsInitialized = true;
            Instance = this;
        }

        public async Task CloseDatabaseAsync()
        {
            Database?.Close();
            await DatabaseAsync.CloseAsync();
        }

        public List<TableMapping>? GetTableMappings(string databasePath = "")
        {
            if (DatabaseAsync == null && !string.IsNullOrWhiteSpace(databasePath))
            {
                InitDatabase(databasePath);
            }
            return DatabaseAsync?.TableMappings.ToList();
        }

        public Task RebuildAllTableAsync() => InitTablesAsync();

        public async Task DropAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                await DatabaseAsync.DropTableAsync(mapping);
            }
        }

        public async Task TryDropAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                try
                {
                    await DatabaseAsync.DropTableAsync(mapping);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public async Task<bool> TryDropTableAsync(TableMapping mapping)
        {
            try
            {
                int result = await DatabaseAsync.DropTableAsync(mapping);
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task ClearAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                await DatabaseAsync.DeleteAllAsync(mapping);
            }
        }

        public async Task ClearTableAsync(TableMapping mapping)
        {
            await DatabaseAsync.DeleteAllAsync(mapping);
        }

        public async Task TryClearAllTableAsync()
        {
            foreach (TableMapping mapping in DatabaseAsync.TableMappings)
            {
                try
                {
                    await DatabaseAsync.DeleteAllAsync(mapping);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public Task BackupDatabaseAsync(string targetFolder, string databaseName) => DatabaseAsync.BackupAsync(targetFolder, databaseName);

        public void BackupDatabase(string targetFolder, string databaseName) => Database?.Backup(targetFolder, databaseName);

        public void RekeyDatabase(string newPassword)
        {
            // Bases on: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/encryption?tabs=netcore-cli
            SQLiteConnectionWithLock con = DatabaseAsync.GetConnection();
            SQLiteCommand command = con
                .CreateCommand(
                    "SELECT quote($newPassword);",
                    new Dictionary<string, object>() { { "$newPassword", newPassword } }
                    );
            string quotedNewPassword = command.ExecuteScalar<string>();
            command = con
                .CreateCommand(
                    $"PRAGMA rekey = {quotedNewPassword}"
                    );
            command.ExecuteNonQuery();
        }

        public void Close()
        {
            Database?.Close();
            DatabaseAsync?.CloseAsync();
        }

        public void Dispose() => Close();

        #endregion

        #region Static

        public static async Task<Tuple<T, TimeSpan?>> StopWatchFunctionAsync<T>(Func<T> action, bool inNewTask = false)
        {
            Stopwatch timer = new();
            timer.Start();
            T result;
            if (inNewTask)
            {
                result = await Task.Run(() =>
                {
                    return action();
                });
            }
            else
            {
                result = action();
            }
            timer.Stop();
            return new Tuple<T, TimeSpan?>(result, timer?.Elapsed);
        }

        public static T StopWatchFunction<T>(Func<T> action, out TimeSpan? duration)
        {
            Stopwatch timer = new();
            timer.Start();
            T result = action();

            timer.Stop();
            duration = timer?.Elapsed;

            return result;
        }

        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();

        #endregion

        #endregion

        #endregion
    }
}