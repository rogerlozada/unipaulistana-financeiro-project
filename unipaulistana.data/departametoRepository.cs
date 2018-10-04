namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class DepartamentoRepository : IDepartamentoRepository
    {
        public DepartamentoRepository (ConexaoContext conexao)
        {
            this.conexao = conexao;
        }
        readonly ConexaoContext conexao;

        public void Adicionar(Departamento departamento)
        {
            string query = "insert into dbo.departamento (Nome) VALUES (@Nome) ";
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = departamento.Nome;
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Departamento departamento)
        {
            string query = string.Format("update dbo.departamento set Nome=@Nome where departamentoID={0}", departamento.DepartamentoID);
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = departamento.Nome;
            cmd.ExecuteNonQuery();
        }

        public void Excluir(int departamentoID)
        {
            string excluiSQL = string.Format(@"delete from departamento where departamentoID={0}",departamentoID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        }

        public Departamento ObterPorID(int departamentoID)
        {
           string sql = string.Format("select * from departamento where departamentoID={0}", departamentoID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var departamento = new Departamento();
            while (sqlDataReader.Read())
            {
                departamento = new Departamento(Convert.ToInt32(sqlDataReader["departamentoID"]), sqlDataReader["nome"].ToString());
            }
            return departamento;
        }

        public IEnumerable<Departamento> ObterTodos()
        {
            var cmd = new SqlCommand("select * from departamento", this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Departamento( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }
    }
}
