using AndreasReitberger.Print3d.SQLite.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SQLite;

namespace AndreasReitberger.Print3d.SQLite.Settings
{
    [Table("settings")]
    public partial class DatabaseSettingsKeyValuePair : ObservableObject, IDatabaseSettingsKeyValuePair
    {
        #region Properties
        [ObservableProperty]
        [property: PrimaryKey]
        public string key;

        [ObservableProperty]
        public string jsonValue;

        [ObservableProperty]
        public string valueType;
        #endregion

        #region Constructor
        public DatabaseSettingsKeyValuePair() { }
        public DatabaseSettingsKeyValuePair(string key, string value)
        {
            Key = key;
            JsonValue = value;
            ValueType = value?.GetType().Name;
        }
        public DatabaseSettingsKeyValuePair(string key, object value)
        {
            Key = key;
            JsonValue = JsonConvert.SerializeObject(value);
            ValueType = value?.GetType().Name;
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
