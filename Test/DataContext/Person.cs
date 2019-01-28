using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DataContext
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool InActive { get; set; }
        public bool IsMarried { get; set; }
        public int RemainingVacation { get; set; }
        public double Salary { get; set; }
    }
}
