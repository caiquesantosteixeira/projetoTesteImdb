using System.ComponentModel;

namespace Base.Infra.Migrations.Enum
{
    public enum EBanco
    {
        [Description("SqlServer")]
        SQLSERVER=0,
        [Description("PostgresSQL")]
        POSTGRESSQL =1
    }
}
