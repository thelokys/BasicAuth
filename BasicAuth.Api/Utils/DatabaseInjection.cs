namespace BasicAuth.Api.Utils
{
    using System;
    using BasicAuth.Api.Configs;
    using BasicAuth.Api.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public static class DatabaseInjection
    {
        public static void UseMongoDatabase(this IServiceCollection services,
            IConfiguration Configuration)
        {

            services.Configure<MongoDbSettings>(
                Configuration.GetSection(nameof(MongoDbSettings))
            );
            services.AddSingleton<MongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            var settings = Configuration
                .GetSection(nameof(MongoDbSettings))
                .Get<MongoDbSettings>();

            services.AddSingleton<IMongoDatabase>(
                new MongoClient(settings.ToString()).GetDatabase(settings.Database)
            );
            services.AddSingleton<User>();
        }
    }
}
