using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    internal class Student:BaseClass
    {
        public string Surname { get; set; }
        public Group group { get; set; }
        public int GroupId { get; set; }
    }
}
