using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.AuthDto
{
    public class RegisterResponse
    {
        public Guid Id { get; set; }

        public string? Token { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}