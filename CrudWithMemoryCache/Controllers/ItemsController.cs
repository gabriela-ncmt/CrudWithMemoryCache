using CrudWithMemoryCache.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CrudWithMemoryCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private static readonly List<Item> _items = new List<Item>();

        public ItemsController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            //try to get from cache
            if(! _cache.TryGetValue("items", out List<Item> items))
            {
                //if its not on cache, it simulates a search of a database
                items = _items;       
                // caches the result with an expiration time of 5 minutes
                _cache.Set("items", items, TimeSpan.FromMinutes(5));
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            //try to get an cached item
            if (!_cache.TryGetValue($"item_{id}", out Item item))
            {
                //simulates a search on database
                item = _items.FirstOrDefault(i => i.Id == id);

                if (item == null)
                    return NotFound();

                //caches
                _cache.Set($"item_{id}", item, TimeSpan.FromMinutes(5));
            }

        return Ok(item);
        }

        [HttpPost]
        public IActionResult PostItem([FromBody] Item item)
        {
            if (item == null)
                return BadRequest();

            //add to the database (in this case, the list)
            item.Id = _items.Count + 1;
            _items.Add(item);

            //clean cache from all items
            _cache.Remove("items");

            return CreatedAtAction(nameof(GetItem), new {Id = item.Id}, item);
        }

        [HttpPut("{id}")]
        public IActionResult PutItem(int id,[FromBody] Item item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
                return NotFound();

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;

            //clean cache from all items
            _cache.Remove("items");

            //Update the cache of the specific item
            _cache.Set($"item_{id}", existingItem, TimeSpan.FromMinutes(5));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            _items.Remove(item);

            //clean cache from all items
            _cache.Remove("items");
            //clean specific item cache
            _cache.Remove($"item_{id}");

            return NoContent() ;
        }
    }
}
