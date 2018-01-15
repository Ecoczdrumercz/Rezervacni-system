using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class OrderViewModel
    {
        public string eventsJson { get; set; }
        public List<int> ids { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string text { get; set; }
    }
}