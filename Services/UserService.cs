using QuotesNestServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace QuotesNestServer.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IOptions<QuotesNestDatabaseSettings> quotesNestDatabaseSettings)
    {
        var mongoClient = new MongoClient(quotesNestDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(quotesNestDatabaseSettings.Value.DatabaseName);

        _userCollection = mongoDatabase.GetCollection<User>(quotesNestDatabaseSettings.Value.Collections["Books"]);
    }
    public async Task<List<User>> GetAsync() =>
        await _userCollection.Find(_ => true).ToListAsync(); //This is a lambda expression. _ is the parameter name. It fetches all Books from the collection.

    public async Task<User?> GetAsync(string id) =>
        await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User newUser) =>
        await _userCollection.InsertOneAsync(newUser);

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _userCollection.DeleteOneAsync(x => x.Id == id);
}