
global using System.Xml.Serialization;
#if SQL
global using SQLite;
global using SQLiteNetExtensions.Attributes;
global using AndreasReitberger.Print3d.SQLite.Interfaces;
global using AndreasReitberger.Print3d.SQLite.SourceGenerator;
#else
global using AndreasReitberger.Print3d.Core.Interfaces;
global using AndreasReitberger.Print3d.Core.SourceGenerator;
#endif
#if NEWTONSOFT
global using Newtonsoft.Json;
#else
global using System.Text.Json;
global using System.Text.Json.Serialization;
#endif