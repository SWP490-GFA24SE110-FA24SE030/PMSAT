using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PMSAT.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public required string UserID { get; set; }  // Format: AD00, US000000, MD00

        [ForeignKey("UserRoles")]
        public required string UserRoleID { get; set; }

        [StringLength(30)]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public required string UserEmail { get; set; }

        [Required]
        [StringLength(255)]
        public required string UserPassword { get; set; }

        [Required]
        public bool UserStatus { get; set; }

        // Navigation property for one-to-one relationship
        public virtual required UserRole UserRole { get; set; }
    }
}
