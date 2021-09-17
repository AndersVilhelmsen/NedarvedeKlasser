using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedarvedeKlasser
{
    public enum TitleEnum { Student, Teacher}
    abstract class Person
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public TitleEnum Title {  get; set; }

    }
}
