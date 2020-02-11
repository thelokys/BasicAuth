namespace BasicAuth.Api.Models
{
    using System.ComponentModel.DataAnnotations;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Newtonsoft.Json;

    public class User
    {
        private IMongoDatabase _collection;

        internal IMongoCollection<User> Users
        {
            get => this._collection?.GetCollection<User>("users");
        }

        public User() { }

        public User(IMongoDatabase collection)
        {
            this._collection = collection;
        }

        [JsonConstructor]
        [BsonConstructor]
        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "[{0}] é obrigatório.")]
        [EmailAddress(ErrorMessage = "Campo em formato de email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "[{0}] é obrigatório.")]
        [StringLength(15,
            ErrorMessage = "[{0}] deve ter no mínimo {2} caracteres e no máximo {1}.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }
    }
}
