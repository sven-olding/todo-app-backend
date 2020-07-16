using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ToDoItemsController : ControllerBase
  {
    private readonly ToDoContext _context;

    public ToDoItemsController(ToDoContext context)
    {
      _context = context;
    }

    // GET: api/ToDoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemDTO>>> GetToDoItems()
    {
      return await _context.ToDoItems
             .Select(x => ItemToDTO(x))
             .ToListAsync();
    }

    // GET: api/ToDoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDTO>> GetToDoItem(long id)
    {
      var toDoItem = await _context.ToDoItems.FindAsync(id);

      if (toDoItem == null)
      {
        return NotFound();
      }

      return ItemToDTO(toDoItem);
    }

    // PUT: api/ToDoItems/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutToDoItem(long id, ToDoItemDTO toDoItem)
    {
      if (id != toDoItem.Id)
      {
        return BadRequest();
      }

      _context.Entry(toDoItem).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ToDoItemExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/ToDoItems
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<ToDoItemDTO>> PostToDoItem(ToDoItemDTO toDoItem)
    {
      var newItem = new ToDoItem
      {
        Text = toDoItem.Text,
        IsComplete = toDoItem.IsComplete
      };
      _context.ToDoItems.Add(newItem);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, ItemToDTO(newItem));
    }

    // DELETE: api/ToDoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(long id)
    {
      var toDoItem = await _context.ToDoItems.FindAsync(id);
      if (toDoItem == null)
      {
        return NotFound();
      }

      _context.ToDoItems.Remove(toDoItem);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ToDoItemExists(long id)
    {
      return _context.ToDoItems.Any(e => e.Id == id);
    }

    private static ToDoItemDTO ItemToDTO(ToDoItem todoItem) =>
        new ToDoItemDTO
        {
          Id = todoItem.Id,
          Text = todoItem.Text,
          IsComplete = todoItem.IsComplete
        };
  }
}
