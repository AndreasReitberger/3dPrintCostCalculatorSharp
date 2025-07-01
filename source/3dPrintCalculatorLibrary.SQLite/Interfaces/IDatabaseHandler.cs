using System.ComponentModel;

namespace AndreasReitberger.Print3d.SQLite.Interfaces
{
    public interface IDatabaseHandler : INotifyPropertyChanged, ICloneable, IDisposable
    {
        #region Properties
        public bool IsInitialized { get; }
        public string DatabasePath { get; }
        public string Passphrase { get; }
        public SQLiteAsyncConnection? DatabaseAsync { get; }
        #endregion

        #region Methods

        //#if DB_SYNC

        public void InitTables();
        //#else
        public Task InitTablesAsync();
        //#endif

        public void InitDatabase(string databasePath, string? passphrase);

        public Task InitDatabaseAsync(string databasePath, string? passphrase);

        public Task RebuildAllTableAsync();

        public Task DropAllTableAsync();

        public Task TryDropAllTableAsync();

        public Task ClearAllTableAsync();

        public Task TryClearAllTableAsync();
        #endregion
    }
}
