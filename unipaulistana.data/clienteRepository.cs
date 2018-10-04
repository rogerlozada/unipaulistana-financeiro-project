namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class ClienteRepository : IClienteRepository
    {
        public ClienteRepository(ConexaoContext conexao)
        {
            this.conexao = conexao;
        }

        readonly ConexaoContext conexao;

        public IEnumerable<Cliente> ObterTodos()
        {
            var cmd = new SqlCommand("select * from cliente", this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Cliente( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }

        public Cliente ObterPorID(int clienteID)
        {
            string sql = string.Format("select * from cliente where clienteID={0}", clienteID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var cliente = new Cliente();
            while (sqlDataReader.Read())
            {
                cliente = new Cliente(Convert.ToInt32(sqlDataReader["clienteID"]), sqlDataReader["nome"].ToString());
            }
            return cliente;
        }

        public void Adicionar(Cliente cliente)
        {
            string query = "insert into dbo.cliente (Nome) VALUES (@Nome) ";
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = cliente.Nome;
            cmd.ExecuteNonQuery();
        } 

        public void Atualizar(Cliente cliente)
        {
            string query = string.Format("update dbo.cliente set Nome=@Nome where ClienteID={0}", cliente.ClienteID);
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = cliente.Nome;
            cmd.ExecuteNonQuery();
        } 

        public void Excluir(int clienteID)
        {
            string excluiSQL = string.Format(@"delete from cliente where ClienteID={0}",clienteID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        } 
    }
}
