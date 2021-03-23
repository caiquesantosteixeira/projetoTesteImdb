using Npgsql;
using System.Data.Common;

namespace Base.Repository.Context
{
    public class ADOPostgresSQLContext
    {
        private NpgsqlConnection conn;
        private readonly string _conexao;
        public ADOPostgresSQLContext(string connectionString)
        {
            _conexao = connectionString;
        }

        public DbConnection GetConection()
        {

            conn = new NpgsqlConnection(_conexao);
            conn.Open();
            return conn;
        }

        //Executa INSERTs, UPDATEs,DELETEs
        public void ExecutarComandoSQL(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void FecharConexao()
        {
            conn.Close();
        }
    }
}
