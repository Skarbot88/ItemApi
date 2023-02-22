using ItemApi.Dtos;
using ItemApi.Extensions;
using ItemApi.Repository;
using ItemAPI.Dtos;
using ItemAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.SignalR.Protocol;

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

        //POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new Item()
            {
                Id= Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem),new {id = item.Id},item.AsDto());    
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };

            _repository.UpdateItem(updatedItem);

            return NoContent(); 
        }

        //DELETE /items/{id}
        [HttpDelete("{id}")]
         public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            _repository.DeleteItem(existingItem.Id);

            return NoContent();
        }

    }
}