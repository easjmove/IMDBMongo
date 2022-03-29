using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace IMDBMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("IMDB Mongo tests!");

            var client = new MongoClient();

            var databases = client.ListDatabases().ToList();

            //foreach (var database in databases)
            //{
            //    Console.WriteLine(database);
            //}

            var IMDB = client.GetDatabase("IMDB");

            var collections = IMDB.ListCollections().ToList();

            //foreach (var collection in collections)
            //{
            //    Console.WriteLine(collection);
            //}

            var titlesbasic = IMDB.GetCollection<BsonDocument>("TitlesBasic");

            var titleList = titlesbasic.Find("{}").Limit(10).ToList();

            var joinedList = titlesbasic
                .Aggregate()
                .Limit(10)
                .Lookup("principals", "tconst", "tconst", "principals")
                .ToList();
            
            foreach (var title in joinedList)
            {
                Console.WriteLine(title);
            }
        }
    }
}
