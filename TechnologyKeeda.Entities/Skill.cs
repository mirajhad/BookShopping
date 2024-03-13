﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.Entities
{
    //Student(*) ---------------(*)Skill
    public class Skill
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<StudentSkill> StudentSkills { get; set; }
= new List<StudentSkill>();
    }
}
