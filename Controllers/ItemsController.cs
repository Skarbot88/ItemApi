using ItemApi.Repository;
using ItemAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItemApi.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly InMemRepo repository;

        public ItemsController()
        {
            repository = new InMemRepo(); 
        }

        //GET /items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems(); 
            return items;       
        }

        //GET /items/id
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound(); 
            }
            return Ok(item);
        }

    }
}