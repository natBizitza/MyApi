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
    public class UserProjectsController : ControllerBase
    {
        private readonly oesiaContext _context;

        public UserProjectsController(oesiaContext context)
        {
            _context = context;
        }

        // GET: api/UserProjects
        [HttpGet]
        public IEnumerable<UserProject> GetUserProject()
        {
            return _context.UserProject;
        }

        // GET: api/UserProjects/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProject([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProject = await _context.UserProject.FindAsync(id);

            if (userProject == null)
            {
                return NotFound();
            }

            return Ok(userProject);
        }

        // PUT: api/UserProjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProject([FromRoute] long id, [FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userProject.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProjectExists(id))
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

        // POST: api/UserProjects
        [HttpPost]
        public async Task<IActionResult> PostUserProject([FromBody] UserProject userProject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserProject.Add(userProject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserProject", new { id = userProject.Id }, userProject);
        }

        // DELETE: api/UserProjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProject([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userProject = await _context.UserProject.FindAsync(id);
            if (userProject == null)
            {
                return NotFound();
            }

            _context.UserProject.Remove(userProject);
            await _context.SaveChangesAsync();

            return Ok(userProject);
        }

        private bool UserProjectExists(long id)
        {
            return _context.UserProject.Any(e => e.Id == id);
        }
    }
}