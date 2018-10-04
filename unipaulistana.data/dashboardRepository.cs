namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class DashboardRepository : IDashboardRepository
    {
        public DashboardRepository (ConexaoContext conexao)
        {
            this.conexao = conexao;
        }
        readonly ConexaoContext conexao;


        public Dashboard DashboardUsuario(int usuarioID)
        {
           string sql = string.Format(@"SELECT 
                                        (SELECT COUNT(*) AS TotalUsuario FROM usuario) AS TotalUsuario,
                                        (SELECT COUNT(*) AS TotalCliente FROM cliente) AS TotalCliente,
                                        (SELECT COUNT(*) FROM solicitacao WHERE ClienteID = {0} AND Status <> {1}) AS TotalSolicitacaoEmAberto
                                    ", usuarioID, (int)StatusSolicitacao.concluido);

            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var dashboard = new Dashboard();
            while (sqlDataReader.Read())
            {
                dashboard = new Dashboard(Convert.ToInt32(sqlDataReader["TotalUsuario"]),
                                            Convert.ToInt32(sqlDataReader["TotalCliente"]),
                                            Convert.ToInt32(sqlDataReader["TotalSolicitacaoEmAberto"]));
            }
            return dashboard;
        }


    }
}
