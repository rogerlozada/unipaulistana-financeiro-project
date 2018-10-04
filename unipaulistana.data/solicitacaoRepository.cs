namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class SolicitacaoRepository : ISolicitacaoRepository
    {
        public SolicitacaoRepository(ConexaoContext conexao)
        {
            this.conexao = conexao;
        }

        readonly ConexaoContext conexao;

        public int Adicionar(Solicitacao solicitacao)
        {
            string query = @"insert into dbo.solicitacao (DataDeCriacao, Descricao, ClienteID, DepartamentoID, UsuarioID, Status, SolicitanteID) 
                                        values (@DataDeCriacao, @Descricao, @ClienteID, @DepartamentoID, @UsuarioID, @Status, @SolicitanteID); SELECT SCOPE_IDENTITY();";
            
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@DataDeCriacao", SqlDbType.DateTime, 255).Value = solicitacao.DataDeCriacao;
            cmd.Parameters.Add("@Descricao", SqlDbType.VarChar, 8000).Value = solicitacao.Descricao;
            cmd.Parameters.Add("@ClienteID", SqlDbType.Int).Value = solicitacao.ClienteID;
            cmd.Parameters.Add("@DepartamentoID", SqlDbType.Int).Value = solicitacao.DepartamentoID;
            cmd.Parameters.Add("@UsuarioID", SqlDbType.Int).Value = solicitacao.UsuarioID;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = StatusSolicitacao.nao_iniciado;
            cmd.Parameters.Add("@SolicitanteID", SqlDbType.Int).Value = solicitacao.SolicitanteID;
            
            return cmd.ExecuteNonQuery();
        }

        public void AdicionarItem(SolicitacaoItem solicitacaoItem, int usuarioID)
        {
            string query = @"insert into dbo.solicitacaoItem (SolicitacaoID, Data, Descricao, usuarioID) 
                                        values (@SolicitacaoID, @Data, @Descricao, @usuarioID)";
            
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@SolicitacaoID", SqlDbType.Int).Value = solicitacaoItem.SolicitacaoID;
            cmd.Parameters.Add("@Data", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Descricao", SqlDbType.VarChar, 8000).Value = solicitacaoItem.Descricao;
            cmd.Parameters.Add("@usuarioID", SqlDbType.Int).Value = usuarioID;
            cmd.ExecuteNonQuery();
        }

        public void Atualizar(Solicitacao solicitacao)
        {
            string query = string.Format(@"update dbo.solicitacao 
                                                set Descricao=@Descricao, 
                                                    ClienteID=@ClienteID, 
                                                    DepartamentoID=@DepartamentoID ,
                                                    UsuarioID=@UsuarioID, 
                                                    Status=@Status
                                                where SolicitacaoID={0}", solicitacao.SolicitacaoID);
                                                
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Descricao", SqlDbType.VarChar, 8000).Value = solicitacao.Descricao;
            cmd.Parameters.Add("@ClienteID", SqlDbType.Int).Value = solicitacao.ClienteID;
            cmd.Parameters.Add("@DepartamentoID", SqlDbType.Int).Value = solicitacao.DepartamentoID;
            cmd.Parameters.Add("@UsuarioID", SqlDbType.Int).Value = solicitacao.UsuarioID;
            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = (int)solicitacao.Status;
            cmd.ExecuteNonQuery();
        }

        public void Concluir(int solicitacaoID)
        {
            string query = string.Format(@"update dbo.solicitacao 
                                                set DataDeConclusao=@DataDeConclusao, 
                                                    Concluido=1 
                                                where SolicitacaoID={0}", solicitacaoID);
                                                
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@DataDeConclusao", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.ExecuteNonQuery();
        }

        public void Excluir(int solicitacaoID)
        {
            string excluiSQL = string.Format(@"delete from solicitacao where SolicitacaoID={0}",solicitacaoID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        }

        public Solicitacao ObterPorID(int solicitacaoID)
        {
            string sql = string.Format(@"select a.SolicitacaoID,
                                            a.DataDeCriacao,
                                            a.DataDeConclusao,
                                            a.Descricao,
                                            a.ClienteID,
                                            a.DepartamentoID,
                                            a.UsuarioID,
                                            a.SolicitanteID,
                                            a.Concluido, 
                                            a.Status,
                                            b.Nome as NomeUsuario,
                                            c.Nome as NomeCliente,
                                            d.Nome as NomeDepartamento,
                                            d.Nome as NomeSolicitante
                                        from solicitacao a
                                        inner join usuario b on (a.usuarioID = b.usuarioID)
                                        inner join cliente c on (a.clienteID = c.ClienteID)
                                        inner join departamento d on (a.departamentoID = d.departamentoID) 
                                        inner join usuario e on (a.usuarioID = e.usuarioID) where a.SolicitacaoID={0}", solicitacaoID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var solicitacao = new Solicitacao();
            while (sqlDataReader.Read())
            {
                solicitacao = new Solicitacao( Convert.ToInt32(sqlDataReader["SolicitacaoID"]),
                                                sqlDataReader["Descricao"].ToString(),
                                                Convert.ToDateTime(sqlDataReader["DataDeCriacao"]),
                                                sqlDataReader["DataDeConclusao"],
                                                Convert.ToBoolean(sqlDataReader["Concluido"]),
                                                Convert.ToInt32(sqlDataReader["ClienteID"]),
                                                Convert.ToInt32(sqlDataReader["departamentoID"]),
                                                Convert.ToInt32(sqlDataReader["UsuarioID"]),
                                                Convert.ToInt32(sqlDataReader["SolicitanteID"]),
                                                (StatusSolicitacao)Convert.ToInt32(sqlDataReader["Status"]),
                                                sqlDataReader["NomeUsuario"].ToString(),
                                                sqlDataReader["NomeCliente"].ToString(),
                                                sqlDataReader["NomeDepartamento"].ToString(),
                                                sqlDataReader["NomeSolicitante"].ToString());
            }
            return solicitacao;
        }

        public IEnumerable<SolicitacaoItem> ObterPorSolicitacaoItens(int solicitacaoID)
        {
            string sql = string.Format(@"select a.SolicitacaoID,
                                            a.solicitacaoItemID,
                                            a.SolicitacaoID,
                                            a.Data,
                                            a.Descricao,
                                            a.usuarioID,
                                            b.Nome as NomeUsuario
                                        from solicitacaoItem a
                                        inner join usuario b on (a.usuarioID = b.usuarioID) where a.SolicitacaoID={0}", solicitacaoID);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
            {
                yield return new SolicitacaoItem( Convert.ToInt32(sqlDataReader["solicitacaoItemID"]),
                                                Convert.ToInt32(sqlDataReader["SolicitacaoID"]),
                                                Convert.ToDateTime(sqlDataReader["Data"]),
                                                sqlDataReader["Descricao"].ToString(),
                                                Convert.ToInt32(sqlDataReader["UsuarioID"]),
                                                sqlDataReader["NomeUsuario"].ToString());
            }
        }

        public IEnumerable<Solicitacao> ObterTodos()
        {
           var cmd = new SqlCommand(@"select a.SolicitacaoID,
                                            a.DataDeCriacao,
                                            a.DataDeConclusao,
                                            a.Descricao,
                                            a.ClienteID,
                                            a.DepartamentoID,
                                            a.UsuarioID,
                                            a.SolicitanteID,
                                            a.Concluido, 
                                            a.Status,
                                            b.Nome as NomeUsuario,
                                            c.Nome as NomeCliente,
                                            d.Nome as NomeDepartamento,
                                            d.Nome as NomeSolicitante
                                        from solicitacao a
                                        inner join usuario b on (a.usuarioID = b.usuarioID)
                                        inner join cliente c on (a.clienteID = c.ClienteID)
                                        inner join departamento d on (a.departamentoID = d.departamentoID)
                                        inner join usuario e on (a.usuarioID = e.usuarioID)", this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Solicitacao( Convert.ToInt32(sqlDataReader["SolicitacaoID"]),
                                                sqlDataReader["Descricao"].ToString(),
                                                Convert.ToDateTime(sqlDataReader["DataDeCriacao"]),
                                                sqlDataReader["DataDeConclusao"],
                                                Convert.ToBoolean(sqlDataReader["Concluido"]),
                                                Convert.ToInt32(sqlDataReader["ClienteID"]),
                                                Convert.ToInt32(sqlDataReader["departamentoID"]),
                                                Convert.ToInt32(sqlDataReader["UsuarioID"]),
                                                Convert.ToInt32(sqlDataReader["SolicitanteID"]),
                                                (StatusSolicitacao)Convert.ToInt32(sqlDataReader["Status"]),
                                                sqlDataReader["NomeUsuario"].ToString(),
                                                sqlDataReader["NomeCliente"].ToString(),
                                                sqlDataReader["NomeDepartamento"].ToString(),
                                                sqlDataReader["NomeSolicitante"].ToString());

        }

        public IEnumerable<Solicitacao> ObterStatusEmAbertoPorUsuario(int usuarioID)
        {
           var cmd = new SqlCommand(string.Format(@"select a.SolicitacaoID,
                                            a.DataDeCriacao,
                                            a.DataDeConclusao,
                                            a.Descricao,
                                            a.ClienteID,
                                            a.DepartamentoID,
                                            a.UsuarioID,
                                            a.SolicitanteID,
                                            a.Concluido, 
                                            a.Status,
                                            b.Nome as NomeUsuario,
                                            c.Nome as NomeCliente,
                                            d.Nome as NomeDepartamento,
                                            d.Nome as NomeSolicitante
                                        from solicitacao a
                                        inner join usuario b on (a.usuarioID = b.usuarioID)
                                        inner join cliente c on (a.clienteID = c.ClienteID)
                                        inner join departamento d on (a.departamentoID = d.departamentoID)
                                        inner join usuario e on (a.usuarioID = e.usuarioID) 
                                        where a.status in(1,2,3)   
                                        and a.usuarioID={0}",usuarioID), this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Solicitacao( Convert.ToInt32(sqlDataReader["SolicitacaoID"]),
                                                sqlDataReader["Descricao"].ToString(),
                                                Convert.ToDateTime(sqlDataReader["DataDeCriacao"]),
                                                sqlDataReader["DataDeConclusao"],
                                                Convert.ToBoolean(sqlDataReader["Concluido"]),
                                                Convert.ToInt32(sqlDataReader["ClienteID"]),
                                                Convert.ToInt32(sqlDataReader["departamentoID"]),
                                                Convert.ToInt32(sqlDataReader["UsuarioID"]),
                                                Convert.ToInt32(sqlDataReader["SolicitanteID"]),
                                                (StatusSolicitacao)Convert.ToInt32(sqlDataReader["Status"]),
                                                sqlDataReader["NomeUsuario"].ToString(),
                                                sqlDataReader["NomeCliente"].ToString(),
                                                sqlDataReader["NomeDepartamento"].ToString(),
                                                sqlDataReader["NomeSolicitante"].ToString());

        }

        public IEnumerable<Solicitacao> Filtrar(SolicitacaoPesquisar pesquisar)
        {
            string sql = string.Format(@"select a.SolicitacaoID,
                                            a.DataDeCriacao,
                                            a.DataDeConclusao,
                                            a.Descricao,
                                            a.ClienteID,
                                            a.DepartamentoID,
                                            a.UsuarioID,
                                            a.SolicitanteID,
                                            a.Concluido, 
                                            a.Status,
                                            b.Nome as NomeUsuario,
                                            c.Nome as NomeCliente,
                                            d.Nome as NomeDepartamento,
                                            d.Nome as NomeSolicitante
                                        from solicitacao a
                                        inner join usuario b on (a.usuarioID = b.usuarioID)
                                        inner join cliente c on (a.clienteID = c.ClienteID)
                                        inner join departamento d on (a.departamentoID = d.departamentoID)
                                        inner join usuario e on (a.usuarioID = e.usuarioID) 
                                        where status={0}",(int)pesquisar.Status);

           var cmd = new SqlCommand(sql, this.conexao.ObterConexao());

            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Solicitacao( Convert.ToInt32(sqlDataReader["SolicitacaoID"]),
                                                sqlDataReader["Descricao"].ToString(),
                                                Convert.ToDateTime(sqlDataReader["DataDeCriacao"]),
                                                sqlDataReader["DataDeConclusao"],
                                                Convert.ToBoolean(sqlDataReader["Concluido"]),
                                                Convert.ToInt32(sqlDataReader["ClienteID"]),
                                                Convert.ToInt32(sqlDataReader["departamentoID"]),
                                                Convert.ToInt32(sqlDataReader["UsuarioID"]),
                                                Convert.ToInt32(sqlDataReader["SolicitanteID"]),
                                                (StatusSolicitacao)Convert.ToInt32(sqlDataReader["Status"]),
                                                sqlDataReader["NomeUsuario"].ToString(),
                                                sqlDataReader["NomeCliente"].ToString(),
                                                sqlDataReader["NomeDepartamento"].ToString(),
                                                sqlDataReader["NomeSolicitante"].ToString());

        }
    }
}
