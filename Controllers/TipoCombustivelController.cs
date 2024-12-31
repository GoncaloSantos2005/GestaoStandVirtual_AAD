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
    public class TipoCombustivelController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: TipoCombustivel
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var combustivelList = db.TipoCombustivel.ToList();
                    return View(combustivelList);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to access this page!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: TipoCombustivel/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. No fuel type specified.";
                        return RedirectToAction("Index");
                    }

                    TipoCombustivel tipoCombustivel = db.TipoCombustivel.Find(id);
                    if (tipoCombustivel == null)
                    {
                        TempData["MessageError"] = "Fuel type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoCombustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view this page!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoCombustivel/Create
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
                    TempData["MessageError"] = "You do not have permissions to add a fuel type!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoCombustivel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoComID,DescricaoTC")] TipoCombustivel tipoCombustivel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.TipoCombustivel.Add(tipoCombustivel);
                        db.SaveChanges();
                        TempData["Message"] = "Fuel type created successfully!";
                        return RedirectToAction("Create");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form.";
                    return View(tipoCombustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to add a fuel type!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoCombustivel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. No fuel type specified.";
                        return RedirectToAction("Index");
                    }

                    TipoCombustivel tipoCombustivel = db.TipoCombustivel.Find(id);
                    if (tipoCombustivel == null)
                    {
                        TempData["MessageError"] = "Fuel type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoCombustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a fuel type!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoCombustivel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoComID,DescricaoTC")] TipoCombustivel tipoCombustivel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(tipoCombustivel).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["Message"] = "Fuel type updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form.";
                    return View(tipoCombustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a fuel type!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoCombustivel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. No fuel type specified.";
                        return RedirectToAction("Index");
                    }

                    TipoCombustivel tipoCombustivel = db.TipoCombustivel.Find(id);
                    if (tipoCombustivel == null)
                    {
                        TempData["MessageError"] = "Fuel type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoCombustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a fuel type!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoCombustivel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    TipoCombustivel tipoCombustivel = db.TipoCombustivel.Find(id);
                    if (tipoCombustivel == null)
                    {
                        TempData["MessageError"] = "Fuel type not found.";
                        return RedirectToAction("Index");
                    }

                    db.TipoCombustivel.Remove(tipoCombustivel);
                    db.SaveChanges();

                    TempData["Message"] = "Fuel type deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a fuel type!";
                    return RedirectToAction("Index", "Home");
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
