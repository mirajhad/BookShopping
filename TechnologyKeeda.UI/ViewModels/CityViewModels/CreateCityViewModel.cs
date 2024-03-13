﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.UI.ViewModels.CityViewModels
{
    public class CreateCityViewModel
    {
        public string CityName { get; set; }
        [Display(Name ="State Names")]
        public int StateId { get; set; }
    }
}