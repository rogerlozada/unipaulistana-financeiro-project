namespace unipaulistana.data
{
    using System;
    using System.Data.SqlClient;
    using Microsoft.Extensions.Options;
    using unipaulistana.model;
    using System.Threading.Tasks;

    public class ConexaoContext
    {
        public ConexaoContext(IOptions<AppConnectionSettings> appSettings)
        {
            this.urlConexao = appSettings.Value.DefaultConnection;
        }

        public SqlConnection ObterConexao()
        {
            if (conexao != null)
                return conexao;

            conexao = new SqlConnection(this.urlConexao);
            conexao.Open();
            return conexao;
        }

        string urlConexao;
        public static SqlConnection conexao;
    }
}
