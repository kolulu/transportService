using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.Models
{
    //public enum Status
    //{
    //    ON, OFF
    //}
    public class Bus
    {
        
        public Bus()
        {
            this.Lines = new HashSet<Line>();
            this.Drivers = new HashSet<Driver>();
        }

        public int BusID { get; set; }
        public string RegNum { get; set; }
        public string Status { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string GeoLocation
        {
            get
            {
                return Latitude + ", " + Longitude;
            }
        }
        public virtual ICollection<Line> Lines { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}