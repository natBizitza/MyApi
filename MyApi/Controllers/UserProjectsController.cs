using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using Newtonsoft.Json;

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
        public async Task<string> GetUserProjectAsync()
        {
            List<UserProject> userProjects = await _context.UserProject.ToListAsync();
            foreach (UserProject userProject in userProjects)
            {
                if (userProject.ProjectsId != null)
                {
                    userProject.Projects = _context.Project.Single(x => x.Id == userProject.ProjectsId);
                    foreach (Module module in _context.Module.Where(x => x.ProjectId == userProject.ProjectsId))
                    {

                        userProject.Projects.Module.Add(module);

                        foreach (Submodule submodule in _context.Submodule.Where(x => x.Id == module.Id))
                        {

                            userProject.Projects.Module.Single(x => x.Id == module.Id).Submodule.Add(submodule);

                            foreach (Tasks task in _context.Task.Where(x => x.Id == submodule.Id))
                            {
                                userProject.Projects.Module.Single(x => x.Id == module.Id).Submodule.Single(x => x.Id == submodule.Id).Task.Add(task);

                                foreach (Subtask subtask in _context.Subtask.Where(x => x.Id == task.Id))
                                {
                                    userProject.Projects.Module.Single(x => x.Id == module.Id).Submodule.Single(x => x.Id == submodule.Id).Task.Single(x => x.Id == task.Id).Subtask.Add(subtask);
                                }

                            }
                        }
                    }
                }
                if (userProject.AppUsersId != null)
                {
                    userProject.AppUsers = _context.AspNetUsers.Single(x => x.Id == userProject.AppUsersId);

                }
            }
            var json = JsonConvert.SerializeObject(userProjects, Formatting.None, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            });
            return json;
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