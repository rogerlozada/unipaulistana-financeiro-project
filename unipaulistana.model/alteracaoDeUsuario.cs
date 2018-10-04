namespace unipaulistana.model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AlteracaoDeUsuario
    {
        public AlteracaoDeUsuario(bool teveAlteracao, Solicitacao solicitacao)
        {
            this.TeveAlteracao = teveAlteracao;
            this.Solicitacao = solicitacao;
        }

        public bool TeveAlteracao { get; private set; }

        public Solicitacao Solicitacao { get; private set; }
    }
}
