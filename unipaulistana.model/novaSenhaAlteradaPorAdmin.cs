namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NovaSenhaAlteradaPorAdmin
    {
        
        public NovaSenhaAlteradaPorAdmin()
        {
            
        }

        public NovaSenhaAlteradaPorAdmin(int userID)
        {
            this.UserID = userID;
        }

        public int UserID { get; set; }

        [Display(Name="Senha")]
        [Required(ErrorMessage="Entre com uma senha.")]
        [StringLength(10, ErrorMessage = "O tamanho da senha deve conter entre 5 e 10 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
