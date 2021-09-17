using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedarvedeKlasser
{
    public enum CourseEnum { Databases, ObjectProgram, Network, ComputerTechnology }

    class CourseModel
    {
        public int Id {  get; set; }
        public CourseEnum Type{  get; set; }
        public string Name {  get; set; }
        public int TeacherId {  get; set; }
        
    }
}
