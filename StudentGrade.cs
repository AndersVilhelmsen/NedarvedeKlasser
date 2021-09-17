using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedarvedeKlasser
{
    class StudentGrade
    {
        public int FK_CourseId { get; set; }
        public int FK_StudentId { get; set;}
        public int? Grade { get; set; }
    }
}
