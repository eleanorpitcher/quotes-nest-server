using MongoDB.Bson; //Provides types used by MongoDB, like ObjectId
using MongoDB.Bson.Serialization.Attributes; //Lets you decorate your class properties with attributes to control how they are stored in MongoDB

namespace QuotesNestServer.Models;

public class TagList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string TagName { get; set; } = null!;
}