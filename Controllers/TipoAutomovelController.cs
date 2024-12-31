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
    public class TipoAutomovelController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: TipoAutomovel
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var tipoAutomoveis = db.TipoAutomovel.ToList();
                    return View(tipoAutomoveis);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view the car type list!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoAutomovel/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    TipoAutomovel tipoAutomovel = db.TipoAutomovel.Find(id);
                    if (tipoAutomovel == null)
                    {
                        return HttpNotFound();
                    }

                    return View(tipoAutomovel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view car type details!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoAutomovel/Create
        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to create a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoAutomovel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoAutomovelID,Descricao")] TipoAutomovel tipoAutomovel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        tipoAutomovel.TipoAutomovelID = 0;
                        db.TipoAutomovel.Add(tipoAutomovel);
                        db.SaveChanges();

                        TempData["Message"] = "Car type created successfully!";
                        return RedirectToAction("Create");
                    }

                    TempData["MessageError"] = "Invalid data. Please check the form and try again.";
                    return View(tipoAutomovel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to create a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoAutomovel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    TipoAutomovel tipoAutomovel = db.TipoAutomovel.Find(id);
                    if (tipoAutomovel == null)
                    {
                        return HttpNotFound();
                    }

                    return View(tipoAutomovel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoAutomovel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoAutomovelID,Descricao")] TipoAutomovel tipoAutomovel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(tipoAutomovel).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["Message"] = "Car type updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Invalid data. Please check the form and try again.";
                    return View(tipoAutomovel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: TipoAutomovel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    TipoAutomovel tipoAutomovel = db.TipoAutomovel.Find(id);
                    if (tipoAutomovel == null)
                    {
                        return HttpNotFound();
                    }

                    return View(tipoAutomovel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoAutomovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    TipoAutomovel tipoAutomovel = db.TipoAutomovel.Find(id);
                    if (tipoAutomovel != null)
                    {
                        db.TipoAutomovel.Remove(tipoAutomovel);
                        db.SaveChanges();

                        TempData["Message"] = "Car type deleted successfully!";
                    }
                    else
                    {
                        TempData["MessageError"] = "The car type does not exist!";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a car type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
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
