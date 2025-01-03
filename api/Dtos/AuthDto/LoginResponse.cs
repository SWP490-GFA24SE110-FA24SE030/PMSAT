using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.LoginDto
{
    public class LoginResponse
    {
        public Guid Id { get; set; }

        public string? Token { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}