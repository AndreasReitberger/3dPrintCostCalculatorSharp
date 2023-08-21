﻿
namespace AndreasReitberger.Print3d.SQLite
{
    public partial class DatabaseHandler
    {
        public class DatabaseHandlerBuilder
        {

            #region Instance
            readonly DatabaseHandler _databaseHandler = new();
            #endregion

            #region Method

            public DatabaseHandler Build()
            {
                if (!string.IsNullOrEmpty(_databaseHandler.DatabasePath))
                {
                    _databaseHandler.Database = new(_databaseHandler.DatabasePath);
                    _databaseHandler.DatabaseAsync = new(_databaseHandler.DatabasePath);
                    _databaseHandler.IsInitialized = true;
                    if (_databaseHandler.Tables?.Count > 0)
                    {
                        _databaseHandler.CreateTables(_databaseHandler.Tables);
                    }
                }
                return _databaseHandler;
            }

            public DatabaseHandlerBuilder WithDatabasePath(string databasePath)
            {
                _databaseHandler.DatabasePath = databasePath;
                return this;
            }

            public DatabaseHandlerBuilder WithTable(Type table)
            {
                _databaseHandler.Tables?.Add(table);
                return this;
            }

            public DatabaseHandlerBuilder WithTables(List<Type> tables)
            {
                _databaseHandler.Tables?.AddRange(tables);
                return this;
            }
            /// <summary>
            /// Performs the provided task, whenever a Database opertion (Delete, Update, Add) was performed.
            /// This can be used to sync data with Firebase, for instance.
            /// </summary>
            /// <param name="task"></param>
            /// <returns></returns>
            public DatabaseHandlerBuilder WithDatabaseOperationTask(Func<Type, Task> task)
            {
                _databaseHandler.OnDatabaseOpertions = task;
                return this;
            }

            #endregion
        }
    }
}
