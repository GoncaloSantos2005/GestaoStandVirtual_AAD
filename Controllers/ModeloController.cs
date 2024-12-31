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
    public class ModeloController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Modelo
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var modelo = db.Modelo.Include(m => m.Marca);
                    return View(modelo.ToList());
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to access the Models list!";
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Modelo/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid Model ID!";
                        return RedirectToAction("Index");
                    }

                    Modelo modelo = db.Modelo.Include(m => m.Marca).FirstOrDefault(m => m.ModeloID == id);

                    if (modelo == null)
                    {
                        TempData["MessageError"] = "Model not found!";
                        return RedirectToAction("Index");
                    }

                    return View(modelo);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view the details of a model!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Modelo/Create
        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    ViewBag.MarcaID = new SelectList(db.Marca, "MarcaID", "NomeMarca");
                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to create a model!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Modelo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ModeloID,Descricao,MarcaID")] Modelo modelo)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Modelo.Add(modelo);
                        db.SaveChanges();

                        TempData["Message"] = "Model created successfully!";
                        return RedirectToAction("Create");
                    }

                    ViewBag.MarcaID = new SelectList(db.Marca, "MarcaID", "NomeMarca", modelo.MarcaID);
                    TempData["MessageError"] = "There was an error creating the model. Please review the form and try again.";
                    return View(modelo);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to create a model!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Modelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. Model ID is required!";
                        return RedirectToAction("Index");
                    }

                    Modelo modelo = db.Modelo.Find(id);
                    if (modelo == null)
                    {
                        TempData["MessageError"] = "The requested model does not exist!";
                        return RedirectToAction("Index");
                    }

                    ViewBag.MarcaID = new SelectList(db.Marca, "MarcaID", "NomeMarca", modelo.MarcaID);
                    return View(modelo);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a model!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Modelo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ModeloID,Descricao,MarcaID")] Modelo modelo)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(modelo).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["Message"] = "Model updated successfully!";
                        return RedirectToAction("Index");
                    }

                    ViewBag.MarcaID = new SelectList(db.Marca, "MarcaID", "NomeMarca", modelo.MarcaID);
                    TempData["MessageError"] = "There was an error updating the model. Please review the form and try again.";
                    return View(modelo);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a model!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Modelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. Model ID is required!";
                        return RedirectToAction("Index");
                    }

                    Modelo modelo = db.Modelo.Find(id);
                    if (modelo == null)
                    {
                        TempData["MessageError"] = "The requested model does not exist!";
                        return RedirectToAction("Index");
                    }

                    return View(modelo);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a model!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Modelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    Modelo modelo = db.Modelo.Find(id);
                    if (modelo != null)
                    {
                        db.Modelo.Remove(modelo);
                        db.SaveChanges();
                        TempData["Message"] = "Model deleted successfully!";
                    }
                    else
                    {
                        TempData["MessageError"] = "The model you are trying to delete does not exist!";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a model!";
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
