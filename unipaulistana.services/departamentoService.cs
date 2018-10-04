namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class DepartamentoService : IDepartamentoService
    {
        public DepartamentoService(IDepartamentoRepository repository)
        {
            this.repository = repository;
        }

        readonly IDepartamentoRepository repository;

        public IEnumerable<Departamento> ObterTodos()
            => this.repository.ObterTodos();

        public Departamento ObterPorID(int departamentoID)
            => this.repository.ObterPorID(departamentoID);

        public void Adicionar(Departamento departamento)
        {
            this.repository.Adicionar(departamento);
        } 

        public void Atualizar(Departamento departamento)
        {
            this.repository.Atualizar(departamento);
        } 

        public void Excluir(int departamentoID)
        {
            this.repository.Excluir(departamentoID);
        } 
    }
}
