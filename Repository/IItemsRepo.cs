using ItemAPI.Entities;

namespace ItemApi.Repository
{
    public interface IItemsRepo
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();

        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
    
}