using PMSAT.BusinessTier.Payload.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Guid> CreateNewProject(CreateNewProjectRequest request);
    }
}
