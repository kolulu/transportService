﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Stop
    {

        public Stop()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int StopID { get; set; }
        public string City { get; set; }
        public string StopName { get; set; }

	    [Display(Name = "City: Stop")]
        public string StopInfo
        {
            get
            {
                return City + ": " + StopName;
            }
        }
    
        public virtual ICollection<Schedule> Schedules { get; set; }

    }
}