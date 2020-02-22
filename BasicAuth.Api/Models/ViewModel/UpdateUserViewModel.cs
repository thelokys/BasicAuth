namespace BasicAuth.Api.Models.ViewModel
{
    public class UpdateUserViewModel : User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ParseToUser(User parser)
        {
            parser.Name = this.Name ?? parser.Name;
            parser.Email = this.Email?? parser.Email;
            parser.Password = this.Password ?? parser.Password;

            return parser;
        }
    }
}
