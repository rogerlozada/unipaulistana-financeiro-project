namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Departamento
    {
        public Departamento(){}

        public Departamento(int departamentoID, string nome)
        {
            this.DepartamentoID = departamentoID;
            this.Nome = nome;
        }

        public int DepartamentoID { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage="O campo nome é obrigatório")]
        [StringLength(255, ErrorMessage = "O departamento deve conter entre 5 e 10 caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
    }
}








