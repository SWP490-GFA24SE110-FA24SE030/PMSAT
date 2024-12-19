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
                Role = projectmemberModel.Role,
            };
        }
    }
}
