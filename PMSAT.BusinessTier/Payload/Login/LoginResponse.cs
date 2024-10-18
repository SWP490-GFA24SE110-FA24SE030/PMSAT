using PMSAT.BusinessTier.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload.Login
{
    public class LoginResponse
    {
        public TokenModel TokenModel { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public RoleEnum Role { get; set; }
        public UserStatus Status { get; set; }
        //public Guid UserId { get; set; }
        //public string FullName { get; set; }
        //public string? Email { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Address {  get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(UserStatus status)
        {
            Status = status;
        }

        public LoginResponse(Guid userId, string username, RoleEnum role, UserStatus status)
        {
            UserId = userId;
            Username = username;
            Role = role;
            Status = status;
        }
    }
}
