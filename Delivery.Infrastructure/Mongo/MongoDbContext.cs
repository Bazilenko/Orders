using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Delivery.Infrastructure.Mongo.Config;

namespace Delivery.Infrastructure.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public IMongoDatabase Database => _database;
        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            mongoSettings.MaxConnectionPoolSize = settings.Value.MaxConnectionPoolSize;
            mongoSettings.MinConnectionPoolSize = settings.Value.MinConnectionPoolSize;
            mongoSettings.ConnectTimeout = TimeSpan.FromSeconds(settings.Value.ConnectTimeoutSeconds);
            mongoSettings.SocketTimeout = TimeSpan.FromSeconds(settings.Value.SocketTimeoutSeconds);

            var client = new MongoClient(mongoSettings);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Courier> Couriers => _database.GetCollection<Courier>("couriers");
        public IMongoCollection<Domain.Entities.Delivery> Deliveries => _database.GetCollection<Domain.Entities.Delivery>("deliveries");
        public IClientSessionHandle StartSession() => _database.Client.StartSession();
    }
}
