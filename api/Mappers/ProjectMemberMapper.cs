using api.Dtos.ProjectMember;
using api.Models;

namespace api.Mappers
{
    public static class ProjectMemberMapper
    {
        public static ProjectMemberDto ToProjectMemberDto(this ProjectMember projectmemberModel)
        {
            return new ProjectMemberDto
            {
                Id = projectmemberModel.Id,
                Role = projectmemberModel.Role,
            };
        }
    }
}
