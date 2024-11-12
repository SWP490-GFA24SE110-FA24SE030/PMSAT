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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Services.Implements
{
    public class ProjectService : IProjectService
    {
        
        private readonly PmsatContext _context;

        public ProjectService(PmsatContext context)
        {
            _context = context;
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

            // Insert the project into the database
            await _context.Projects.AddAsync(project);

            // Commit the transaction if applicable, depending on your ORM
            int rowsAffected = await _context.SaveChangesAsync(); // Replace with actual method to commit changes

            if (rowsAffected <= 0)
            {
                throw new InvalidOperationException("Failed to create project. Please try again later.");
            }

            // Return the ID of the created project
            return project.Id;
        }

        //public Task<GetAllProjectReponse> GetAllProject(PagingModel pagingModel)
        //{
        //    var response = _unitOfWork.GetRepository<Project>().GetPagingListAsync(
        //        selector: project => new GetAllProjectReponse(project.Id,project.Name,EnumUtil.ParseEnum<ProjectStatus>(project.Status)),
        //        page: pagingModel.page,
        //        size: pagingModel.size,
        //        orderBy: x => x.OrderByDescending(x => x.Name)
        //        );

        //    return response;
        //}
    }
}
