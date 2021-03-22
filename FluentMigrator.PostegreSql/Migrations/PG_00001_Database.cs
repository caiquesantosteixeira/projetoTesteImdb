namespace FluentMigrator.PostegreSql.Migrations
{
    [FluentMigrator.Migration(00002)]
    public class PG_00001_Database: FluentMigrator.Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.EmbeddedScript("CriacaoBancoPostGresql_Tabelas.sql");
        }
    }
}
