using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.WebAPI.Utility
{
    public class SchoolSettings
    {
        public string SchoolName { get; set; }
        public Address Address { get; set; }
    }
    public class Address
    {
        public string City { get; set; }
        public int ISDCode { get; set; }
    }
}
