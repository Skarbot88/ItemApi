using ItemAPI.Entities;

namespace ItemApi.Repository
{
    public interface IInMemRepo
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
    
}