using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Tasks.Requests
{
    public class AllTasksRequest
    {
        public int StartIndex { get; set; }
        public int Limit { get; set; }
    }
}
