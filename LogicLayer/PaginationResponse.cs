using LogicLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public  class PaginationResponse
    {
        public int PageIndex = 1;
        public int PageSize = 10;
        public int TotalCount;
        public IEnumerable<TaskDto> Data=new List<TaskDto>();
    }
}
