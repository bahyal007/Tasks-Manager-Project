using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_part2_Server.DTOs;
using TaskModel;

namespace Project_part2_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoriesController : ControllerBase
    {
        private readonly TasksDatabaseContext _context;

        public TaskCategoriesController(TasksDatabaseContext context)
        {
            _context = context;
        }

        // GET: api/TaskCategories
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.Include(c => c.ToDoItems).ToListAsync();
        }

        // GET: api/TaskCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);
            return category is null ? NotFound() : category;
        }

        // GET: api/TaskCategories/5
        [HttpGet("CategoryTaskDetails/{id}")]
        public async Task<ActionResult<CategoryTaskDetails>> GetCategoryTaskDetails(int id)
        {
            CategoryTaskDetails? categoryTaskDetails = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryTaskDetails
                {
                    Id = c.Id,
                    Name = c.Name,
                    ToDoItems = c.ToDoItems.ToList()
                })
                .SingleOrDefaultAsync();

            if (categoryTaskDetails == null)
            {
                return NotFound();
            }
            else
            {
                return categoryTaskDetails;
            }
        }





        // PUT: api/TaskCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/TaskCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'TasksDatabaseContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/TaskCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            Category? category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
