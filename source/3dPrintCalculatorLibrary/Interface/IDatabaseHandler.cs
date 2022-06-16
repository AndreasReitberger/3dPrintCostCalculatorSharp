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
//#if DB_SYNC
        public SQLiteConnection Database { get; }
//#else
        public SQLiteAsyncConnection DatabaseAsync { get; }
//#endif
        #endregion

        #region Methods

//#if DB_SYNC

        public void InitTables();
//#else
        public Task InitTablesAsync();
//#endif

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
