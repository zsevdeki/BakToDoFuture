using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakToDoFuture.Models;

namespace BakToDoFuture.Controllers
{
    public class DepartmentsController : Controller
    {
        private FutureDbContext db = new FutureDbContext();

        // GET: Departments
        public async Task<ActionResult> Index()
        {
            return View(await db.Departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            var department=new Department();
            return View(department);
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.CreatedBy = User.Identity.Name;
                department.CreateDate=DateTime.Now;
                department.UpdateBy = User.Identity.Name;
                department.UpdateDate=DateTime.Now;
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.UpdateBy = User.Identity.Name;
                department.UpdateDate = DateTime.Now;
                db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
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
        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from s in db.Departments.ToList()
                              select new
                              {
                                  DepartmanIsmi = s.Name,
                                  OlusturulmaTarihi = s.CreateDate,
                                  OlusturanKullanici = s.CreatedBy,
                                  GuncellemeTarihi = s.UpdateDate,
                                  GuncelleyenKullanici = s.UpdateBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Departman.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grid.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Departman Ismi,Olusturma Tarihi,Olusturan Kullanici,Guncelleme Tarihi,Guncelleyen Kullanici");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Departman.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            var departments = db.Departments;
            foreach (var department in departments)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                    department.Name,
                    department.CreateDate,
                    department.CreatedBy,
                    department.UpdateDate,
                    department.UpdateBy
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
