namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Login
    {
        [Display(Name="Email")]
        [Required(ErrorMessage="Entre com um email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Senha")]
        [Required(ErrorMessage="Entre com uma senha.")]
        [StringLength(10, ErrorMessage = "O tamanho da senha deve conter entre 5 e 10 caracteres", MinimumLength = 5)]
        public string Senha { get; set; }
    }
}
