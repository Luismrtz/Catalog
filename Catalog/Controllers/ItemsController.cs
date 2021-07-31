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
        private readonly InMemItemsRepository repository;

        public ItemsController()
        {
            repository = new InMemItemsRepository();
        }
        // Gets ALL items
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repository.GetItems();
            return items;
        }
        // need to provide a template to get the ID. 
        [HttpGet("{id}")]
        

        //ActionResult<wrapped method name> allows us to return MORE than one type from this method
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            // to create a proper status-code(error) for NOT FOUND 
               if(item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
