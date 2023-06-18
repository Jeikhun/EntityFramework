using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    internal class Group:BaseClass
    {
        public ICollection<Student> Students { get; set; }



    }
}
