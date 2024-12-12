using System.ComponentModel.DataAnnotations;

namespace api.Dtos.ProjectMember
{
    public class AddProjectMemberRequest
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
        public required string Role { get; set; }
    }
}
