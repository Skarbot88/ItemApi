using ItemAPI.Entities;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace ItemApi.Repository
{

    public class InMemRepo : IItemsRepo
    {
        private readonly List<Item> items = new()
        {
            new Item {Id = Guid.NewGuid(), Name = "Potion", Price = 3, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Master Sword", Price = 300, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Training Sword", Price = 20, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 60, CreatedDate = DateTimeOffset.Now},
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(x => x.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(x => x.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(x => x.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask; 
        }
    }
}