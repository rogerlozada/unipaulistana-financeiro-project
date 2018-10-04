namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class DiretivaSegurancaRepository : IDiretivaSegurancaRepository
    {
        public DiretivaSegurancaRepository (ConexaoContext conexao)
        {
            this.conexao = conexao;
        }
        readonly ConexaoContext conexao;


        public void AdicionarPermissao(DiretivaSeguranca diretivaSeguranca)
        {
            string query = @"insert into dbo.grupoDiretivaSeguranca (DiretivaSegurancaID, GrupoDeSegurancaID) 
                                values (@DiretivaSegurancaID, @GrupoDeSegurancaID)";
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@DiretivaSegurancaID", SqlDbType.Int).Value = diretivaSeguranca.DiretivaSegurancaID;
            cmd.Parameters.Add("@GrupoDeSegurancaID", SqlDbType.Int).Value = diretivaSeguranca.GrupoSegurancaID;
            cmd.ExecuteNonQuery();
        }

        public void RemoverPermissao(DiretivaSeguranca diretivaSeguranca)
        {
             string excluiSQL = string.Format(@"delete from grupoDiretivaSeguranca where DiretivaSegurancaID={0} 
                                                                                   and GrupoDeSegurancaID={1}",
                                                                                   diretivaSeguranca.DiretivaSegurancaID,
                                                                                   diretivaSeguranca.GrupoSegurancaID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        }

        public DiretivaSeguranca ObterPorID(int diretivaSegurancaID)
        {
           string sql = string.Format("select * from diretivaSeguranca where diretivaSegurancaID={0}", diretivaSegurancaID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var diretivaSeguranca = new DiretivaSeguranca();
            while (sqlDataReader.Read())
            {
                diretivaSeguranca = new DiretivaSeguranca(Convert.ToInt32(sqlDataReader["diretivaSegurancaID"]), sqlDataReader["nome"].ToString());
            }
            return diretivaSeguranca;
        }

        public IEnumerable<DiretivaSeguranca> ObterTodos()
        {
            var cmd = new SqlCommand("select * from diretivaSeguranca", this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new DiretivaSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }

        public IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasAoGrupoDoUsuario(int usuarioID)
        {
            var cmd = new SqlCommand(string.Format(@"select * from diretivaSeguranca 
                                        where DiretivaSegurancaID not in 
	                                        (select DiretivaSegurancaID from grupoDiretivaSeguranca 
			                                    where GrupoDeSegurancaID = (select grupoDeSegurancaID from usuario where usuarioID={0}))",usuarioID), 
                                                this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new DiretivaSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }


        public IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupoDoUsuario(int usuarioID)
        {
            var cmd = new SqlCommand(string.Format(@"select * from diretivaSeguranca 
                                        where DiretivaSegurancaID in 
	                                        (select DiretivaSegurancaID from grupoDiretivaSeguranca 
			                                    where GrupoDeSegurancaID = (select grupoDeSegurancaID from usuario where usuarioID={0}))",usuarioID), 
                                                this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new DiretivaSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }

        public IEnumerable<DiretivaSeguranca> ObterDiretivasNaoAssociadasGrupo(int grupoID)
        {
            string sql = string.Format(@"select * from diretivaSeguranca 
                                        where DiretivaSegurancaID not in 
	                                        (select DiretivaSegurancaID from grupoDiretivaSeguranca where grupoDeSegurancaID={0})",grupoID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new DiretivaSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }

        public IEnumerable<DiretivaSeguranca> ObterDiretivasAssociadasGrupo(int grupoID)
        {
            var cmd = new SqlCommand(string.Format(@"select * from diretivaSeguranca 
                                        where DiretivaSegurancaID in 
	                                        (select DiretivaSegurancaID from grupoDiretivaSeguranca where grupoDeSegurancaID={0})",grupoID), 
                                                this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new DiretivaSeguranca( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString());
        }
    }
}
