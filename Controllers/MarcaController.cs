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
    public class MarcaController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Marca
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var marcas = db.Marca.ToList();
                    return View(marcas);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to view this list!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Marca/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. The ID is required.";
                        return RedirectToAction("Index");
                    }

                    Marca marca = db.Marca.Find(id);
                    if (marca == null)
                    {
                        TempData["MessageError"] = "The requested brand does not exist.";
                        return RedirectToAction("Index");
                    }

                    return View(marca);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to view details!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Marca/Create
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
                    TempData["MessageError"] = "You do not have the necessary permissions to create a new brand!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Marca/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarcaID,NomeMarca")] Marca marca)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Marca.Add(marca);
                        db.SaveChanges();

                        TempData["Message"] = "Brand created successfully!";
                        return RedirectToAction("Create");
                    }

                    TempData["MessageError"] = "Failed to create the brand. Please check the form and try again.";
                    return View(marca);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to create a new brand!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Marca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. No ID provided.";
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Marca marca = db.Marca.Find(id);

                    if (marca == null)
                    {
                        TempData["MessageError"] = "Brand not found.";
                        return HttpNotFound();
                    }

                    return View(marca);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to edit a brand!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Marca/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarcaID,NomeMarca")] Marca marca)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(marca).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["Message"] = "Brand updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Failed to update the brand. Please check the form and try again.";
                    return View(marca);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to edit a brand!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Marca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. No ID provided.";
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Marca marca = db.Marca.Find(id);

                    if (marca == null)
                    {
                        TempData["MessageError"] = "Brand not found.";
                        return HttpNotFound();
                    }

                    return View(marca);
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to delete a brand!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    Marca marca = db.Marca.Find(id);

                    if (marca == null)
                    {
                        TempData["MessageError"] = "Brand not found.";
                        return RedirectToAction("Index");
                    }

                    db.Marca.Remove(marca);
                    db.SaveChanges();

                    TempData["Message"] = "Brand deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have the necessary permissions to delete a brand!";
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
