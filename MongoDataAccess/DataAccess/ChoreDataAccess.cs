using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess
{
    public class ChoreDataAccess
    {
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "choreDB";
        private const string ChoreCollectionName = "choreChart";
        private const string UserCollectionName = "users";
        private const string ChoreHistoryCollection = "choreHistory";

        private IMongoCollection<T>ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DatabaseName);
            return db.GetCollection<T>(collection);
           
        }

        public async Task<List<UserModel>> GetAllUsers()
{
            var userCollection = ConnectToMongo<UserModel>(UserCollectionName);
            var result  = await userCollection.FindAsync(_ => true);
            return result.ToList();
        }


        public async Task<List<ChoreModel>> GetAllChores()
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            var result = await choresCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task<List<ChoreModel>> GetAllChoresForAUser(UserModel user)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            var result = await choresCollection.FindAsync(c=>c.AssignedTo.Id == user.Id);
            return result.ToList();
        }

        public Task CreateUser(UserModel user)
        {
            var userCollection = ConnectToMongo<UserModel>(UserCollectionName);
            return userCollection.InsertOneAsync(user);
        }

        public Task CreateChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            return choresCollection.InsertOneAsync(chore);
        }

        public Task UpdatedChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);
            return choresCollection.ReplaceOneAsync(filter, chore, options : new ReplaceOptions { IsUpsert = true});
        }

        public Task DeleteChore(ChoreModel chore)
        {
            var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            return choresCollection.DeleteOneAsync(c => c.Id == chore.Id);
        }

        public async Task ComplateChore(ChoreModel chore)
        {
            //var choresCollection = ConnectToMongo<ChoreModel>(ChoreCollectionName);
            //var filter = Builders<ChoreModel>.Filter.Eq(field: "Id", chore.Id);
            //await choresCollection.ReplaceOneAsync(filter,chore);

            //var choreHistoryCollection = ConnectToMongo<ChoreHistoryModel>(ChoreHistoryCollection);
            //await choreHistoryCollection.InsertOneAsync(document: new ChoreHistoryModel(chore));

            var client = new MongoClient(ConnectionString);
            using var session = await client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                var db = client.GetDatabase(DatabaseName);
                var choresCollection = db.GetCollection<ChoreModel>(ChoreCollectionName);
                var filter = Builders<ChoreModel>.Filter.Eq(field: "Id", chore.Id);
                await choresCollection.ReplaceOneAsync(filter, chore);
                var choreHistoryCollection = ConnectToMongo<ChoreHistoryModel>(ChoreHistoryCollection);
                // await choreHistoryCollection.InsertOneAsync(document: new ChoreHistoryModel(chore));
                await session.CommitTransactionAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
