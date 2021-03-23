namespace FluentMigrator.SqlServer.Migrations
{
    [FluentMigrator.Migration(00001)]
    public class SqlServer_00001_Database: FluentMigrator.Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql("");
        }
    }
}
