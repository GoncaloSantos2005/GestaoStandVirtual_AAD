using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandVirtual.Models
{
    public class AutomovelAnuncioViewModel
    {
        public Anuncio Anuncio { get; set; }
        public Automovel Automovel { get; set; }
    }
    public class UsuarioContactoViewModel
    {
        public Usuario Usuario { get; set; }
        public List<Contacto> Contactos { get; set; } = new List<Contacto>();
    }
    public class ClienteComContacto
    {
        public int UserID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? NumContacto { get; set; }
        public string DescricaoTipoContacto { get; set; }

    }

    public class ClienteDetalhesViewModel
    {
        public int ClienteID { get; set; }
        public string NomeCliente { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacaoCliente { get; set; }
        public int? Contacto { get; set; }
        public string TipoContacto { get; set; }
        public int? IDAnuncio { get; set; }
        public string TituloAnuncio { get; set; }
        public int? PrecoAnuncio { get; set; }
        public string Matricula { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int? AnoAutomovel { get; set; }
        public string Cor { get; set; }
        public string Combustivel { get; set; }
    }


}