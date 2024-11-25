using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Sprint;
using api.Models;

namespace api.Interfaces
{
    public interface ISprintRepository
    {
        public Task<Sprint> CreateAsync(Sprint sprintModel);
    }
}