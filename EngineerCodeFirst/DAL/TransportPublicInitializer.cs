using EngineerCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EngineerCodeFirst.DAL
{
    public class TransportPublicInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TransportPublicContext>
    {
        protected override void Seed(TransportPublicContext context)
        {
            var buses = new List<Bus>
            {
                new Bus{RegNum = "SGL A123",Status="OFF"},
                new Bus{RegNum = "SGL B456",Status="OFF"},
                new Bus{RegNum = "SGL C789",Status="ON"},
                new Bus{RegNum = "SGL Y666",Status="ON"},
            };
            buses.ForEach(s => context.Buses.Add(s));
            context.SaveChanges();

            var drivers = new List<Driver>
            {
                new Driver{DriverName="Clint", DriverSurname="Eastwood", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 3)}},
                new Driver{DriverName="Jay", DriverSurname="Z", Status = "ON", Buses = new List<Bus>{buses.Single(b => b.BusID == 4)}},
                new Driver{DriverName="Sherlock", DriverSurname="Holmes", Status = "OFF"},
            };

            

            drivers.ForEach(s => context.Drivers.Add(s));
            context.SaveChanges();

            var lines = new List<Line>
            {
                new Line{LineNumber=47, Direction="Szczyglowice"},
                new Line{LineNumber=47, Direction="Zabrze"},
                new Line{LineNumber=58, Direction="Knurow", Buses = new List<Bus>{buses.Single(b => b.BusID == 3)}},
                new Line{LineNumber=58, Direction="Gliwice"},
                new Line{LineNumber=120, Direction="Knurow"},
                new Line{LineNumber=120, Direction="Katowice", Buses = new List<Bus>{buses.Single(b => b.BusID == 4)}},
            };
            lines.ForEach(s => context.Lines.Add(s));
            context.SaveChanges();

            var stops = new List<Stop>
            {
                new Stop{City="Gliwice", StopName="Komag"},
                new Stop{City="Gliwice", StopName="Plac Piastow"},
                new Stop{City="Gliwice", StopName="Wroclawska"},
                new Stop{City="Gliwice", StopName="Zwyciestwa"},
                new Stop{City="Gliwice", StopName="Warszawska"},
                new Stop{City="Gliwice", StopName="Dworcowa"},
                new Stop{City="Gliwice", StopName="Robotnicza"},
                new Stop{City="Gliwice", StopName="Lotnikow"},
                new Stop{City="Gieraltowice", StopName="Skrzyzowanie"},
                new Stop{City="Gieraltowice", StopName="Kosciol"},
                new Stop{City="Gieraltowice", StopName="Granica"},
                new Stop{City="Gieraltowice", StopName="Wyzwolenia"},
                new Stop{City="Katowice", StopName="Andrzeja"},
                new Stop{City="Katowice", StopName="Francuska"},
                new Stop{City="Katowice", StopName="Mikolowska"},
                new Stop{City="Katowice", StopName="Rondo"},
                new Stop{City="Katowice", StopName="Chorzowska"},
                new Stop{City="Knurow", StopName="Szpitalna"},
                new Stop{City="Knurow", StopName="Straz"},
                new Stop{City="Knurow", StopName="Wilsona"},
                new Stop{City="Knurow", StopName="Dworcowa"},
                new Stop{City="Knurow", StopName="Kopalnia"},
                new Stop{City="Zabrze", StopName="Wolnosci"},
                new Stop{City="Zabrze", StopName="3go Maja"},
                new Stop{City="Zabrze", StopName="Sienkiewicza"},
                new Stop{City="Zabrze", StopName="Plac Teatralny"},
                new Stop{City="Paniowki", StopName="Swietlica"},
                new Stop{City="Borowa Wies", StopName="Swietlica"},
            };
            stops.ForEach(s => context.Stops.Add(s));
            context.SaveChanges();
            

        }
    }
}