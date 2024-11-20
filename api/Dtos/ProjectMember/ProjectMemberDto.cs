namespace api.Dtos.ProjectMember
{
    public class ProjectMemberDto
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public required string Role { get; set; }
    }
}
