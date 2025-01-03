using StandVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StandVirtual.Controllers
{
    public class AutomovelAnuncioController : Controller
    {
        StandVirtualTestesEntities1 db = new StandVirtualTestesEntities1();
        // GET: Automovel/SellCar
        public ActionResult SellCar()
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }
            // Prepara os dados para a view
            var viewModel = new AutomovelAnuncioViewModel
            {
                Anuncio = new Anuncio(),
                Automovel = new Automovel()
            };
            // Preenche listas para os dropdowns na view
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao");
            ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao");
            ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "TipoCombustivel.DescricaoTC");
            ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao");

            return View(viewModel);
        }

        // POST: Automovel/SellCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellCar(AutomovelAnuncioViewModel viewModel)
        {
            // Verifica se o utilizador está autenticado
            if (Session["UserId"] == null)
            {
                TempData["MessageError"] = "You need to log in first!";
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cria o anúncio
                    viewModel.Anuncio.DataCriacao = DateTime.Now;
                    viewModel.Anuncio.DataExpiracao = DateTime.Now.AddMonths(1);
                    viewModel.Anuncio.StatusID = 1;
                    viewModel.Anuncio.UserId = Convert.ToInt32(Session["UserId"]);

                    db.Anuncio.Add(viewModel.Anuncio);
                    db.SaveChanges();

                    // Associa o anúncio ao automóvel
                    viewModel.Automovel.AnuncioID = viewModel.Anuncio.AnuncioID;
                    db.Automovel.Add(viewModel.Automovel);              

                    
                    db.SaveChanges();
                    TempData["Message"] = "Car successfully listed!";
                    return RedirectToAction("AdList", "Anuncio");
                }
                    catch (Exception ex)
                {
                    TempData["MessageError"] = "An error occurred while listing the car. Please try again.";
                    System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
                }
            }

            // Recarrega as listas se algo falhar
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "TipoStatus.Descricao", viewModel.Anuncio.StatusID);
            ViewBag.ModeloID = new SelectList(db.Modelo, "ModeloID", "Descricao", viewModel.Automovel.ModeloID);
            ViewBag.CombustivelID = new SelectList(db.Combustivel, "CombustivelID", "TipoCombustivel.DescricaoTC", viewModel.Automovel.CombustivelID);
            ViewBag.TipoAutomovelID = new SelectList(db.TipoAutomovel, "TipoAutomovelID", "Descricao", viewModel.Automovel.TipoAutomovelID);

            return View(viewModel);
        }
    }
}