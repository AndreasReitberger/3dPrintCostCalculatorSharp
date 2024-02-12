using AndreasReitberger.Print3d.Core.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndreasReitberger.Print3d.Core
{
    public partial class Email : ObservableObject, IEmail
    {
        #region Properties
        [ObservableProperty]
        Guid id;

        [ObservableProperty]
        Guid customerId;

        [ObservableProperty]
        string emailAddress = string.Empty;
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion
    }
}
