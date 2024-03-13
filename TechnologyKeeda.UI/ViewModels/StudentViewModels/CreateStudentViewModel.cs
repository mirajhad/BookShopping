﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.Entities;

namespace TechnologyKeeda.UI.ViewModels.StudentViewModels
{
    public class CreateStudentViewModel
    {
        public string StudentName { get; set; }
        public Address PhysicalAddress { get; set; }
        public List<CheckBoxTable> SkillList { get; set; } = new List<CheckBoxTable>();

    }
    public class CheckBoxTable
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; } = "Default";
        public bool IsChecked { get; set; }
    }
}
