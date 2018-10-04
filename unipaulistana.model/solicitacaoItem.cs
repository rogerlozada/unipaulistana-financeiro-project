namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SolicitacaoItem
    {
        public SolicitacaoItem(){}

        public SolicitacaoItem(int solicitacaoID, 
                               string descricao,
                               int usuarioID)
        {
            this.SolicitacaoID = solicitacaoID;
            this.Descricao = descricao;
            this.UsuarioID = usuarioID;
        }

        public SolicitacaoItem(int solicitacaoID)
        {
            this.SolicitacaoID = solicitacaoID;
        }

        public SolicitacaoItem(int solicitacaoItemID, 
                                int solicitacaoID, 
                                DateTime data,
                                string descricao,
                                int usuarioID,
                                string nomeUsuario)
        {
            this.SolicitacaoItemID = solicitacaoID;
            this.SolicitacaoID = solicitacaoID;
            this.Data = data;
            this.Descricao = descricao;
            this.UsuarioID = usuarioID;
            this.NomeUsuario = nomeUsuario;
        }

        public int SolicitacaoItemID { get; set; }

        [Display(Name="Protocolo")]
        public int SolicitacaoID { get; set; }
        
        public string NomeUsuario { get; set; }
        
        public DateTime Data { get; set; }

        [Display(Name="Descrição")]
        [Required(ErrorMessage="O campo descrição é obrigatório")]
        [StringLength(8000, ErrorMessage = "A descrição deve conter entre 5 e 8000 caracteres", MinimumLength = 5)]
        public string Descricao { get; set; }

        [Display(Name="Usuário")]
        public int UsuarioID { get; set; }
    }
}








