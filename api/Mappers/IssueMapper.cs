using api.Dtos.Issue;
using api.Models;

namespace api.Mappers
{
    public static class IssueMapper
    {
        public static IssueDto ToIssueDto(this Issue issueModel)
        {
            return new IssueDto
            {
                Id = issueModel.Id,
                Type = issueModel.Type,
                Detail = issueModel.Detail, 
            };
        }
    }
}
