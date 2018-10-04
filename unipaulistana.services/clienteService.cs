namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public class ClienteService : IClienteService
    {
        public ClienteService(IClienteRepository repository)
        {
            this.repository = repository;
        }

        readonly IClienteRepository repository;

        public IEnumerable<Cliente> ObterTodos()
            => this.repository.ObterTodos();

        public Cliente ObterPorID(int clienteID)
            => this.repository.ObterPorID(clienteID);

        public void Adicionar(Cliente cliente)
        {
            this.repository.Adicionar(cliente);
        } 

        public void Atualizar(Cliente cliente)
        {
            this.repository.Atualizar(cliente);
        } 

        public void Excluir(int clienteID)
        {
            this.repository.Excluir(clienteID);
        } 
    }
}
