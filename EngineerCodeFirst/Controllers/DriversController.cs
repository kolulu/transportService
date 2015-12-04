using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EngineerCodeFirst.DAL;
using EngineerCodeFirst.Models;
using EngineerCodeFirst.ViewModel;
using System.Data.Entity.Infrastructure;

namespace EngineerCodeFirst.Controllers
{
    public class DriversController : Controller
    {
        private TransportPublicContext db = new TransportPublicContext();

        // GET: Drivers
        public ActionResult Index()
        {
            return View(db.Drivers.ToList());
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            //***************** adding lines ******************//
            var driver = new Driver();

            driver.Lines = new List<Line>();
            PopulateAssignedLineData(driver);
            //************************************************//

            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverID,DriverName,DriverSurname,Status,DriverLogin,DriverPass")] Driver driver, string[] selectedLines)
        {
            //******************* adding lines **********************//
            if (selectedLines != null)
            {
                driver.Lines = new List<Line>();
                foreach (var line in selectedLines)
                {
                    var lineToAdd = db.Lines.Find(int.Parse(line));
                    driver.Lines.Add(lineToAdd);
                }
            }
            //************************************************//



            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //************************************************//
            PopulateAssignedLineData(driver);
            //************************************************//

            return View(driver);
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //Driver driver = db.Drivers.Find(id);

            //************** editing lines ********************//
            Driver driver = db.Drivers
                .Include(i => i.Lines)
                .Where(i => i.DriverID == id)
                .Single();
            PopulateAssignedLineData(driver);
            //************************************************//



            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverID,DriverName,DriverSurname,Status,DriverLogin,DriverPass")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(driver);
        }
        */


        //************** editing with drivers and lines ********************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedLines)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var driverToUpdate = db.Drivers
               .Include(i => i.Lines)
               .Where(i => i.DriverID == id)
               .Single();

            if (TryUpdateModel(driverToUpdate, "",
               new string[] { "DriverID,DriverName,DriverSurname,Status,DriverLogin,DriverPass" }))
            {
                try
                {
                    UpdateDriverLines(selectedLines, driverToUpdate); //****** added for lines *******//
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedLineData(driverToUpdate); //****** added for lines *******//
            return View(driverToUpdate);
        }

        //************************************************//


        // GET: Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        //********************** adding lines ******************//
        private void PopulateAssignedLineData(Driver driver)
        {
            var allLines = db.Lines;
            var busLines = new HashSet<int>(driver.Lines.Select(c => c.LineID));
            var viewModel = new List<AssignedLineData>();
            foreach (var line in allLines)
            {
                viewModel.Add(new AssignedLineData
                {
                    LineID = line.LineID,
                    Direction = line.Direction,
                    LineNumber = line.LineNumber,
                    Assigned = busLines.Contains(line.LineID)
                });
            }
            ViewBag.Lines = viewModel;
        }
        //************************************************//

        //**************** editing lines ***********************//
        private void UpdateDriverLines(string[] selectedLines, Driver driverToUpdate)
        {
            if (selectedLines == null)
            {
                driverToUpdate.Lines = new List<Line>();
                return;
            }

            var selectedLinesHS = new HashSet<string>(selectedLines);
            var busLines = new HashSet<int>
                (driverToUpdate.Lines.Select(c => c.LineID));
            foreach (var line in db.Lines)
            {
                if (selectedLinesHS.Contains(line.LineID.ToString()))
                {
                    if (!busLines.Contains(line.LineID))
                    {
                        driverToUpdate.Lines.Add(line);
                    }
                }
                else
                {
                    if (busLines.Contains(line.LineID))
                    {
                        driverToUpdate.Lines.Remove(line);
                    }
                }
            }
        }
        //************************************************//




    }
}
