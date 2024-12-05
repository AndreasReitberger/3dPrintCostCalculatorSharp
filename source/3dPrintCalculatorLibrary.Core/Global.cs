
global using System.Xml.Serialization;
#if SQL
global using SQLite;
global using SQLiteNetExtensions.Attributes;
global using AndreasReitberger.Print3d.SQLite.Interfaces;
#else
global using AndreasReitberger.Print3d.Core.Interfaces;
#endif