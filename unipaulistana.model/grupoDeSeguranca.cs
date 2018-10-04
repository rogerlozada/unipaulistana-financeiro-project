namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GrupoDeSeguranca
    {
        public GrupoDeSeguranca(){}

        public GrupoDeSeguranca(int grupoDeSegurancaID, string nome)
        {
            this.GrupoDeSegurancaID = grupoDeSegurancaID;
            this.Nome = nome;
        }

        public int GrupoDeSegurancaID { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage="O campo nome é obrigatório")]
        [StringLength(255, ErrorMessage = "O nome deve conter entre 5 e 10 caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}








