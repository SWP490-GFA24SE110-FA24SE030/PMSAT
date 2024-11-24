using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Task
{
    public class AssignTaskToMemberDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }  // Member's email from request body
    }
}
