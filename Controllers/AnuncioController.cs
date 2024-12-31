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
    public class AnuncioController : Controller
    {
        private StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();

        // GET: Anuncio
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2" || (string)Session["UserPerm"] == "3")
            {
                try
                {
                    var anuncio = db.Anuncio
                        .Include(a => a.Status)
                        .Include(a => a.Usuario);

                    if (!anuncio.Any())
                    {
                        TempData["MessageError"] = "No records found.";
                    }

                    return View(anuncio.ToList());
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = "An error occurred while retrieving the data.";
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return View(new List<Anuncio>());
                }
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Anuncio/Details/5
        public ActionResult Details(int? id)
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
                    TempData["MessageError"] = "Invalid ID!";
                    return RedirectToAction("Index");
                }

                try
                {
                    // Busca o anúncio pelo ID
                    Anuncio anuncio = db.Anuncio.Find(id);

                    if (anuncio == null)
                    {
                        TempData["MessageError"] = "Announcement not found!";
                        return RedirectToAction("Index");
                    }

                    return View(anuncio);
                }
                catch (Exception ex)
                {
                    // Captura erros inesperados
                    TempData["MessageError"] = "An error occurred while retrieving the details.";
                    // Log opcional para depuração
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Anuncio/Create
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
                // Preenche as listas dropdown
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao");
                ViewBag.UserId = new SelectList(db.Usuario, "UserID", "Email");
                return View();
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Anuncio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnuncioID,Titulo,DataCriacao,DataExpiracao,NumFav,NumVisualizacoes,NumCliques,Preco,Foto,Video,Descricao,StatusID,UserId")] Anuncio anuncio)
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
                    try
                    {
                        // Adiciona o novo anúncio
                        db.Anuncio.Add(anuncio);
                        db.SaveChanges();
                        TempData["Message"] = "Announcement created successfully!";
                        return RedirectToAction("Create");
                    }
                    catch (Exception ex)
                    {
                        TempData["MessageError"] = "An error occurred while creating the announcement.";
                        // Log opcional para depuração
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

                // Recarrega as listas dropdown em caso de erro
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao", anuncio.StatusID);
                ViewBag.UserId = new SelectList(db.Usuario, "UserID", "Email", anuncio.UserId);
                return View(anuncio);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Anuncio/Edit/5
        public ActionResult Edit(int? id)
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
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Busca o anúncio pelo ID
                Anuncio anuncio = db.Anuncio.Find(id);
                if (anuncio == null)
                {
                    return HttpNotFound();
                }

                // Preenche as listas dropdown
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao", anuncio.StatusID);
                ViewBag.UserId = new SelectList(db.Usuario, "UserID", "Email", anuncio.UserId);

                return View(anuncio);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Anuncio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnuncioID,Titulo,DataCriacao,DataExpiracao,NumFav,NumVisualizacoes,NumCliques,Preco,Foto,Video,Descricao,StatusID,UserId")] Anuncio anuncio)
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
                    try
                    {
                        // Atualiza o anúncio na base de dados
                        anuncio.DataCriacao = DateTime.Now;
                        db.Entry(anuncio).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["Message"] = "Announcement updated successfully!";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        TempData["MessageError"] = "An error occurred while updating the announcement.";
                        // Log opcional para depuração
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }

                // Recarrega as listas dropdown em caso de erro
                ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao", anuncio.StatusID);
                ViewBag.UserId = new SelectList(db.Usuario, "UserID", "Email", anuncio.UserId);

                return View(anuncio);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }


        // GET: Anuncio/Delete/5
        public ActionResult Delete(int? id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Busca o anúncio pelo ID
                Anuncio anuncio = db.Anuncio.Find(id);
                if (anuncio == null)
                {
                    TempData["MessageError"] = "Announcement not found!";
                    return RedirectToAction("Index");
                }

                return View(anuncio);
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Anuncio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            // Verifica as permissões do utilizador
            if ((string)Session["UserPerm"] == "1" || (string)Session["UserPerm"] == "2")
            {
                try
                {
                    // Busca o anúncio pelo ID e remove
                    Anuncio anuncio = db.Anuncio.Find(id);
                    if (anuncio != null)
                    {
                        db.Anuncio.Remove(anuncio);
                        db.SaveChanges();
                        TempData["Message"] = "Announcement deleted successfully!";
                    }
                    else
                    {
                        TempData["MessageError"] = "Announcement not found!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = "An error occurred while trying to delete the announcement.";
                    // Log opcional para depuração
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                return RedirectToAction("Index");
            }
            else
            {
                TempData["MessageError"] = "You do not have permissions to access this page!";
                return RedirectToAction("Index", "Home");
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
