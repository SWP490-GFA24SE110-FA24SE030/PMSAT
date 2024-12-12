using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Workflow> GetLatestWorkflowForTaskAsync(Guid taskId)
        {
            return await _context.Workflows
            .Where(w => w.TaskId == taskId)
            .OrderByDescending(w => w.UpdatedAt)
            .FirstOrDefaultAsync();
        }
    }
}
