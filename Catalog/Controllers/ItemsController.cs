using Catalog.DataTransObj;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Controllers
{

    [ApiController]
    //[Route("[controller]")] ... used if we want to call like //GET   /times... or we can specify the specific route I want to use
    [Route("items")]
    //controllerbase turns into a controller class
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository) //constructor links params to instance variable "repository"
        {
            this.repository = repository; // instance variable = constructor's paramater
        }
        // Gets ALL items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }


        // need to provide a template to get the ID. 
        [HttpGet("{id}")]
        //ActionResult<wrapped method name> allows us to return MORE than one type from this method
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            // to create a proper status-code(error) for NOT FOUND 
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }
        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }
        // PUT /items
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto) {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }
            //record type:"with- expression" creates an immutable copy of existingItem with the properties we listed to modify it
            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updatedItem);
            return NoContent();
        }
        // DELETE /items
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);

            return NoContent();
        }
    }
}
