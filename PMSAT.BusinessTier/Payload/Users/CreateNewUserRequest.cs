using PMSAT.BusinessTier.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload.Users
{
    public class CreateNewUserRequest
    {
        public RoleEnum Role { get; set; }
        [Required(ErrorMessage = "Username is missing")]
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Email is wrong format")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
