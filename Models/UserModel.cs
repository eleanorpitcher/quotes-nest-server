using MongoDB.Bson; //Provides types used by MongoDB, like ObjectId
using MongoDB.Bson.Serialization.Attributes; //Lets you decorate your class properties with attributes to control how they are stored in MongoDB

namespace QuotesNestServer.Models;

public class User
{
    [BsonId] //Makes this property the primary key for the document in MongoDB
    [BsonRepresentation(BsonType.ObjectId)] //Converts between MongoDB's ObjectId type and C# string. Lets you work with the ID as a string in your code but store it as ObjectId in MongoDB
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Quote> Quotes { get; set; } = new List<Quote>(); //The () here is the constructor of the List<Tag>. It is a default constructor which creates an empty list.
}