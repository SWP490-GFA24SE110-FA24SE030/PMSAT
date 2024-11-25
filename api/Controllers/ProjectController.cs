using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Project;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/project")]
    
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() 
        {
            var projects = await _projectRepo.GetAllAsync(); 

            var projectDto = projects.Select(s => s.ToProjectDto());

            return Ok(projectDto);
        }

        [HttpGet("prjid={id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

            if (project == null) 
            {
                return NotFound();
            }

            return Ok(project.ToProjectDto());
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestDto projectDto) 
        {
            var projectModel = projectDto.ToProjectFromCreateDto();

            await _projectRepo.CreateAsync(projectModel);

            return CreatedAtAction(nameof(GetById), new { id = projectModel.Id}, projectModel.ToProjectDto());
        }

        [HttpPut]
        [Route("edit/prjid={id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectRequestDto updateProjectRequestDto)
        {
            var projectModel = await _projectRepo.UpdateByIdAsync(id, updateProjectRequestDto);

            if (projectModel == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete]
        [Route("delete/prjid={id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var projectModel = await _projectRepo.DeleteByIdAsync(id);

            if (projectModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("delete/all")]
        public async Task<IActionResult> DeleteAllAsync()
        {
            var projectModel = await _projectRepo.DeleteAllAsync();

            if (projectModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}