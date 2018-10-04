namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class SolicitacaoService : ISolicitacaoService
    {
        public SolicitacaoService(ISolicitacaoRepository repository, IUsuarioRepository usuarioRepository)
        {
            this.repository = repository;
            this.usuarioRepository = usuarioRepository;
        }

        readonly ISolicitacaoRepository repository;
        readonly IUsuarioRepository usuarioRepository;

        public IEnumerable<Solicitacao> ObterTodos()
            => this.repository.ObterTodos();

        public Solicitacao ObterPorID(int solicitacaoID)
            => this.repository.ObterPorID(solicitacaoID);

        public int Adicionar(Solicitacao solicitacao, int solicitanteID)
        {
            solicitacao.SolicitanteID = solicitanteID;
            solicitacao.UsuarioID = solicitanteID;
            solicitacao.DataDeCriacao = DateTime.Now;
            return this.repository.Adicionar(solicitacao);
        } 

        public void Atualizar(Solicitacao solicitacao)
        {
            AlteracaoDeUsuario AlteracaoDeUsuario = this.TeveAlteracaoDeUsuario(solicitacao);
            if (AlteracaoDeUsuario.TeveAlteracao)
                this.InserirItemTrocaDeUsuario(AlteracaoDeUsuario.Solicitacao, solicitacao);

            this.repository.Atualizar(solicitacao);
            if(solicitacao.EstaFinalizada())
                this.Concluir(solicitacao.SolicitacaoID);
        } 

        public void Excluir(int solicitacaoID)
        {
            this.repository.Excluir(solicitacaoID);
        }

        public void Concluir(int solicitacaoID)
        {
            this.repository.Concluir(solicitacaoID);
        }

        public IEnumerable<SolicitacaoItem> ObterPorSolicitacaoItens(int solicitacaoID)
            => this.repository.ObterPorSolicitacaoItens(solicitacaoID);

        public void AdicionarItem(SolicitacaoItem solicitacaoItem, int usuarioID)
        {
            this.repository.AdicionarItem(solicitacaoItem, usuarioID);
        }

        public IEnumerable<Solicitacao> ObterStatusEmAbertoPorUsuario(int usuarioID)
            => this.repository.ObterStatusEmAbertoPorUsuario(usuarioID);


        public AlteracaoDeUsuario TeveAlteracaoDeUsuario(Solicitacao dados)
        {
            Solicitacao solicitacao = this.repository.ObterPorID(dados.SolicitacaoID);
            return new AlteracaoDeUsuario(solicitacao.UsuarioID != dados.UsuarioID, solicitacao);
        }

        public void InserirItemTrocaDeUsuario(Solicitacao solicitacaoAtual, Solicitacao solicitacaoNova)
        {
            Usuario usuarioSolicitacaoAtual = this.usuarioRepository.ObterPorID(solicitacaoAtual.UsuarioID);
            Usuario usuarioSolicitacaoNova = this.usuarioRepository.ObterPorID(solicitacaoNova.UsuarioID);

            string descricao = string.Format("tarefa repassada do usuário {0} para o usuário {1}", usuarioSolicitacaoAtual.Nome, usuarioSolicitacaoNova.Nome);

            var solicitacaoItem = new SolicitacaoItem(solicitacaoAtual.SolicitacaoID, descricao, solicitacaoAtual.UsuarioID);
            this.AdicionarItem(solicitacaoItem, solicitacaoAtual.UsuarioID);
        }

        public IEnumerable<Solicitacao> Filtrar(SolicitacaoPesquisar pesquisar)
            => this.repository.Filtrar(pesquisar);
    }
}
