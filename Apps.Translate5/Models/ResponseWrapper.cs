using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models
{
    public class ResponseWrapper<T>
    {
        public T Rows { get; set; }
    }
}
