using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedarvedeKlasser
{
    sealed class Teacher : Person
    {
        public bool CoffeeClub {  get; set; }
        public int Wages {  get; set; }
        public List<CourseModel> Courses {  get; set; }

    }
}
