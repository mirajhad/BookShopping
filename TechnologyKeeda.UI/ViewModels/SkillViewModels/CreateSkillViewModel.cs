﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.UI.Validations;

namespace TechnologyKeeda.UI.ViewModels.SkillViewModels
{
    public class CreateSkillViewModel
    {
        [Uppercase]
        public string Title { get; set; }

    }
}
