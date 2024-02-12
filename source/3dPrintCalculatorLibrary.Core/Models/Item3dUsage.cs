using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace AndreasReitberger.Print3d.Core
{
    /// <summary>
    /// This is an additional item usage which can be added to the calculation job. 
    /// For instance, if you need to add screws or other material to the calculation.
    /// </summary>
    public partial class Item3dUsage : ObservableObject, IItem3dUsage
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        IItem3d? item;

        [ObservableProperty]
        double quantity;

        [ObservableProperty]
        IFile3d? file;
        partial void OnFileChanged(IFile3d? value)
        {
            LinkedToFile = value is not null;
        }

        [ObservableProperty]
        bool linkedToFile = false;
        #endregion

        #region Constructor
        public Item3dUsage()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Clone
        public object Clone() => MemberwiseClone();
        
        #endregion

        #region Overrides
        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
        
        public override bool Equals(object obj)
        {
            if (obj is not Item3dUsage item)
                return false;
            return Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}
