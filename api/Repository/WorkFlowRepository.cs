using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        private readonly PmsatContext _context;

        public WorkFlowRepository(PmsatContext context)
        {
            _context = context;
        }

        public async Task<Workflow> CreateAsync(Workflow workflowModel)
        {
            await _context.Workflows.AddAsync(workflowModel);
            await _context.SaveChangesAsync();
            return workflowModel;
        }
    }
}
