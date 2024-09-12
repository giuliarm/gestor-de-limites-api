using System.ComponentModel.DataAnnotations;

namespace GestorDeLimites.Application.Models.Requests
{
    public class CadastrarLimiteRequest
    {

        [Required(ErrorMessage = "O campo Documento é obrigatório.")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo Agencia é obrigatório.")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "O campo Conta é obrigatório.")]
        public string Conta { get; set; }

        [Required(ErrorMessage = "O campo Limite é obrigatório.")]
        public decimal Limite { get; set; }

    }
}
