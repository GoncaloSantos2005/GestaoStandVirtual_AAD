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
    public class AutomovelController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Automovel
        public ActionResult Index()
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                // Obtém a lista de automóveis com os relacionamentos necessários
                var automovel = db.Automovel
                    .Include(a => a.Anuncio)
                    .Include(a => a.Combustivel)
                    .Include(a => a.Modelo)
                    .Include(a => a.TipoAutomovel);

                return View(automovel.ToList());
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Automovel/Details/5
        public ActionResult Details(string id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                if (id == null)
                {
                    TempData["MessageError"] = "Invalid request. No ID provided.";
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Busca o automóvel pelo ID
                Automovel automovel = db.Automovel.Find(id);

                if (automovel == null)
                {
                    TempData["MessageError"] = "Automobile not found.";
                    return HttpNotFound();
                }

                return View(automovel);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to view this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Automovel/Create
        public ActionResult Create()
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                // Preenche as listas para o dropdown
                ViewBag.AnuncioID = new SelectList(db.Anuncio, "AnuncioID", "Titulo");
                ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "TipoCombustivel.DescricaoTC");
                ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao");
                ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao");
                return View();
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to create a car!";
                return RedirectToAction("Index", "Automovel");
            }
        }

        // POST: Automovel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatriculaAut,Ano,NumPortas,Cor,Potencia,Cilindrada,TipoCaixa,Quilometros,TipoAutomovelID,ModeloID,CombustivelID,AnuncioID,Importado")] Automovel automovel)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                if (ModelState.IsValid)
                {
                    db.Automovel.Add(automovel);
                    db.SaveChanges();
                    TempData["Message"] = "Car successfully created!";
                    return RedirectToAction("Create");
                }

                // Se houver erros, volta a preencher os dropdowns
                TempData["MessageError"] = "There were errors in your submission. Please fix them and try again.";
                ViewBag.AnuncioID = new SelectList(db.Anuncio, "AnuncioID", "Titulo", automovel.AnuncioID);
                ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "TipoCombustivel.DescricaoTC", automovel.CombustivelID);
                ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao", automovel.ModeloID);
                ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao", automovel.TipoAutomovelID);
                return View(automovel);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to create a car!";
                return RedirectToAction("Index", "Automovel");
            }
        }


        // GET: Automovel/Edit/5
        public ActionResult Edit(string id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica se o utilizador tem permissões para editar
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Automovel automovel = db.Automovel.Find(id);
                if (automovel == null)
                {
                    return HttpNotFound();
                }

                ViewBag.AnuncioID = new SelectList(db.Anuncio, "AnuncioID", "Titulo", automovel.AnuncioID);
                ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "CombustivelID", automovel.CombustivelID);
                ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao", automovel.ModeloID);
                ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao", automovel.TipoAutomovelID);
                return View(automovel);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to edit a car!";
                return RedirectToAction("Index", "Automovel");
            }
        }

        // POST: Automovel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatriculaAut,Ano,NumPortas,Cor,Potencia,Cilindrada,TipoCaixa,Quilometros,TipoAutomovelID,ModeloID,CombustivelID,AnuncioID,Importado")] Automovel automovel)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica se o utilizador tem permissões para editar
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(automovel).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Car successfully updated!";
                    return RedirectToAction("Index");
                }

                TempData["MessageError"] = "There were errors in your submission. Please fix them and try again.";
                ViewBag.AnuncioID = new SelectList(db.Anuncio, "AnuncioID", "Titulo", automovel.AnuncioID);
                ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "CombustivelID", automovel.CombustivelID);
                ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao", automovel.ModeloID);
                ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao", automovel.TipoAutomovelID);
                return View(automovel);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to edit a car!";
                return RedirectToAction("Index", "Automovel");
            }
        }


        // GET: Automovel/Delete/5
        public ActionResult Delete(string id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica se o utilizador tem permissões para eliminar
            if ((string)Session["UserPerm"] == "1")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Automovel automovel = db.Automovel.Find(id);
                if (automovel == null)
                {
                    return HttpNotFound();
                }

                return View(automovel);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to delete a car!";
                return RedirectToAction("Index", "Automovel");
            }
        }

        // POST: Automovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica se o utilizador tem permissões para eliminar
            if ((string)Session["UserPerm"] == "1")
            {
                Automovel automovel = db.Automovel.Find(id);
                if (automovel == null)
                {
                    TempData["MessageError"] = "Car not found!";
                    return RedirectToAction("Index");
                }

                db.Automovel.Remove(automovel);
                db.SaveChanges();
                TempData["Message"] = "Car successfully deleted!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to delete a car!";
                return RedirectToAction("Index", "Automovel");
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
