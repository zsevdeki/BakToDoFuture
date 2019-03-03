using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakToDoFuture.Models;

namespace BakToDoFuture.Controllers
{
    public class SidesController : Controller
    {
        private FutureDbContext db = new FutureDbContext();

        // GET: Sides
        public async Task<ActionResult> Index()
        {
            return View(await db.Sides.ToListAsync());
        }

        // GET: Sides/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Side side = await db.Sides.FindAsync(id);
            if (side == null)
            {
                return HttpNotFound();
            }
            return View(side);
        }

        // GET: Sides/Create
        public ActionResult Create()
        {
            var side=new Side();
            return View(side);
        }

        // POST: Sides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Side side)
        {
            if (ModelState.IsValid)
            {
                side.CreateDate = DateTime.Now;
                side.CreatedBy = User.Identity.Name;
                side.UpdateDate = DateTime.Now;
                side.UpdateBy = User.Identity.Name;
                db.Sides.Add(side);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(side);
        }

        // GET: Sides/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Side side = await db.Sides.FindAsync(id);
            if (side == null)
            {
                return HttpNotFound();
            }
            return View(side);
        }

        // POST: Sides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Side side)
        {
            if (ModelState.IsValid)
            {
                side.UpdateDate = DateTime.Now;
                side.UpdateBy = User.Identity.Name;
                db.Entry(side).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(side);
        }

        // GET: Sides/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Side side = await db.Sides.FindAsync(id);
            if (side == null)
            {
                return HttpNotFound();
            }
            return View(side);
        }

        // POST: Sides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Side side = await db.Sides.FindAsync(id);
            db.Sides.Remove(side);
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
            grid.DataSource = from data in db.Sides.ToList()
                              select new
                              {

                                  TarafIsmi = data.Name,
                                  OlusturulmaTarihi = data.CreateDate,
                                  OlusturanKullanici = data.CreatedBy,
                                  GuncellenmeTarihi = data.UpdateDate,
                                  GuncelleyenKullanici = data.UpdateBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Taraf.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);
            grid.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }


        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Taraf Ismi,Olusturulma Tarihi,Olusturan Kullanici,Guncellenme Tarihi,Guncelleyen Kullanici");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Taraf.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            var side = db.Sides;
            foreach (var sides in side)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4}",
                    sides.Name,
                    sides.CreateDate,
                    sides.CreatedBy,
                    sides.UpdateDate,
                    sides.UpdateBy
                    ));
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
