using Apps.Translate5.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Models.Comments.Response
{
    public class ListCommentsResponse
    {
        public IEnumerable<CommentDto> Comments { get; set; }
    }
}
