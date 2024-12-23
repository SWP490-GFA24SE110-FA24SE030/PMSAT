using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User userModel) 
        {
            return new UserDto 
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                TaskPReporters = userModel.TaskPReporters,
                TaskPAssignees = userModel.TaskPAssignees,
            };
        }

        public static User ToUserFromCreateDTO(this CreateUserRequestDto userDto)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password,
            Role = userDto.Role,
            Status = userDto.Status

        };
    }
    }

    
}