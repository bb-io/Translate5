using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Tasks.Requests
{
    public class ChangeTaskNameRequest
    {
        public string TaskId { get; set; }

        public string NewName { get; set; }
    }
}
