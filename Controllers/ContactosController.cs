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
    public class ContactosController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Contactos/Index
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var contacto = db.Contacto.Include(c => c.TipoContacto).Include(c => c.Usuario).ToList();
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to view contacts.";
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Contactos/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid contact ID.";
                        return RedirectToAction("Index");
                    }
                    Contacto contacto = db.Contacto.Find(id);
                    if (contacto == null)
                    {
                        TempData["MessageError"] = "Contact not found.";
                        return RedirectToAction("Index");
                    }
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to view contact details.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: Contactos/Create
        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto");
                    ViewBag.UserID = new SelectList(db.Usuario, "UserID", "Email");
                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to create contacts.";
                    return RedirectToAction("Index", "Contactos");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Contactos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contacto1,NumContacto,TipoContactoID,UserID")] Contacto contacto)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        contacto.Contacto1 = 0;
                        db.Contacto.Add(contacto);
                        db.SaveChanges();

                        TempData["Message"] = "Contact created successfully!";
                        return RedirectToAction("Create");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form.";
                    ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto", contacto.TipoContactoID);
                    ViewBag.UserID = new SelectList(db.Usuario, "UserID", "Email", contacto.UserID);
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to create contacts.";
                    return RedirectToAction("Index", "Contactos");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Contactos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid contact ID.";
                        return RedirectToAction("Index");
                    }
                    Contacto contacto = db.Contacto.Find(id);
                    if (contacto == null)
                    {
                        TempData["MessageError"] = "Contact not found.";
                        return RedirectToAction("Index");
                    }
                    ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto", contacto.TipoContactoID);
                    ViewBag.UserID = new SelectList(db.Usuario, "UserID", "Email", contacto.UserID);
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to edit contacts.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Contactos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contacto1,NumContacto,TipoContactoID,UserID")] Contacto contacto)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(contacto).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Message"] = "Contact updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form.";
                    ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto", contacto.TipoContactoID);
                    ViewBag.UserID = new SelectList(db.Usuario, "UserID", "Email", contacto.UserID);
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to edit contacts.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: Contactos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid contact ID.";
                        return RedirectToAction("Index");
                    }
                    Contacto contacto = db.Contacto.Find(id);
                    if (contacto == null)
                    {
                        TempData["MessageError"] = "Contact not found.";
                        return RedirectToAction("Index");
                    }
                    return View(contacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to delete contacts.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    Contacto contacto = db.Contacto.Find(id);
                    if (contacto == null)
                    {
                        TempData["MessageError"] = "Contact not found.";
                        return RedirectToAction("Index");
                    }

                    db.Contacto.Remove(contacto);
                    db.SaveChanges();

                    TempData["Message"] = "Contact deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permission to delete contacts.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first.";
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
