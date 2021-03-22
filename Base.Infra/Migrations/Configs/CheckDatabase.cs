using Dapper;
using Base.Infra.Context;
using System;
using Base.Infra.Migrations.Enum;

namespace Base.Infra.Migrations.Configs
{
    public class CheckDatabase
    {
        public static void DatabaseExist(string connectionString, EBanco banco)
        {

            var _dataBaseName = ExtractDataBaseName(connectionString, banco);
            if (string.IsNullOrWhiteSpace(_dataBaseName))
            {
                throw new Exception("Não foi possivel identificar o banco de dados;");
            }

            // Remove o nome do banco de dados porque se o banco não existir não conseguirá conectar para criar.
            var _connectionSemBD = ConnectionRemoveDatabase(connectionString, banco);

            // Cria o Banco caso não exista
            switch (banco)
            {
                case EBanco.SQLSERVER:
                        CheckOrCreateDatabaseSqlServer(_connectionSemBD, _dataBaseName);
                    break;
                case EBanco.POSTGRESSQL:
                    CheckOrCreateDatabasePostgreSql(_connectionSemBD, _dataBaseName);
                    break;               
            }
            
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

        private static string ExtractDataBaseName(string connectionString, EBanco banco)
        {
            string databaseName = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra1 = connectionString.Split(';');
                foreach (var part in quebra1)
                {
                    if(EBanco.SQLSERVER == banco)
                    {
                        if (part.ToUpper().Contains("CATALOG"))
                        {
                            var part2 = part.Split("=");
                            databaseName = part2[1];
                        }
                    }else if (EBanco.POSTGRESSQL == banco)
                    {
                        if (part.ToUpper().Contains("DATABASE"))
                        {
                            var part2 = part.Split("=");
                            databaseName = part2[1];
                        }
                    }                    
                }
            }
            return databaseName;
        }

        private static string ConnectionRemoveDatabase(string connectionString, EBanco banco)
        {
            string connection = "";

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var quebra1 = connectionString.Split(';');
                foreach (var part in quebra1)
                {                    
                    if (!string.IsNullOrWhiteSpace(part))
                    {                       
                        if (EBanco.SQLSERVER == banco)
                        {
                            if (!part.ToUpper().Contains("CATALOG"))
                            {
                                connection += part + ";";
                            }
                        }
                        else if (EBanco.POSTGRESSQL == banco)
                        {
                            if (!part.ToUpper().Contains("DATABASE"))
                            {
                                connection += part + ";";
                            }
                        }
                    }

                }
            }
            return connection;
        }
    }
}
