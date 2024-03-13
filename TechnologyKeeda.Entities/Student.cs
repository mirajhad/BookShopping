using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.Entities
{
    // Student(1) -----------(*)StudentSkill(*)-----------(1)Skill
    public class Student
    {
        public int Id { get; set; }  //1
        public string Name { get; set; }//Amit
        public Address PermanentAddress { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set; }  //{1,1}{1,2}
        = new List<StudentSkill>();

    }
}
