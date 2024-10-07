using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PMSAT.Entities
{
    public class User
    {
        [Column(TypeName = "varchar(10)")]
        public required string UserID { get; set; }  // Format: AD00, US000000, MD00

        [StringLength(30)]
        public string? UserName { get; set; }

        [StringLength(50)]
        public required string UserEmail { get; set; }

        [StringLength(255)]
        public required string UserPassword { get; set; }

        public bool UserStatus { get; set; }

        public required string UserRoleID { get; set; } //Fkey

        // Navigation property for one-to-one relationship
        public required UserRole UserRole { get; set; }

    }
}
