using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Payload
{
    public class PagingModel
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
        public string? sort { get; set; }
        public string? order { get; set; }
    }

    public class SortModel
    {
        public string? sort { get; set; } = null;
        public string? order { get; set; } = null;
    }
}
