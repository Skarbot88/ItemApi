using ItemApi.Extensions;
using ItemApi.Repository;
using ItemAPI.Dtos;
using ItemAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ItemApi.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IInMemRepo _repository;

        public ItemsController(IInMemRepo repository)
        {
            _repository = repository; 
        }

        //GET /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = _repository.GetItems().Select(x => x.AsDto()); 
            return items;       
        }

        //GET /items/id
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);
            if (item is null)
            {
                return NotFound(); 
            }
    
            return Ok(item.AsDto());
        }

    }
}