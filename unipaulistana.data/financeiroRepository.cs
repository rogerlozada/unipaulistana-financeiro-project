namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class FinanceiroRepository : IFinanceiroRepository
    {
        public FinanceiroRepository(ConexaoContext conexao)
        {
            this.conexao = conexao;
        }

        readonly ConexaoContext conexao;

        public IEnumerable<Financeiro> ObterTodos()
        {
            var cmd = new SqlCommand(@"select a.financeiroID, a.DataDeVencimento, a.Valor, a.ClienteID, a.Status, b.nome as nomeCliente 
                                        from financeiro a
                                        inner join cliente b on (a.clienteID = b.ClienteID)", this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Financeiro( Convert.ToInt32(sqlDataReader.GetValue(0)),
                                             Convert.ToDateTime(sqlDataReader.GetValue(1)),
                                             Convert.ToDouble(sqlDataReader.GetValue(2)),
                                             (StatusTitulo)Convert.ToInt32(sqlDataReader.GetValue(3)),
                                             Convert.ToInt32(sqlDataReader.GetValue(4)),
                                             sqlDataReader.GetValue(5).ToString());
        }

        public Financeiro ObterPorID(int financeiroID)
        {
            string sql = string.Format(@"select a.financeiroID, a.DataDeVencimento, a.Valor, a.ClienteID, a.Status, b.nome as nomeCliente 
                                        from financeiro a
                                        inner join cliente b on (a.clienteID = b.ClienteID) where a.financeiroID={0}", financeiroID);

            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var financeiro = new Financeiro();
            while (sqlDataReader.Read())
            {
                financeiro = new Financeiro( Convert.ToInt32(sqlDataReader.GetValue(0)),
                                             Convert.ToDateTime(sqlDataReader.GetValue(1)),
                                             Convert.ToDouble(sqlDataReader.GetValue(2)),
                                             (StatusTitulo)Convert.ToInt32(sqlDataReader.GetValue(3)),
                                             Convert.ToInt32(sqlDataReader.GetValue(4)),
                                             sqlDataReader.GetValue(5).ToString());
            }

            return financeiro;
        }

        public void Adicionar(Financeiro financeiro)
        {
            string query = @"insert into dbo.financeiro (DataDeVencimento, Valor, ClienteID, Status) 
                                    VALUES (@DataDeVencimento, @Valor, @ClienteID, @Status) ";
            
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@DataDeVencimento", SqlDbType.DateTime).Value = financeiro.DataDeVencimento;
            cmd.Parameters.Add("@Valor", SqlDbType.Float).Value = financeiro.Valor;
            cmd.Parameters.Add("@ClienteID", SqlDbType.Int).Value = financeiro.ClienteID;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = (int)financeiro.Status;
            cmd.ExecuteNonQuery();
        } 

        public void Atualizar(Financeiro financeiro)
        {
            string query = string.Format(@"update dbo.financeiro set DataDeVencimento=@DataDeVencimento,
                                                                     Valor=@Valor,
                                                                     ClienteID=@ClienteID,
                                                                     Status=@Status
                                                            where financeiroID={0}", financeiro.FinanceiroID);

            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@DataDeVencimento", SqlDbType.DateTime).Value = financeiro.DataDeVencimento;
            cmd.Parameters.Add("@Valor", SqlDbType.Float).Value = financeiro.Valor;
            cmd.Parameters.Add("@ClienteID", SqlDbType.Int).Value = financeiro.ClienteID;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = (int)financeiro.Status;
            cmd.ExecuteNonQuery();
        } 

        public void Excluir(int financeiroID)
        {
            string excluiSQL = string.Format(@"delete from financeiro where financeiroID={0}",financeiroID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        } 
    }
}
