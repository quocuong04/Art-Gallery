using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Art_Gallery.Models;
using static Art_Gallery.Controllers.AdminControllers.AuctionsAdminController;

namespace Art_Gallery.Controllers.AdminControllers
{
    public class EmployeesAdminController : BaseController
    {
        private Art_GalleryEntities db = new Art_GalleryEntities();

        // GET: EmployeesAdmin
        public async Task<ActionResult> Index()
        {
            return View(await db.Employees.ToListAsync());
        }

        
        // GET: EmployeesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeId,Email,EmployeeName,Password,Adress,PhoneNumber,Age,Sex")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: EmployeesAdmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var functionIds = await (from rel in db.Groups
                                     where rel.EmployeeId == id
                                     select rel.FunctionId).ToArrayAsync();

            var model = new EmployeeEditModel
            {
                Employee = await db.Employees.FindAsync(id),
                SelectedIds = functionIds
            };
            ViewBag.Functions = new MultiSelectList(db.Functions, "FunctionId", "FunctionName", functionIds);
            return View(model);
        }

        // POST: EmployeesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeId,Email,EmployeeName,Password,Adress,PhoneNumber,Age,Sex")] Employee employee,int[] selectedIds)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (selectedIds != null && selectedIds.Length > 0)
                {
                    var existingRel = db.Groups.Where(rel => rel.EmployeeId == employee.EmployeeId);
                    db.Groups.RemoveRange(existingRel);

                    foreach (var functionId in selectedIds)
                    {
                        var relArtworkAuction = new Group
                        {
                            EmployeeId = employee.EmployeeId,
                            FunctionId = functionId
                        };
                        db.Groups.Add(relArtworkAuction);
                    }

                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: EmployeesAdmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: EmployeesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
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
        public class EmployeeEditModel
        {
            public Employee Employee { get; set; }
            public int?[] SelectedIds { get; set; }
        }
    }
}
