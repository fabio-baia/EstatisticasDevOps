using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Build
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public DateTimeOffset DataBonita { get; set; }
        public List<Property> Property { get; set; }
    }
}