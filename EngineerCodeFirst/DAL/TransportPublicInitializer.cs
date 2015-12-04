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



            var schedules = new List<Schedule>
            {
                new Schedule{BusOrder = 0, DepartureTime = "15:35:00", LineID = 2, StopID = 18},
                new Schedule{BusOrder = 1, DepartureTime = "15:39:00", LineID = 2, StopID = 21},
                new Schedule{BusOrder = 2, DepartureTime = "15:45:00", LineID = 2, StopID = 11},
                new Schedule{BusOrder = 3, DepartureTime = "15:47:00", LineID = 2, StopID = 10},
                new Schedule{BusOrder = 4, DepartureTime = "15:59:00", LineID = 2, StopID = 23},
               
                new Schedule{BusOrder = 0, DepartureTime = "13:03:00", LineID = 4, StopID = 9},
                new Schedule{BusOrder = 1, DepartureTime = "13:08:00", LineID = 4, StopID = 10},
                new Schedule{BusOrder = 2, DepartureTime = "13:12:00", LineID = 4, StopID = 8},
                new Schedule{BusOrder = 3, DepartureTime = "13:16:00", LineID = 4, StopID = 1},

                new Schedule{BusOrder = 0, DepartureTime = "21:21:00", LineID = 6, StopID = 21},
                new Schedule{BusOrder = 1, DepartureTime = "21:27:00", LineID = 6, StopID = 9},
                new Schedule{BusOrder = 2, DepartureTime = "21:29:00", LineID = 6, StopID = 10},
                new Schedule{BusOrder = 3, DepartureTime = "21:33:00", LineID = 6, StopID = 27},
                new Schedule{BusOrder = 4, DepartureTime = "21:35:00", LineID = 6, StopID = 28},
                new Schedule{BusOrder = 5, DepartureTime = "21:50:00", LineID = 6, StopID = 15},
                new Schedule{BusOrder = 6, DepartureTime = "21:59:00", LineID = 6, StopID = 16},
            };
            schedules.ForEach(s => context.Schedules.Add(s));
            context.SaveChanges();
        }
    }
}