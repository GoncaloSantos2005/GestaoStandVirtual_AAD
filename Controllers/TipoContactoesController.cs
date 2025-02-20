﻿using System;
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
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    var tipoContactos = db.TipoContacto.ToList();
                    return View(tipoContactos);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view contact types!";
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoContactoes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. ID is required.";
                        return RedirectToAction("Index");
                    }

                    TipoContacto tipoContacto = db.TipoContacto.Find(id);
                    if (tipoContacto == null)
                    {
                        TempData["MessageError"] = "Contact type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoContacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to view contact type details!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: TipoContactoes/Create
        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    // Preenchendo o ViewBag com os dados necessários

                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to validate a model/version!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You have to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoContactoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoContacto1,DescricaoTipoContacto")] TipoContacto tipoContacto)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (ModelState.IsValid)
                    {
                        tipoContacto.TipoContacto1 = 0;
                        db.TipoContacto.Add(tipoContacto);
                        db.SaveChanges();

                        TempData["Message"] = "Contact type created successfully!";
                        return RedirectToAction("Create", "TipoContactoes");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form and try again.";
                    return View(tipoContacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to create a contact type!";
                    return RedirectToAction("View");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        // GET: TipoContactoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. ID is required.";
                        return RedirectToAction("Index");
                    }

                    TipoContacto tipoContacto = db.TipoContacto.Find(id);
                    if (tipoContacto == null)
                    {
                        TempData["MessageError"] = "Contact type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoContacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a contact type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoContactoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoContacto1,DescricaoTipoContacto")] TipoContacto tipoContacto)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(tipoContacto).State = EntityState.Modified;
                        db.SaveChanges();

                        TempData["Message"] = "Contact type updated successfully!";
                        return RedirectToAction("Index");
                    }

                    TempData["MessageError"] = "Invalid data. Please review the form and try again.";
                    return View(tipoContacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to edit a contact type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // GET: TipoContactoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1") // Verificar se o utilizador tem permissões para eliminar
                {
                    if (id == null)
                    {
                        TempData["MessageError"] = "Invalid request. ID is required.";
                        return RedirectToAction("Index");
                    }

                    TipoContacto tipoContacto = db.TipoContacto.Find(id);
                    if (tipoContacto == null)
                    {
                        TempData["MessageError"] = "Contact type not found.";
                        return RedirectToAction("Index");
                    }

                    return View(tipoContacto);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a contact type!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: TipoContactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1") // Verificar se o utilizador tem permissões para eliminar
                {
                    TipoContacto tipoContacto = db.TipoContacto.Find(id);
                    if (tipoContacto == null)
                    {
                        TempData["MessageError"] = "Contact type not found.";
                        return RedirectToAction("Index");
                    }

                    db.TipoContacto.Remove(tipoContacto);
                    db.SaveChanges();

                    TempData["Message"] = $"Contact type '{tipoContacto.TipoContacto1}' deleted successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to delete a contact type!";
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
