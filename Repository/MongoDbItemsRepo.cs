using System.Collections.Immutable;
using ItemApi.Controllers;
using ItemAPI.Entities;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ItemApi.Repository
{
    public class MongoDbItemsRepo : IItemsRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;
        private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;
        public MongoDbItemsRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public void CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item); 
        }

        public void DeleteItem(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            _itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return _itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = _filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            _itemsCollection.ReplaceOne(filter, item);
        }
    }
}