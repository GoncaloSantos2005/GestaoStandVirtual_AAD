using StandVirtual.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StandVirtual.Controllers
{
    public class HomeController : Controller
    {
        StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario u)
        {
            // this action handles the post (login)
            if (ModelState.IsValid) //check if it is valid
            {
                using (StandVirtualTestesEntities1 dc = new StandVirtualTestesEntities1())
                {
                    var v = dc.Usuario.Where(a => a.Email.Equals(u.Email) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["UserId"] = v.UserID.ToString();
                        Session["UserNome"] = v.Nome.ToString();
                        Session["UserEmail"] = v.Email.ToString();
                        Session["UserPerm"] = v.PermID.ToString();
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["MessageError"] = "Invalid Username or Password! Please try again";
                        return View(u);
                    }

                }
            }
            return View(u);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Create()
        {
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    // Preenchendo o ViewBag com os dados necessários
                    ViewBag.CodPostalId = new SelectList(db.CodigoPostal, "CodPostalId", "Localizacao");
                    ViewBag.PermID = new SelectList(db.TipoPerm, "PermID", "Descricao");

                    return View();
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to validate a model/version!";
                    return RedirectToAction("Index", "Categorias");
                }
            }
            else
            {
                TempData["MessageError"] = "You have to log in first!";
                return RedirectToAction("Login", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Email, Usuario users)
        {
            ViewBag.CodPostalId = new SelectList(db.CodigoPostal, "CodPostalId", "Localizacao");
            ViewBag.PermID = new SelectList(db.TipoPerm, "PermID", "Descricao");
            if (Session["UserId"] != null)
            {
                if ((string)Session["UserPerm"] == "1")
                {
                    if (ModelState.IsValid)
                    {
                        bool nomeExistente = db.Usuario.Any(u => u.Email == Email);
                        users.DataCriacao = DateTime.Now;
                        users.UserID = 0;
                        if (!nomeExistente)
                            db.Usuario.Add(users);
                        else
                        {
                            TempData["MessageError"] = "There is already a user with that email!";
                            return View(users);
                        }
                        db.SaveChanges();

                        TempData["Message"] = "User " + users.Nome + " created successfully!";

                        return RedirectToAction("Create", "Home"); // Redirect to dashboard page after successful creation
                    }
                    return View(users);
                }
                else
                {
                    TempData["MessageError"] = "You do not have permissions to validate a model/version!";
                    return RedirectToAction("Index", "Categorias");
                }

            }
            else
            {
                TempData["MessageError"] = "You have to log in first!";
                return RedirectToAction("Login", "Home");
            }

        }
    }
}