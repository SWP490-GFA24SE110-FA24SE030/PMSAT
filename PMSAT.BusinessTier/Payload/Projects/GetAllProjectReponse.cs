using PMSAT.BusinessTier.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload.Projects
{
    public class GetAllProjectReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }

        public GetAllProjectReponse(Guid id, string name, ProjectStatus status)
        {
            Id = id;
            Name = name;
            Status = status;
        }
    }
}
