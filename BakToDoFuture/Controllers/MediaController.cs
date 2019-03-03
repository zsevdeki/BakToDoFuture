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
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using BakToDoFuture.Models;

namespace BakToDoFuture.Controllers
{
    public class MediaController : Controller
    {
        private FutureDbContext db = new FutureDbContext();

        // GET: Media
        public async Task<ActionResult> Index()
        {
            return View(await db.Medias.ToListAsync());
        }

        // GET: Media/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            var media=new Media();
            return View(media);
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Media media)
        {
            if (ModelState.IsValid)
            {
                media.CreateDate = DateTime.Now;
                media.CreatedBy = User.Identity.Name;
                media.UpdateDate = DateTime.Now;
                media.UpdateBy = User.Identity.Name;

                //Upload işlemi
                if (!string.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileInfo=new FileInfo(Server.MapPath("~"+media.FilePath));
                    media.FileSize = ((float)fileInfo.Length) / ((float)1024);
                    media.Extension = fileInfo.Extension;
                    media.ContentType = fileInfo.Extension;
                }

                db.Medias.Add(media);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(media);
        }

        // GET: Media/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Extension,FilePath,FileSize,Year,Month,ContentType,CreateDate,CreatedBy,UpdateDate,UpdateBy")] Media media)
        {
            if (ModelState.IsValid)
            {
                media.UpdateDate = DateTime.Now;
                media.UpdateBy = User.Identity.Name;

                //Upload işlemi
                if (!string.IsNullOrEmpty(media.FilePath))
                {
                    FileInfo fileInfo = new FileInfo(Server.MapPath("~" + media.FilePath));
                    media.FileSize = ((float)fileInfo.Length) / ((float)1024);
                    media.Extension = fileInfo.Extension;
                    media.ContentType = fileInfo.Extension;
                }

                db.Entry(media).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(media);
        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string categoryFolder = "";


            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var uploadLocation = Server.MapPath("~/uploads");
                        categoryFolder = "/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "/";

                        fName = file.FileName;
                        var extension = Path.GetExtension(fName).ToLower();
                        var contentType = file.ContentType;
                        float fileSize = ((float)file.ContentLength) / ((float)1024);
                        if (!Directory.Exists(uploadLocation + categoryFolder))
                        //                 ~/uploads/2018-06/
                        {
                            Directory.CreateDirectory(uploadLocation + categoryFolder);
                        }
                        if (!System.IO.File.Exists(uploadLocation + categoryFolder + fName))
                        //                 ~/uploads/2018-06/onur.xls
                        {
                            file.SaveAs(uploadLocation + categoryFolder + fName);
                        }
                        else
                        {
                            throw new Exception("Bu dosya mevcut hacı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                isSavedSuccessfully = false;
                
            }
            if (isSavedSuccessfully)
            {
                return Json(new {Message = "/uploads" + categoryFolder + fName, success = true});
            }
            else
            {
                return Json(new {Message = "Hata oldu,dosya kaydedilemedi.", success = false});
            }
        }

        // GET: Media/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Media media = await db.Medias.FindAsync(id);
            if (media == null)
            {
                return HttpNotFound();
            }
            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Media media = await db.Medias.FindAsync(id);
            db.Medias.Remove(media);
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
        public void ExportToCsv()
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("Medya Adi-Aciklama-Uzanti-Dosya Yolu-Dosya Boyutu-Yıl-Ay-Olusturulma Tarihi-Olusturan Kullanici-Guncellenme Tarihi-Guncelleyen Kullanici");
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Medya.csv");
            Response.ContentType = "text/csv";
            var medya = db.Medias;
            foreach (var medias in medya)
            {
                sw.WriteLine(string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}",

                    medias.Name,
                    medias.Description,
                    medias.Extension,
                    medias.FilePath,
                    medias.FileSize,
                    medias.Year,
                    medias.Month,
                    medias.CreateDate,
                    medias.CreatedBy,
                    medias.UpdateDate,
                    medias.UpdateBy
                    )
                    );
            }
            Response.Write(sw.ToString());
            Response.End();
        }
        public void ExportToExcel()
        {
            var grid = new GridView();
            grid.DataSource = from data in db.Medias.ToList()
                              select new
                              {
                                  Id = data.Id,
                                  Adı = data.Name,
                                  Açıklama = data.Description,
                                  Uzantı = data.Extension,
                                  DosyaYolu = data.FilePath,
                                  DosyaBoyutu = data.FileSize,
                                  Yıl = data.Year,
                                  Ay = data.Month,
                                  OlusturulmaTarihi = data.CreateDate,
                                  OlusturanKullanici = data.CreatedBy,
                                  GuncellenmeTarihi = data.UpdateDate,
                                  GuncelleyenKullanici = data.UpdateBy
                              };
            grid.DataBind();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=Medias.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new HtmlTextWriter(sw);

            grid.RenderControl(hw);

            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
