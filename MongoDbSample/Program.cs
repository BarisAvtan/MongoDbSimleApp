using MongoDB.Driver;
using MongoDbSample;
using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;

//add dependencies nuget setup mongo db driver 


//Bu kod bolokları çalıştıgında mongo db üzerinde simpleDB veritabanı oluşuruluyor.
//using MongoDB.Driver;
//using MongoDbSample;

//string connectionString = "mongodb://localhost:27017";
//string databaseName = "choreDB";
//string collectionName = "people";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel
//{
//    FirstName = "TestName",
//    LastName = "TestLastName"
//};

//await collection.InsertOneAsync(person);

//var results = await collection.FindAsync(_ => true);

//foreach (var result in results.ToList())
//{
//    Console.WriteLine($"{result.Id} : {result.FirstName} {result.LastName}");
//}

//PART -2- üst kısmı açıklama satırına çektik  MongoDataAccess projesini referans olarak bu projeye ekledik.

////////


ChoreDataAccess db = new ChoreDataAccess();

await db.CreateUser(user: new UserModel()
{
    FirstName = "TestDS",
    LastName = "TestLastName"

}
);

var users = await db.GetAllUsers();

var chore = new ChoreModel()
{
    AssignedTo = users.First(),
    ChoreText = "Test chore text",
    FrequencyInDays = 7
};

await db.CreateChore(chore);
