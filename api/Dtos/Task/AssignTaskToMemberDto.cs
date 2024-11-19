namespace api.Dtos.Task
{
    public class AssignTaskToMemberDto
    {
        public Guid TaskId { get; set; }
        public Guid MemberID { get; set; }
        public Guid LeaderID { get; set; }
    }
}
