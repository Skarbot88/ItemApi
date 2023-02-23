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
        private readonly IItemsRepo _repository;

        public ItemsController(IItemsRepo repository)
        {
            _repository = repository; 
        }

        //GET /items
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync())
                        .Select(x => x.AsDto()); 
            return items;       
        }

        //GET /items/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound(); 
            }
    
            return Ok(item.AsDto());
        }

        //POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new Item()
            {
                Id= Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };

            await _repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync),new {id = item.Id},item.AsDto());    
        }

        //PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };

            await _repository.UpdateItemAsync(updatedItem);

            return NoContent(); 
        }

        //DELETE /items/{id}
        [HttpDelete("{id}")]
         public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await _repository.GetItemAsync(id);

            if(existingItem is null)
            {
                return NotFound();
            }

            await _repository.DeleteItemAsync(existingItem.Id);

            return NoContent();
        }

    }
}