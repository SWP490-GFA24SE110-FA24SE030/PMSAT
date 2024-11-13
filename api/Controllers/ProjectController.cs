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
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectController(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var projects = await _projectRepo.GetAllAsync(); 

            var projectDto = projects.Select(s => s.ToProjectDto());

            return Ok(projectDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var project = await _projectRepo.GetByIdAsync(id);

            if (project == null) 
            {
                return NotFound();
            }

            return Ok(project.ToProjectDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectRequestDto projectDto) 
        {
            var projectModel = projectDto.ToProjectFromCreateDto();

            await _projectRepo.CreateAsync(projectModel);

            return CreatedAtAction(nameof(GetById), new { id = projectModel.Id}, projectModel.ToProjectDto());
        }

        
    }
}