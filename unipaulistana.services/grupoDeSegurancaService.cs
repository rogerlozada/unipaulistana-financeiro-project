namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class GrupoDeSegurancaService : IGrupoDeSegurancaService
    {
        public GrupoDeSegurancaService(IGrupoDeSegurancaRepository repository)
        {
            this.repository = repository;
        }

        readonly IGrupoDeSegurancaRepository repository;

        public IEnumerable<GrupoDeSeguranca> ObterTodos()
            => this.repository.ObterTodos();

        public GrupoDeSeguranca ObterPorID(int grupoDeSegurancaID)
            => this.repository.ObterPorID(grupoDeSegurancaID);

        public void Adicionar(GrupoDeSeguranca grupoDeSeguranca)
        {
            this.repository.Adicionar(grupoDeSeguranca);
        } 

        public void Atualizar(GrupoDeSeguranca grupoDeSeguranca)
        {
            this.repository.Atualizar(grupoDeSeguranca);
        } 

        public void Excluir(int grupoDeSegurancaID)
        {
            this.repository.Excluir(grupoDeSegurancaID);
        } 
    }
}
