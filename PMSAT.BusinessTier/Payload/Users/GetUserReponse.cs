using PMSAT.BusinessTier.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload.Users
{
    public class GetUserReponse
    {
        public Guid Id { get; set; }
        public RoleEnum Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }

        public GetUserReponse() { }

        public GetUserReponse(Guid id, RoleEnum role, string userName, string email, UserStatus status)
        {
            Id = id;
            Role = role;
            UserName = userName;
            Email = email;
            Status = status;
        }
    }
}
