using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PMSAT.BusinessTier.Constants;
using PMSAT.BusinessTier.Enums;
using PMSAT.BusinessTier.Payload;
using PMSAT.BusinessTier.Payload.Projects;
using PMSAT.BusinessTier.Services.Interfaces;
using PMSAT.BusinessTier.Utils;
using PMSAT.DataTier.Models;
using PMSAT.DataTier.Paginate;
using PMSAT.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Services.Implements
{
    public class ProjectService : BaseService<ProjectService>, IProjectService
    {
        public ProjectService(IUnitOfWork<PmsatContext> unitOfWork, ILogger<ProjectService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Guid> CreateNewProject(CreateNewProjectRequest request)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Project>().InsertAsync(project);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) throw new BadHttpRequestException(MessageConstant.User.CreateUserFailed);
            return project.Id;
        }

        public Task<IPaginate<GetAllProjectReponse>> GetAllProject(PagingModel pagingModel)
        {
            var response = _unitOfWork.GetRepository<Project>().GetPagingListAsync(
                selector: project => new GetAllProjectReponse(project.Id,project.Name,EnumUtil.ParseEnum<ProjectStatus>(project.Status)),
                page: pagingModel.page,
                size: pagingModel.size,
                orderBy: x => x.OrderByDescending(x => x.Name)
                );

            return response;
        }
    }
}
