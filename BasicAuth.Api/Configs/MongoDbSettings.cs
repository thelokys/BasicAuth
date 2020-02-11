namespace BasicAuth.Api.Configs
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MongoDbSettings
    {

        public MongoDbSettings() { }


        public MongoDbSettings(string driver, string register, string username,
            string password, string host, string database, string options)
        {
            this.Driver = driver;
            this.Register = register;
            this.Username = username;
            this.Password = password;
            this.Host = host;
            this.Database = database;
            this.Options = options;
        }

        public string Driver { get; set; }

        public string Register { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public string Database { get; set; }

        public string Options { get; set; }

        /// <summary>
        /// Monta através do atributos a conexão do mongo
        /// /// </summary>
        /// <returns>Retorna a conexão completa do mongodb</returns>
        public override string ToString()
        {
            return $@"{Driver}+{Register}://{Username}:{Password}@{Host}/{Database}?{Options}";
        }
    }
}
