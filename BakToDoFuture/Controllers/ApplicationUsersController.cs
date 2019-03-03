using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BakToDoFuture.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BakToDoFuture.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private FutureDbContext db = new FutureDbContext();

        // GET: ApplicationUsers
        public async Task<ActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await db.Users.FirstOrDefaultAsync(u=>u.Id==id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        //// GET: ApplicationUsers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ApplicationUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ApplicationUsers.Add(applicationUser);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        // GET: ApplicationUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await db.Users.FirstOrDefaultAsync(u=>u.Id==id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await db.Users.FirstOrDefaultAsync(u=>u.Id==id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            db.Users.Remove(applicationUser);
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
            grid.DataSource = from data in db.Users.ToList()
                              select new
                              {
                                  Id = data.Id,
                                  EPosta = data.Email,
                                  EPostaOnay = data.EmailConfirmed,
                                  Telefon = data.PhoneNumber,
                                  TelefonOnay = data.PhoneNumberConfirmed,
                                  IkiAsamaliKimlikDogrulamaEtkin = data.TwoFactorEnabled,
                                  KilitlemeBitisTarihi = data.LockoutEndDateUtc,
                                  KilitlemeEtkin = data.LockoutEnabled,
                                  BasarisizErisimSayisi = data.AccessFailedCount,
                                  KullaniciAdi = data.UserName
                              };
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ApplicationUsers.xls");
            Response.ContentType = "application/excel";
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
            sw.WriteLine("E-Posta,E-Posta Onay,Telefon,Telefon Onay,Iki Asamali Kimlik Dogrulama Etkin,Kilitleme Bitis Tarihi,Kilitleme Etkin,Basarisiz Erisim Sayisi,Kullanici Adi");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=ApplicationUsers.csv");
            Response.ContentType = "text/csv";
            var users = db.Users;
            foreach (var data in users)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                  data.Id,
                                  data.Email,
                                  data.EmailConfirmed,
                                  data.PhoneNumber,
                                  data.PhoneNumberConfirmed,
                                  data.TwoFactorEnabled,
                                  data.LockoutEndDateUtc,
                                  data.LockoutEnabled,
                                  data.AccessFailedCount,
                                  data.UserName

                    )
                    );
            }
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());


            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
