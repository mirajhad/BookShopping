﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.Services
{
    public interface IDbInitializer
    {
        Task Initialize();

    }
}
