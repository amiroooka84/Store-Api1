using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.DAL
{
    public static class ConStr
    {
        public static string con { get; set; } = "Data Source=.;Initial Catalog=StoreApi1;Integrated Security=True";
        //public static string con { get; set; } = "Server=localhost;Database=OrderDb; User ID=sa; Password=Sa@123456; MultipleActiveResultSets=true";
    }

}
