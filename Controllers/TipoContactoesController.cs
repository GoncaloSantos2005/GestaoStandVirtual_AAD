using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StandVirtual.Models;

namespace StandVirtual.Controllers
{
    public class TipoContactoesController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: TipoContactoes
        public ActionResult Index()
        {
            return View(db.TipoContacto.ToList());
        }

        // GET: TipoContactoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoContacto tipoContacto = db.TipoContacto.Find(id);
            if (tipoContacto == null)
            {
                return HttpNotFound();
            }
            return View(tipoContacto);
        }

        // GET: TipoContactoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoContactoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoContacto1,DescricaoTipoContacto")] TipoContacto tipoContacto)
        {
            if (ModelState.IsValid)
            {
                db.TipoContacto.Add(tipoContacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoContacto);
        }

        // GET: TipoContactoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoContacto tipoContacto = db.TipoContacto.Find(id);
            if (tipoContacto == null)
            {
                return HttpNotFound();
            }
            return View(tipoContacto);
        }

        // POST: TipoContactoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoContacto1,DescricaoTipoContacto")] TipoContacto tipoContacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoContacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoContacto);
        }

        // GET: TipoContactoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoContacto tipoContacto = db.TipoContacto.Find(id);
            if (tipoContacto == null)
            {
                return HttpNotFound();
            }
            return View(tipoContacto);
        }

        // POST: TipoContactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoContacto tipoContacto = db.TipoContacto.Find(id);
            db.TipoContacto.Remove(tipoContacto);
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
    }
}
