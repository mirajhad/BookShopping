using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologyKeeda.UI.Utility;

namespace TechnologyKeeda.UI.ViewModels.SkillViewModels
{
    public class SkillViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

    }
    public class PagedSkillViewModel
    {
        public List<SkillViewModel> Skills { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
