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
                    // Filling the ViewBag with the necessary data
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

        public ActionResult Contacts()
        {
            return View();
        }


        public ActionResult RegisterClient()
        {
            // Populate the list of contact types for the dropdown
            ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto");
            ViewBag.CodPostalId = new SelectList(db.CodigoPostal, "CodPostalId", "Localizacao");

            var viewModel = new UsuarioContactoViewModel
            {
                Usuario = new Usuario(),
                Contactos = new List<Contacto> { new Contacto() } // An empty contact to start with
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClient(UsuarioContactoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Add the client (User)
                    viewModel.Usuario.DataCriacao = DateTime.Now;
                    viewModel.Usuario.PermID = 3; // Cliente
                    db.Usuario.Add(viewModel.Usuario);
                    db.SaveChanges();

                    // Add associated contacts
                    foreach (var contacto in viewModel.Contactos)
                    {
                        contacto.UserID = viewModel.Usuario.UserID; // Associates with the client
                        db.Contacto.Add(contacto);
                    }

                    db.SaveChanges();

                    TempData["Message"] = "Customer and contacts registered successfully!";
                    return RedirectToAction("Contacts");
                }
                catch (Exception ex)
                {
                    TempData["MessageError"] = $"Error registering client: {ex.Message}";
                }
            }

            // Reload contact types if something fails
            ViewBag.TipoContactoID = new SelectList(db.TipoContacto, "TipoContacto1", "DescricaoTipoContacto");
            ViewBag.CodPostalId = new SelectList(db.CodigoPostal, "CodPostalId", "Localizacao", viewModel.Usuario.CodigoPostal);
            return View(viewModel);
        }


        public List<ClienteComContacto> ListTClient()
        {
            using (db)
            {   
                return db.Database.SqlQuery<ClienteComContacto>("spListarClientes").ToList();
            }
        }

        public ActionResult ListClient()
        {
            var clientes = ListTClient();
            return View(clientes);
        }

        public ActionResult ListClientDetails()
        {
            var query = @"
            SELECT DISTINCT 
                u.UserID AS ClienteID,
                u.Nome AS NomeCliente,
                u.Email AS Email,
                u.DataCriacao AS DataCriacaoCliente,
                (SELECT TOP 1 c.NumContacto 
                 FROM Contacto c 
                 WHERE c.UserID = u.UserID 
                 ORDER BY c.NumContacto) AS Contacto,
                (SELECT TOP 1 tc.DescricaoTipoContacto 
                 FROM Contacto c 
                 JOIN TipoContacto tc ON c.TipoContactoID = tc.TipoContacto 
                 WHERE c.UserID = u.UserID 
                 ORDER BY c.NumContacto) AS TipoContacto,
                a.AnuncioID AS IDAnuncio,
                a.Titulo AS TituloAnuncio,
                a.Preco AS PrecoAnuncio,
                a.DataCriacao AS DataAnuncio,
                aut.MatriculaAut AS Matricula,
                aut.Ano AS AnoAutomovel,
                aut.Cor AS CorAutomovel,
                mod.Descricao AS Modelo,
                mar.NomeMarca AS Marca,
                tcomb.DescricaoTC AS Combustivel
            FROM Usuario u
            LEFT JOIN Anuncio a ON u.UserID = a.UserId  
            LEFT JOIN Automovel aut ON a.AnuncioID = aut.AnuncioID
            LEFT JOIN Modelo mod ON aut.ModeloID = mod.ModeloID
            LEFT JOIN Marca mar ON mod.MarcaID = mar.MarcaID
            LEFT JOIN Combustivel comb ON aut.CombustivelID = comb.CombustivelID
            LEFT JOIN TipoCombustivel tcomb ON comb.TipoComID = tcomb.TipoComID
            ORDER BY u.Nome, a.DataCriacao DESC;
            ";





            // Execute the query and return the data as a list
            var detalhesClientes = db.Database.SqlQuery<ClienteDetalhesViewModel>(query).ToList();
            return View(detalhesClientes);
        }

    }
}
