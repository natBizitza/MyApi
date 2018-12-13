using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtasksController : ControllerBase
    {
        private readonly oesiaContext _context;

        public SubtasksController(oesiaContext context)
        {
            _context = context;
        }

        // GET: api/Subtasks
        [HttpGet]
        public IEnumerable<Subtask> GetSubtask()
        {
            return _context.Subtask;
        }

        // GET: api/Subtasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubtask([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subtask = await _context.Subtask.FindAsync(id);

            if (subtask == null)
            {
                return NotFound();
            }

            return Ok(subtask);
        }

        // PUT: api/Subtasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubtask([FromRoute] long id, [FromBody] Subtask subtask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subtask.Id)
            {
                return BadRequest();
            }

            _context.Entry(subtask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubtaskExists(id))
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

        // POST: api/Subtasks
        [HttpPost]
        public async Task<IActionResult> PostSubtask([FromBody] Subtask subtask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Subtask.Add(subtask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubtask", new { id = subtask.Id }, subtask);
        }

        // DELETE: api/Subtasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubtask([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subtask = await _context.Subtask.FindAsync(id);
            if (subtask == null)
            {
                return NotFound();
            }

            _context.Subtask.Remove(subtask);
            await _context.SaveChangesAsync();

            return Ok(subtask);
        }

        private bool SubtaskExists(long id)
        {
            return _context.Subtask.Any(e => e.Id == id);
        }
    }
}