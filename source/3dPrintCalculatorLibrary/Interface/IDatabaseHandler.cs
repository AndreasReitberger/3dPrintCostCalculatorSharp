using SQLite;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AndreasReitberger.Interface
{
    public interface IDatabaseHandler : INotifyPropertyChanged, ICloneable, IDisposable
    {
        #region Properties
        public bool IsInitialized { get; }
        public string DatabasePath { get; }
        public SQLiteAsyncConnection DatabaseAsync { get; }
        public SQLiteConnection Database { get; }
        #endregion

        #region Methods

        public Task InitTablesAsync();
        public void InitTables();

        public void InitDatabase(string databasePath);

        public Task InitDatabaseAsync(string databasePath);

        public Task RebuildAllTableAsync();

        public Task DropAllTableAsync();

        public Task TryDropAllTableAsync();

        public Task ClearAllTableAsync();

        public Task TryClearAllTableAsync();
        #endregion
    }
}
