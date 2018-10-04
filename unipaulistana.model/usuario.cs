namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    { 
        public Usuario(){}

        public Usuario(int usuarioID, string nome, string email, string senha)
        {
            this.UsuarioID = usuarioID;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.ConfirmarSenha = senha;
        }

        public Usuario(int usuarioID, string nome, string email, string senha, string foto)
        {
            this.UsuarioID = usuarioID;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Foto = foto;
            this.ConfirmarSenha = senha;
        }

        public Usuario(int usuarioID, string nome, string email, string senha, string foto, int departamentoID, string nomeDepartamento,
                       int grupoDeSegurancaID, string nomeGrupoSeguranca)
        {
            this.UsuarioID = usuarioID;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
            this.Foto = foto;
            this.ConfirmarSenha = senha;
            this.DepartamentoID = departamentoID;
            this.Departamento = new Departamento(departamentoID, nomeDepartamento);
            this.GrupoDeSegurancaID = grupoDeSegurancaID;
            this.GrupoDeSeguranca = new GrupoDeSeguranca(grupoDeSegurancaID, nomeGrupoSeguranca);
        }

        [Display(Name="Usuário")]
        public int UsuarioID { get; set; }

        [Required(ErrorMessage="O campo nome é obrigatório")]
        public string Nome { get; set; }

        public string Foto { get; set; }

        [Display(Name="Departamento")]
        public int DepartamentoID { get; set; }
        public Departamento Departamento { get; set; }

        [Display(Name="Grupo de segurança")]
        public int GrupoDeSegurancaID { get; set; }
        
        public GrupoDeSeguranca GrupoDeSeguranca { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage="O campo email é obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage="Informe um email válido.")]
        public string Email { get; set; }

        [Display(Name="Senha")]
        [Required(ErrorMessage="O campo senha é requerido.")]
        [StringLength(10, ErrorMessage = "O tamanho da senha deve conter entre 5 e 10 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name="Confirmar senha")]
        [Required(ErrorMessage="O campo confirmar senha é requerido.")]
        [StringLength(10, ErrorMessage = "O tamanho da senha deve conter entre 5 e 10 caracteres", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage="A senha informada não é igual a senha confirmada.")]
        public string ConfirmarSenha { get; set; }

        public bool ContemUsuario()
            => this != null && this.UsuarioID != 0;

        public bool UsuarioAdmin()
            => this.Nome.ToLower() == "admin";
    }
}
