using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handson_3.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public bool Permanent { get; set; }
        public string Department { get; set; }
        public string Skills { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

