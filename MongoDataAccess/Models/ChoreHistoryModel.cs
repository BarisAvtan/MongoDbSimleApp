using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MongoDataAccess.Models
{
    public class ChoreHistoryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ChoreID { get; set; }

        public int ChoreText { get; set; }

  
        public DateTime? DateComplated { get; set; }

        public UserModel WhoCompleted { get; set; }

        public ChoreHistoryModel()
        {

        }

        public ChoreHistoryModel(ChoreHistoryModel chore)        {
           // Id = id;
            ChoreID = chore.ChoreID;
            ChoreText = chore.ChoreText;
            DateComplated = chore.DateComplated;
            WhoCompleted = chore.WhoCompleted;
        }

    }
}
