

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbSample
{
    public class PersonModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
