using CRUD.Models;
using CRUD.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUD.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _items;

        public ItemService(IOptions<MongoDBConfig> settings)
        {
            var client = new MongoClient(settings.Value.Connection);
            var db = client.GetDatabase(settings.Value.Dbname);
            _items = db.GetCollection<Item>(settings.Value.ItemsCollection);
        }

        public async Task<List<Item>> GetAllAsync() =>
            await _items.Find(_ => true).ToListAsync();

        public async Task<Item?> GetByIdAsync(string id) =>
            await _items.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Item> CreateAsync(Item item)
        {
            await _items.InsertOneAsync(item);
            return item;
        }

        public async Task UpdateAsync(string id, Item item) =>
            await _items.ReplaceOneAsync(x => x.Id == id, item);

        public async Task DeleteAsync(string id) =>
            await _items.DeleteOneAsync(x => x.Id == id);
    }
}
