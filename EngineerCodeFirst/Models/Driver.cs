using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    public class Driver
    {
        
        public Driver()
        {
            this.Buses = new HashSet<Bus>();
            this.Lines = new HashSet<Line>();
        }

        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
       // public Status? Status { get; set; }
        public string Status { get; set; }
        public string DriverLogin { get; set; }
        public string DriverPass { get; set; }

        [Display(Name = "Driver")]
        public string DriverInfo
        {
            get
            {
                return DriverName + " " + DriverSurname;
            }
        }
        public virtual ICollection<Bus> Buses { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
    }
}