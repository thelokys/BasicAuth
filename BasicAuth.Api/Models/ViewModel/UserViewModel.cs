using MongoDB.Driver;

namespace BasicAuth.Api.Models.ViewModel
{
    public class UserViewModel : User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserViewModel CopyProperties(User model)
        {
            this.Name = model.Name ?? this.Name;
            this.Email = model.Email ?? this.Email;
            this.Password = model.Password ?? this.Password;
            return this;
        }
    }
}
