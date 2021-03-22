namespace FluentMigrator.PostegreSql.Migrations
{
    [FluentMigrator.Migration(00002)]
    public class PG_00002_UpdateColAdministradorTableUsuarios:FluentMigrator.Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql("update usuarios set administrador=true where id='666ef5e2-b9b3-4691-8449-52c6444bb6b2';");
        }
    }
}
