using ItemAPI.Entities;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace ItemApi.Repository
{

    public class InMemRepo : IInMemRepo
    {
        private readonly List<Item> items = new()
        {
            new Item {Id = Guid.NewGuid(), Name = "Potion", Price = 3, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Master Sword", Price = 300, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Training Sword", Price = 20, CreatedDate = DateTimeOffset.Now},
            new Item {Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 60, CreatedDate = DateTimeOffset.Now},
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(x => x.Id == id).SingleOrDefault();
        }
    }
}