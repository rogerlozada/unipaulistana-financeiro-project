namespace unipaulistana.model
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using unipaulistana.data;

    public class UsuarioRepository : IUsuarioRepository
    {
        public UsuarioRepository(ConexaoContext conexao)
        {
            this.conexao = conexao;
        }

        readonly ConexaoContext conexao;

        public IEnumerable<Usuario> ObterTodos()
        {
            var cmd = new SqlCommand(@"select 
                                        a.usuarioID,
                                        a.Nome,
                                        a.Email,
                                        a.Senha,
                                        a.Foto,
                                        a.DepartamentoID,
                                        b.nome as nomeDepartamento,
                                        a.GrupoDeSegurancaID,
                                        c.nome as nomeGrupoDeSeguranca
                                    from usuario a 
                                    inner join departamento b on(a.departamentoid=b.departamentoid)
                                    inner join grupoDeSeguranca c on(a.grupoDeSegurancaID=c.grupoDeSegurancaID)", this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            while (sqlDataReader.Read())
                yield return new Usuario( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString(),
                                                       sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(),
                                                       sqlDataReader.GetValue(4).ToString(), Convert.ToInt32(sqlDataReader.GetValue(5).ToString()),
                                                       sqlDataReader.GetValue(6).ToString(), Convert.ToInt32(sqlDataReader.GetValue(7).ToString()),
                                                       sqlDataReader.GetValue(8).ToString());
        }

        public Usuario ObterPorID(int usuarioID)
        {
            string sql = string.Format(@"select 
                                        a.usuarioID,
                                        a.Nome,
                                        a.Email,
                                        a.Senha,
                                        a.Foto,
                                        a.DepartamentoID,
                                        b.nome as nomeDepartamento,
                                        a.GrupoDeSegurancaID,
                                        c.nome as nomeGrupoDeSeguranca
                                    from usuario a 
                                    inner join departamento b on(a.departamentoid=b.departamentoid)
                                    inner join grupoDeSeguranca c on(a.grupoDeSegurancaID=c.grupoDeSegurancaID)
                                         where a.usuarioID={0}", usuarioID);

            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var usuario = new Usuario();
            while (sqlDataReader.Read())
            {
                usuario = new Usuario( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString(),
                                                       sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(),
                                                       sqlDataReader.GetValue(4).ToString(), Convert.ToInt32(sqlDataReader.GetValue(5).ToString()),
                                                       sqlDataReader.GetValue(6).ToString(), Convert.ToInt32(sqlDataReader.GetValue(7).ToString()),
                                                       sqlDataReader.GetValue(8).ToString());
            }
            return usuario;
        }

        public Usuario Login(string email, string senha)
        {
            string sql = string.Format("select * from usuario where email='{0}' and senha='{1}'", email, senha);
            var cmd = new SqlCommand(sql, this.conexao.ObterConexao());
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            var usuario = new Usuario();
            while (sqlDataReader.Read())
            {
                usuario = new Usuario( Convert.ToInt32(sqlDataReader.GetValue(0)), sqlDataReader.GetValue(1).ToString(),
                                                       sqlDataReader.GetValue(2).ToString(), sqlDataReader.GetValue(3).ToString(),
                                                       sqlDataReader.GetValue(4).ToString());
            }
            return usuario;
        }

        public void Adicionar(Usuario usuario)
        {
            string query = @"insert into dbo.usuario (Nome, Email, Senha, Foto, DepartamentoID, GrupoDeSegurancaID) 
                                        values (@Nome, @Email, @Senha, @Foto, @DepartamentoID, @GrupoDeSegurancaID) ";
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 255).Value = usuario.Nome;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = usuario.Email;
            cmd.Parameters.Add("@Senha", SqlDbType.VarChar, 10).Value = usuario.Senha;
            cmd.Parameters.Add("@Foto", SqlDbType.VarChar, 10).Value = "user.png";
            cmd.Parameters.Add("@DepartamentoID", SqlDbType.Int).Value = usuario.DepartamentoID;
            cmd.Parameters.Add("@GrupoDeSegurancaID", SqlDbType.Int).Value = usuario.GrupoDeSegurancaID;
            cmd.ExecuteNonQuery();
        } 

        public void Atualizar(Usuario usuario)
        {
            string query = string.Format(@"update dbo.usuario 
                                                set Nome=@Nome, 
                                                    Email=@Email, 
                                                    DepartamentoID=@DepartamentoID, 
                                                    GrupoDeSegurancaID=@GrupoDeSegurancaID 
                                                where UsuarioID={0}", usuario.UsuarioID);
                                                
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = usuario.Nome;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = usuario.Email;
            cmd.Parameters.Add("@DepartamentoID", SqlDbType.Int).Value = usuario.DepartamentoID;
            cmd.Parameters.Add("@GrupoDeSegurancaID", SqlDbType.Int).Value = usuario.GrupoDeSegurancaID;
            cmd.ExecuteNonQuery();
        } 

        public void Excluir(int usuarioID)
        {
            string excluiSQL = string.Format(@"delete from usuario where UsuarioID={0}",usuarioID);
            var cmd = new SqlCommand();
            cmd.CommandText = excluiSQL ;
            cmd.Connection = this.conexao.ObterConexao();
            cmd.ExecuteNonQuery();
        } 

        public void AtualizarSenha(int usuarioID, string novaSenha)
        {
            string query = string.Format("update dbo.usuario set Senha=@Senha where UsuarioID={0}", usuarioID);
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Senha", SqlDbType.VarChar, 10).Value = novaSenha;
            cmd.ExecuteNonQuery();
        } 

        public void AtualizarFoto(int usuarioID, string nomeDaImagem)
        {
            string query = string.Format("update dbo.usuario set Foto=@Foto where UsuarioID={0}", usuarioID);
            var cmd = new SqlCommand(query, this.conexao.ObterConexao());
            cmd.Parameters.Add("@Foto", SqlDbType.VarChar, 50).Value = nomeDaImagem;
            cmd.ExecuteNonQuery();
        } 
    }
}
