namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Cliente
    {
        public Cliente(){}

        public Cliente(int clienteID, string nome)
        {
            this.ClienteID = clienteID;
            this.Nome = nome;
        }

        public int ClienteID { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage="O campo nome é obrigatório")]
        [StringLength(255, ErrorMessage = "O nome deve conter entre 5 e 10 caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}
