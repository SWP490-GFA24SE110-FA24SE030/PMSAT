using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
}
}