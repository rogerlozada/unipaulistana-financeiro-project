namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class GrupoDeSegurancaRepository : IGrupoDeSegurancaRepository
    {
        public GrupoDeSegurancaRepository(ConexaoContext conexao)
        {
            this.conexao = conexao;
        }
        readonly ConexaoContext conexao;

        public void Adicionar(GrupoDeSeguranca grupoDeSeguranca)
        {
            string query = "insert into dbo.grupoDeSeguranca (Nome) VALUES (@Nome) ";
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = grupoDeSeguranca.Nome;
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(GrupoDeSeguranca grupoDeSeguranca)
        {
            string query = string.Format("update dbo.grupoDeSeguranca set Nome=@Nome where grupoDeSegurancaID={0}", grupoDeSeguranca.GrupoDeSegurancaID);
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = grupoDeSeguranca.Nome;
            cmd.ExecuteNonQuery();
        }

        public void Excluir(int grupoDeSegurancaID)
        {
            string excluiSQL = string.Format(@"delete from grupoDeSeguranca where grupoDeSegurancaID={0}",grupoDeSegurancaID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        }

        public GrupoDeSeguranca ObterPorID(int grupoDeSegurancaID)
        {
           string sql = string.Format("select * from grupoDeSeguranca where grupoDeSegurancaID={0}", grupoDeSegurancaID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var grupoDeSeguranca = new GrupoDeSeguranca();
            while (sqlDataReader.Read())
            {
                grupoDeSeguranca = new GrupoDeSeguranca(Convert.ToInt32(sqlDataReader["grupoDeSegurancaID"]), sqlDataReader["nome"].ToString());
            }
            return grupoDeSeguranca;
        }

        public IEnumerable<GrupoDeSeguranca> ObterTodos()
        {
            var cmd = new SqlCommand("select * from grupoDeSeguranca", this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new GrupoDeSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }
    }
}
