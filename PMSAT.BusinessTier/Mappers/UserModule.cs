using AutoMapper;
using PMSAT.BusinessTier.Payload.Users;
using PMSAT.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Mappers
{
    public class UserModule : Profile
    {
        public UserModule()
        {
            CreateMap<User, GetUserReponse>();

        }
    }
}
