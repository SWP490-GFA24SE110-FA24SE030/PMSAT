using api.Models;

namespace api.Interfaces
{
    public interface IWorkFlowRepository
    {
        Task<Workflow> CreateAsync(Workflow workflowModel);
    }
}
