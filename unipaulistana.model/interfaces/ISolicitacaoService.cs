namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface ISolicitacaoService
    {
        IEnumerable<Solicitacao> ObterTodos();
        Solicitacao ObterPorID(int solicitacaoID);
        int Adicionar(Solicitacao solicitacao, int solicitanteID);
        void Atualizar(Solicitacao solicitacao);
        void Excluir(int solicitacaoID);
        void Concluir(int solicitacaoID);
        IEnumerable<SolicitacaoItem> ObterPorSolicitacaoItens(int solicitacaoID);
        void AdicionarItem(SolicitacaoItem solicitacaoItem, int usuarioID);
        IEnumerable<Solicitacao> ObterStatusEmAbertoPorUsuario(int usuarioID);
        IEnumerable<Solicitacao> Filtrar(SolicitacaoPesquisar pesquisar);
    } 
}
