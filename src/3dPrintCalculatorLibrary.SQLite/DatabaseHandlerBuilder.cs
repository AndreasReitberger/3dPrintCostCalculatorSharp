﻿
using SQLite;
using System.Linq;

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

            public async Task<DatabaseHandler> BuildAsync()
            {
                if (!string.IsNullOrEmpty(_databaseHandler.DatabasePath))
                {
                    SQLiteConnectionString con =
                        new(_databaseHandler.DatabasePath, true, key: _databaseHandler.Passphrase);
                    _databaseHandler.DatabaseAsync = new(con);
                    _databaseHandler.IsInitialized = true;

                    List<Type> existingTables = [.. _databaseHandler.DatabaseAsync.TableMappings.Select(mapping => mapping.MappedType)];
                    List<Type> changedTables = [.. _databaseHandler.Tables.Except(existingTables)];

                    if (changedTables.Count > 0)
                    {
                        await _databaseHandler.CreateTablesAsync(changedTables);
                    }
                    else if (_databaseHandler.Tables?.Count == 0)
                        await _databaseHandler.InitTablesAsync();
                }
                return _databaseHandler;
            }

            public DatabaseHandler Build()
            {
                if (!string.IsNullOrEmpty(_databaseHandler.DatabasePath))
                {
                    SQLiteConnectionString con =
                        new(_databaseHandler.DatabasePath, true, key: _databaseHandler.Passphrase);
                    _databaseHandler.DatabaseAsync = new(con);
                    _databaseHandler.IsInitialized = true;

                    List<Type> existingTables = [.. _databaseHandler.DatabaseAsync.TableMappings.Select(mapping => mapping.MappedType)];
                    List<Type> changedTables = [.. _databaseHandler.Tables.Except(existingTables)];

                    if (changedTables.Count > 0)
                    {
                        _databaseHandler.CreateTablesAsync(changedTables);
                    }
                    else if (_databaseHandler.Tables?.Count == 0)
                        _databaseHandler.InitTables();
                }
                return _databaseHandler;
            }

            public DatabaseHandlerBuilder WithDatabasePath(string databasePath)
            {
                _databaseHandler.DatabasePath = databasePath;
                return this;
            }

            public DatabaseHandlerBuilder WithDefaultTables()
            {
                _databaseHandler.Tables?.AddRange(_databaseHandler.DefaultTables);
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
            public DatabaseHandlerBuilder WithPassphrase(string passphrase)
            {
                _databaseHandler.Passphrase = passphrase;
                return this;
            }

            #endregion
        }
    }
}
