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
        string key;

        [ObservableProperty]
        string jsonValue;

        [ObservableProperty]
        string valueType;
        partial void OnValueTypeChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
                Type = Type.GetType(value, throwOnError: false);
        }

        [ObservableProperty]
        [property: Ignore]
        Type type;
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
