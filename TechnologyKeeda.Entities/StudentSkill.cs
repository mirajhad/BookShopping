using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.Entities
{
    // Student(1) -----------(*)StudentSkill(*)-----------(1)Skill
    public class StudentSkill
    {
        public int StudentId { get; set; }  // Foreign Key
        public Student Student { get; set; }
        public int SkillId { get; set; }  // Foreign Key
        public Skill Skill { get; set; }

    }
}


// Student              StudentSkill                      Skill
// Id     Name          StudentId  SkillId               Id   Title
// 1      Ramesh           1         1                   1     C#
// 2      Amit             1          2                  2      Java
//                          2         3                     3       ASP.NET
//                          2          2
                       //   2         2
                       // Composite key

