using MongoDB.Bson; //Provides types used by MongoDB, like ObjectId
using MongoDB.Bson.Serialization.Attributes; //Lets you decorate your class properties with attributes to control how they are stored in MongoDB

namespace QuotesNestServer.Models;

public class Quote
{
    [BsonId] //Makes this property the primary key for the document in MongoDB
    [BsonRepresentation(BsonType.ObjectId)] //Converts between MongoDB's ObjectId type and C# string. Lets you work with the ID as a string in your code but store it as ObjectId in MongoDB
    public string? Id { get; set; }
    public string Text { get; set; } = null!;
    public string? BookTitle { get; set; }
    public string? BookAuthor { get; set; }
    public bool isFavourite { get; set; }
    public List<string>? TagList { get; set; } = new();
    
    // [BsonRepresentation(BsonType.ObjectId)]
    // public string UserId { get; set; } = null!;
}