/*
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AndreasReitberger.Models.Database
{
    public class Database3d
    {
        #region Variables
        readonly SQLiteAsyncConnection database;
        #endregion

        #region Constructor
        public Database3d(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            InitTables();
        }
        #endregion

        #region Methods

        #region Private

        void InitTables()
        {
            database?.CreateTableAsync<Manufacturer>().Wait();
            database?.CreateTableAsync<Printer3d>().Wait();
            database?.CreateTableAsync<Material3d>().Wait();
            database?.CreateTableAsync<Workstep>().Wait();
            database?.CreateTableAsync<HourlyMachineRate>().Wait();
            database?.CreateTableAsync<Customer3d>().Wait();
            database?.CreateTableAsync<Calculation3d>().Wait();
        }

        async Task InitTablesAsync()
        {
            await database?.CreateTableAsync<Manufacturer>();
            await database?.CreateTableAsync<Printer3d>();
            await database?.CreateTableAsync<Material3d>();
            await database?.CreateTableAsync<Workstep>();
            await database?.CreateTableAsync<HourlyMachineRate>();
            await database?.CreateTableAsync<Customer3d>();
            await database?.CreateTableAsync<Calculation3d>();
        }

        #endregion

        #region Public

        public async Task<List<Printer3d>> GetPrintersAsync()
        {
            return await database.Table<Printer3d>().ToListAsync();
        }
        public Task<List<Printer3d>> GetPrinters()
        {
            return database.Table<Printer3d>().ToListAsync();
        }

        public async Task<Printer3d> GetPrinterAsync(Guid id)
        {
            return await database.Table<Printer3d>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }
        public Task<Printer3d> GetPrinter(Guid id)
        {
            return database.Table<Printer3d>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<int> SetPrinterAsync(Printer3d printer)
        {
            Printer3d item = await GetPrinterAsync(printer.Id);
            return item != null ?
                await database.UpdateAsync(printer) :
                await database.InsertAsync(printer);
        }
        public Task<int> SetPrinter(Printer3d printer)
        {
            Printer3d item = GetPrinter(printer.Id).Result;
            return item != null ?
                database.UpdateAsync(printer) :
                database.InsertAsync(printer);
        }

        public async Task<int> DeletePrinterAsync(Printer3d printer)
        {
            return await database.DeleteAsync(printer);
        }
        public Task<int> DeletePrinter(Printer3d printer)
        {
            return database.DeleteAsync(printer);
        }
        #endregion

        #endregion
    }
}
*/