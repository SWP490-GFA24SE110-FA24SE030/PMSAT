using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSAT.Entities
{
    public class UserRole
    {
        [Column(TypeName = "varchar(5)")]
        public required string UserRoleID { get; set; } // e.g., "AD", "US", "MD"
        
        [StringLength(20)]
        public required string UserRoleName { get; set; } // Role name

        // Navigation property
        public required User User { get; set; } // One-to-one relationship
    }
}
