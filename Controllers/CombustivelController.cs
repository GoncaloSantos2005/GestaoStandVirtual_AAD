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
    public class CombustivelController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Combustivel
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var combustivel = db.Combustivel.Include(c => c.TipoCombustivel).ToList();
                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view the fuel list!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Combustivel/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request! Fuel ID is missing.";
                        return RedirectToAction("Index");
                    }

                    Combustivel combustivel = db.Combustivel.Include(c => c.TipoCombustivel)
                                                            .FirstOrDefault(c => c.CombustivelID == id);
                    if (combustivel == null)
                    {
                        TempData["MessageError"] = "The specified fuel record was not found.";
                        return RedirectToAction("Index");
                    }

                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view fuel details!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Combustivel/Create
        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    ViewBag.TipoComID = new SelectList(db.TipoCombustivel, "TipoComID", "DescricaoTC");
                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to add a new fuel!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Combustivel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CombustivelID,TipoComID")] Combustivel combustivel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Combustivel.Add(combustivel);
                        db.SaveChanges();
                        TempData["Message"] = "Fuel added successfully!";
                        return RedirectToAction("Create");
                    }

                    TempData["MessageError"] = "Failed to create the fuel record. Please review the form.";
                    ViewBag.TipoComID = new SelectList(db.TipoCombustivel, "TipoComID", "DescricaoTC", combustivel.TipoComID);
                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to add a new fuel!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Combustivel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid fuel ID.";
                        return RedirectToAction("Index");
                    }

                    Combustivel combustivel = db.Combustivel.Find(id);
                    if (combustivel == null)
                    {
                        TempData["MessageError"] = "Fuel record not found.";
                        return RedirectToAction("Index");
                    }

                    ViewBag.TipoComID = new SelectList(db.TipoCombustivel, "TipoComID", "DescricaoTC", combustivel.TipoComID);
                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit this record!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Combustivel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CombustivelID,TipoComID")] Combustivel combustivel)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(combustivel).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Message"] = "Fuel record updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Failed to update the record. Please review the form.";
                    ViewBag.TipoComID = new SelectList(db.TipoCombustivel, "TipoComID", "DescricaoTC", combustivel.TipoComID);
                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit this record!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Combustivel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid fuel ID.";
                        return RedirectToAction("Index");
                    }

                    Combustivel combustivel = db.Combustivel.Find(id);
                    if (combustivel == null)
                    {
                        TempData["MessageError"] = "Fuel record not found.";
                        return RedirectToAction("Index");
                    }

                    return View(combustivel);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete this record!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Combustivel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    Combustivel combustivel = db.Combustivel.Find(id);
                    if (combustivel != null)
                    {
                        db.Combustivel.Remove(combustivel);
                        db.SaveChanges();
                        TempData["Message"] = "Fuel record deleted successfully!";
                    }
                    else
                    {
                        TempData["MessageError"] = "Fuel record not found.";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete this record!";
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
