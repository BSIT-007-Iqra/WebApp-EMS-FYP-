using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Utils
{
    public class EventDates
    {
        public DateTime Event_Date { get; set; } = DateTime.Now;
        public TimeSpan Start_Time { get; set; } = TimeSpan.Zero;
        public TimeSpan End_Time { get; set; } = TimeSpan.Zero;
        public bool Confirm { get; set; } = false;
        public string Event_Name { get; set; } = " ";
    }
    public class SearchFilterData
    {
        public string Location { get; set; } = " ";
        public string Slot { get; set; } = " ";
        public decimal Price { get; set; } = 1000;
    }
}