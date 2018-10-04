namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IClienteRepository
    {
        IEnumerable<Cliente> ObterTodos();
        Cliente ObterPorID(int clienteID);
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int clienteID);
    } 
}
