using Dapper;
using Base.Repository.Context;
using System;

namespace Base.Repository.Migrations.Configs
{
    public class CheckDatabase
    {
        public static void DatabaseExist(string connectionString)
        {

            var _dataBaseName = ExtractDataBaseName(connectionString);
            if (string.IsNullOrWhiteSpace(_dataBaseName))
            {
                throw new Exception("Não foi possivel identificar o banco de dados;");
            }

            // Remove o nome do banco de dados porque se o banco não existir não conseguirá conectar para criar.
            var _connectionSemBD = ConnectionRemoveDatabase(connectionString);


           CheckOrCreateDatabaseSqlServer(_connectionSemBD, _dataBaseName);
              
            
        }      

        private static void CheckOrCreateDatabaseSqlServer(string connectionSemBD, string dataBase)
        {
            try
            {
                string sqlCheck = $"if not exists(select * from sys.databases where name='{dataBase}') BEGIN " +
                                   $"exec('Create database {dataBase}') END";

                var dapp = new ADOSqlServerContext(connectionSemBD);
                using (var conn = dapp.GetConection())
                {
                    conn.ExecuteScalar(sqlCheck);
                    conn.Dispose();
                }
                dapp.FecharConexao();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possivel identificar o banco de dados;");
            }
        }

        private static void CheckOrCreateDatabasePostgreSql(string connectionSemBD, string dataBase)
        {
            try
            {
                
                string sqlCheck = $"SELECT * FROM pg_catalog.pg_database WHERE datname=('{dataBase}')";

                var dapp = new ADOPostgresSQLContext(connectionSemBD);
                using (var conn = dapp.GetConection())
                {
                    var retorno = conn.Query<Object>(sqlCheck).AsList();
                    if(retorno.Count <= 0)
                    {
                        string query = $"CREATE DATABASE \"{dataBase}\" " +
                            $" WITH" +
                            $" OWNER = postgres" +
                            $" ENCODING = 'UTF8' " +
                            $" LC_COLLATE = 'Portuguese_Brazil.1252'" +
                            $" LC_CTYPE = 'Portuguese_Brazil.1252' " +
                            $" TABLESPACE = pg_default" +
                            $" CONNECTION LIMIT = -1; ";

                        conn.Execute(query);
                    }
                    conn.Dispose();
                }
                dapp.FecharConexao();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel identificar o banco de dados;");
            }
        }

        private static string ExtractDataBaseName(string connectionString)
        {
            string databaseName = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra1 = connectionString.Split(';');
                foreach (var part in quebra1)
                {
                    if (part.ToUpper().Contains("CATALOG"))
                    {
                        var part2 = part.Split("=");
                        databaseName = part2[1];
                    }                 
                }
            }
            return databaseName;
        }

        private static string ConnectionRemoveDatabase(string connectionString)
        {
            string connection = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra1 = connectionString.Split(';');
                foreach (var part in quebra1)
                {                    
                    if (!string.IsNullOrWhiteSpace(part))
                    {   
                        if (!part.ToUpper().Contains("CATALOG"))
                        {
                            connection += part + ";";
                        }
                    }

                }
            }
            return connection;
        }
    }
}
