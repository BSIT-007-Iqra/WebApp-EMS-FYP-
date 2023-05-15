using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Utils
{
    public static class BaseHelper
    {
        public static Customer customer { get; set; }
        public static Event_Organizers event_organizers { get; set; }
        public static Admin Admin { get; set; }

       public static Website_Details website{ get; set; }

       
        //public static decimal totalsalary { get; set; }
        //public static IEnumerable<Episode>  Episodelist { get; set; } 
    }
}