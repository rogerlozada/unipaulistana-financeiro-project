namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;

    public interface IClienteService
    {
        IEnumerable<Cliente> ObterTodos();
        Cliente ObterPorID(int clienteID);
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int clienteID);
    } 
}
