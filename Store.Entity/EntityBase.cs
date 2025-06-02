using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace StoreApi.Entity
{
    public abstract class EntityBase
    {

        public int id { get; set; }
    }
}
