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
    public class CustomersController : Controller
    {
        private FutureDbContext db = new FutureDbContext();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var customer=new Customer();
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Phone,Fax,Website,Address,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CreatedBy = User.Identity.Name;
                customer.CreateDate = DateTime.Now;
                customer.UpdateBy = User.Identity.Name;
                customer.UpdateDate = DateTime.Now;
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Phone,Fax,Website,Address,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.UpdateBy = User.Identity.Name;
                customer.UpdateDate = DateTime.Now;
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
            grid.DataSource = from data in db.Customers.ToList()
                              select new
                              {
                                  Id = data.Id,
                                  Isim = data.Name,
                                  EPosta = data.Email,
                                  Telefon = data.Phone,
                                  Faks = data.Fax,
                                  WebSitesi = data.Website,
                                  Adres = data.Address,
                                  OlusturulmaTarihi = data.CreateDate,
                                  OlusturanKullanici = data.CreatedBy,
                                  GuncellenmeTarihi = data.UpdateDate,
                                  GuncelleyenKullanici = data.UpdateBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Musteriler.xls");
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
            sw.WriteLine("Id,Isim,E-posta,Telefon,Faks,Web Sitesi,Adres,Olusturulma Tarihi,Olusturan Kullanici,Guncellenme Tarihi,Guncelleyen Kullanici");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Musteriler.csv");
            Response.ContentType = "text/csv";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            var Musteriler = db.Customers;
            foreach (var musteri in Musteriler)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                    musteri.Id,
                    musteri.Name,
                    musteri.Email,
                    musteri.Phone,
                    musteri.Fax,
                    musteri.Website,
                    musteri.Address,
                    musteri.CreateDate,
                    musteri.CreatedBy,
                    musteri.UpdateDate,
                    musteri.UpdateBy
                    )
                    );
            }
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
