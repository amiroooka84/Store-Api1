using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Entity._Like
{
    public class Like : EntityBase
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
