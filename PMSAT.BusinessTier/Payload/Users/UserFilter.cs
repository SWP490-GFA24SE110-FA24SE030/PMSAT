using PMSAT.BusinessTier.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload.Users
{
    public class UserFilter
    {
        public string? UserName { get; set; }
        public RoleEnum? Role { get; set; }
        public UserStatus? Status { get; set; }
    }
}
