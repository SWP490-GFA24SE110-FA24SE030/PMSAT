using Microsoft.AspNetCore.Mvc;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Payload;
using PMSAT.BusinessTier.Payload.Projects;
using PMSAT.BusinessTier.Services.Implements;
using PMSAT.BusinessTier.Services.Interfaces;
using PMSAT.DataTier.Models;
using PMSAT.DataTier.Paginate;

namespace PMSAT.API.Controllers
{
    [ApiController]
    public class ProjectController : BaseController<ProjectController>
    {
        private readonly IProjectService _projectService;

        public ProjectController(ILogger<ProjectController> logger, IProjectService projectService) : base(logger)
        {
            _projectService = projectService;
        }

        [HttpPost(ApiEndPointConstant.Project.CreateNewProjectEndPoint)]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(UnauthorizedObjectResult))]
        public async Task<IActionResult> CreateNewProject(CreateNewProjectRequest request)
        {
            var response = await _projectService.CreateNewProject(request);
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Project.ProjectsEndPoint)]
        [ProducesResponseType(typeof(IPaginate<GetAllProjectReponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProject([FromQuery] PagingModel paging)
        {
            var response = await _projectService.GetAllProject(paging);
            return Ok(response);
        }
    }
}
