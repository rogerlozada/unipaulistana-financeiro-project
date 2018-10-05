namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class FinanceiroService : IFinanceiroService
    {
        public FinanceiroService(IFinanceiroRepository repository)
        {
            this.repository = repository;
        }

        readonly IFinanceiroRepository repository;

        public IEnumerable<Financeiro> ObterTodos()
            => this.repository.ObterTodos();

        public Financeiro ObterPorID(int financeiroID)
            => this.repository.ObterPorID(financeiroID);

        public void Adicionar(Financeiro financeiro)
        {
            this.repository.Adicionar(financeiro);
        } 

        public void Atualizar(Financeiro financeiro)
        {
            this.repository.Atualizar(financeiro);
        } 

        public void Excluir(int financeiroID)
        {
            this.repository.Excluir(financeiroID);
        }

        public IEnumerable<Financeiro> Filtrar(FinanceiroFiltrar filtrar)
            => this.repository.Filtrar(filtrar);
    }
}
