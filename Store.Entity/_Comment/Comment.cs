using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Comment
{
    public class Comment : EntityBase
    {
        public string Content { get; set; } 
        public string Date { get; set; } 
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
