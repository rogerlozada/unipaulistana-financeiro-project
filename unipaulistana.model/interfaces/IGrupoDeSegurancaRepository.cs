namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IGrupoDeSegurancaRepository
    {
        IEnumerable<GrupoDeSeguranca> ObterTodos();
        GrupoDeSeguranca ObterPorID(int grupoDeSegurancaID);
        void Adicionar(GrupoDeSeguranca grupoDeSeguranca);
        void Atualizar(GrupoDeSeguranca  grupoDeSeguranca);
        void Excluir(int grupoDeSegurancaID);
    } 
}
