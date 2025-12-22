using CommunityToolkit.Mvvm.ComponentModel;

#if SQL
namespace AndreasReitberger.Print3d.SQLite
{
    [Table($"{nameof(Email)}Addresses")]
#else
namespace AndreasReitberger.Print3d.Core
{
#endif
    public partial class Email : ObservableObject, IEmail
    {
        #region Properties
#if SQL
        [PrimaryKey]
#endif
        [ObservableProperty]
        public partial Guid Id { get; set; }

#if SQL
        [ForeignKey(typeof(Customer3d))]
#endif
        [ObservableProperty]
        public partial Guid CustomerId { get; set; }

        [ObservableProperty]
        public partial string EmailAddress { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public Email()
        {
            Id = Guid.NewGuid();
        }
        #endregion

        #region Override
        public override string ToString() => JsonSerializer.Serialize(this!, SourceGenerationContext.Default.Email);
        #endregion
    }
}
