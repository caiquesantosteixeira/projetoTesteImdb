namespace FluentMigrator.SqlServer.Migrations
{
    [FluentMigrator.Migration(00002)]
    public class SqlServer_00002_UpdateColAdministradorTableUsuarios: FluentMigrator.Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql("update usuarios set administrador=1 where id='666ef5e2-b9b3-4691-8449-52c6444bb6b2';");
        }
    }
}
