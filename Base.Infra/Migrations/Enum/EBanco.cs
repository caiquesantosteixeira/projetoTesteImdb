using System.ComponentModel;

namespace Base.Repository.Migrations.Enum
{
    public enum EBanco
    {
        [Description("SqlServer")]
        SQLSERVER=0,
        [Description("PostgresSQL")]
        POSTGRESSQL =1
    }
}
