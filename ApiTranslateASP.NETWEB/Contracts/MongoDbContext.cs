using ApiTranslateASP.NETWEB.Models;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Translation> YourCollection
    {
        get { return _database.GetCollection<Translation>("TRANSLATEAPP"); }
    }
}




