﻿using System.Data;
using System.Net;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;
using ContosoUniversity.Caching;
using EFCache;

namespace ContosoUniversity.Controllers
{

    public class StudentController : Controller
    {
        
        public SchoolContext db = new SchoolContext();
       
        // GET: /Student/
        public ActionResult Index()
        {
            //System.Diagnostics.Debug.WriteLine(Caching.Configuration.Cache.Count);
            string query = db.Students.ToString();
            object x; 
            if (Caching.Configuration.Cache.GetItem("TEST_SAMO_" + query + "_", out x) == false)
            {
                sqldep dep = new sqldep();
                dep.Initialization(query);
            }
            return View(db.Students);
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

         //POST: /Student/Create
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName, FirstMidName, EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(student);
        }

        //     // GET: /Student/Edit/5
        //     public ActionResult Edit(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //         }
        //         Student student = db.Students.Find(id);
        //         if (student == null)
        //         {
        //             return HttpNotFound();
        //         }
        //         return View(student);
        //     }

        //     // POST: /Student/Edit/5
        //     // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //     // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //     [HttpPost]
        //     [ValidateAntiForgeryToken]
        //     public ActionResult Edit([Bind(Include="ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        //     {
        //         try
        //{
        //         if (ModelState.IsValid)
        //         {
        //             db.Entry(student).State = EntityState.Modified;
        //             db.SaveChanges();
        //             return RedirectToAction("Index");
        //         }
        //}
        //         catch (DataException /* dex */)
        //         {
        //             //Log the error (uncomment dex variable name and add a line here to write a log.
        //             ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //         }
        //         return View(student);
        //     }

        //     // GET: /Student/Delete/5
        //     public ActionResult Delete(int? id, bool? saveChangesError = false)
        //     {
        //         if (id == null)
        //         {
        //             return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //         }
        //         if (saveChangesError.GetValueOrDefault())
        //         {
        //             ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
        //         }
        //         Student student = db.Students.Find(id);
        //         if (student == null)
        //         {
        //             return HttpNotFound();
        //         }
        //         return View(student);
        //     }

        //     // POST: /Student/Delete/5
        //     [HttpPost, ActionName("Delete")]
        //     [ValidateAntiForgeryToken]
        //     public ActionResult DeleteConfirmed(int id)
        //     {
        //         try
        //         {
        //             Student student = db.Students.Find(id);
        //             db.Students.Remove(student);
        //             db.SaveChanges();
        //         }
        //         catch (DataException/* dex */)
        //         {
        //             //Log the error (uncomment dex variable name and add a line here to write a log.
        //             return RedirectToAction("Delete", new { id = id, saveChangesError = true });
        //         }
        //         return RedirectToAction("Index");
        //     }

        //     protected override void Dispose(bool disposing)
        //     {
        //         if (disposing)
        //         {
        //             db.Dispose();
        //         }
        //         base.Dispose(disposing);
        //     }
    }
}

   
