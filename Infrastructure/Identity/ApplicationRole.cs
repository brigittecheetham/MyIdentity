﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole : IRole<int>
    { 
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
