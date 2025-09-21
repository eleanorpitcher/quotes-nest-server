//Service layer of the app. Main job is to encapsulate data access logic (talking to MongoDB)
//How does it relate to the controller?
//The controller doesn't need to know how the database works - it just calls the service. Service is the middleman between controller and DB
//Having a service layer keeps the code clean, organised, and testable

using QuotesNestServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver; //Allows you to connect to the DB and perform CRUD operations

namespace QuotesNestServer.Services;

public class QuotesService
{
    private readonly IMongoCollection<Quote> _quotesCollection; //Declares a private field of the Quote type

    public QuotesService(IOptions<QuotesNestDatabaseSettings> quotesNestDatabaseSettings)
    {
        var mongoClient = new MongoClient(quotesNestDatabaseSettings.Value.ConnectionString); //Creates a new connection string to the server
        var mongoDatabase = mongoClient.GetDatabase(quotesNestDatabaseSettings.Value.DatabaseName); //Gets DB object from MongoDB. mongoDatabase now represents that database and allows access to its collections

        _quotesCollection = mongoDatabase.GetCollection<Quote>(quotesNestDatabaseSettings.Value.Collections["Quotes"]); //Retrieves quotes collection from DB

    }

    public async Task<List<Quote>> GetAsync() =>
        await _quotesCollection.Find(_ => true).ToListAsync(); //This is a lambda expression. _ is the parameter name. It fetches all quotes from the collection.

    public async Task<Quote?> GetAsync(string id) =>
        await _quotesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Quote newQuote) =>
        await _quotesCollection.InsertOneAsync(newQuote);

    public async Task UpdateAsync(string id, Quote updatedQuote) =>
        await _quotesCollection.ReplaceOneAsync(x => x.Id == id, updatedQuote);

    public async Task RemoveAsync(string id) =>
        await _quotesCollection.DeleteOneAsync(x => x.Id == id);

}