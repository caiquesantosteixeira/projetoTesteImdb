using System.Data.Common;
using System.Data.SqlClient;

namespace Base.Repository.Context
{
    public class ADOSqlServerContext
    {        
        private SqlConnection conn;
        private readonly string _conexao;     

        public ADOSqlServerContext(string connectionString)
        {
            _conexao = connectionString;
        }

        public DbConnection GetConection()
        {           

            conn = new SqlConnection(_conexao);
            conn.Open();
            return conn;
        }

        //Executa INSERTs, UPDATEs,DELETEs
        public void ExecutarComandoSQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void FecharConexao()
        {
            conn.Close();
        }
    }
}
