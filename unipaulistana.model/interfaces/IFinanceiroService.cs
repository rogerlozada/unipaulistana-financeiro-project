namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IFinanceiroService
    {
        IEnumerable<Financeiro> ObterTodos();
        Financeiro ObterPorID(int financeiroID);
        void Adicionar(Financeiro financeiro);
        void Atualizar(Financeiro financeiro);
        void Excluir(int financeiroID);
        IEnumerable<Financeiro> Filtrar(FinanceiroFiltrar filtrar);
    } 
}
