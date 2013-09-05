namespace Migrations
{
	/* Templates

	sb.Table("tablename")
	.IdColumn("idcolumnname", c.Int, 1, 1)
	.Column("columnname",c.Int,n.NotNull)
	.Column("columnnamw2",c.String,n.Null,50)
	.Column("columnname3",c.String,n.Null,int.MaxValue).End()
	.AddPK("idcolumnname")
	.AddUniqueIndex("columnname")
	.AddIndex("columnname");

	sb.AlterTable("tablename")
	.AddFK("columnname","FTable.FColumn")
	.AddIndex("columnname")
	.AddUniqueIndex("columnname")
	.AddPK("idcolumnname");

	sb.DropTable("tablename");

	*/

	public class _20130706131215_create_users : Migration
	{
		protected string table_name = "dbo.users";

		protected override void _Up()
		{
            sb.Table(table_name)
               .IdColumn("id", c.Int, 1, 1)
               .Column("uid",                           c.Guid,         n.NotNull)//
               .Column("created_at",                    c.DateTime2,    n.NotNull)//
               .Column("activated",                     c.Bool,         n.NotNull)//
               .Column("activated_at",                  c.DateTime2,    n.NotNull)//
               .Column("last_accepted_terms_at",        c.DateTime2,    n.NotNull)//
               .Column("last_signin_at",                c.DateTime2,    n.NotNull)//
               .Column("last_activity_at",              c.DateTime2,    n.NotNull)//
               .Column("session_token",                 c.Guid,         n.NotNull)//
               .Column("session_token_expires_at",      c.DateTime2,    n.NotNull)//
               .Column("suspended",                     c.Bool,         n.NotNull)//
               .Column("suspended_at",                  c.DateTime2,    n.NotNull)//
               .Column("suspended_reason",              c.Int,          n.NotNull)//
               .Column("verify_email_token",            c.Guid,         n.NotNull)
               .Column("verify_email_token_expires_at", c.DateTime2,    n.NotNull)//
               .Column("set_password_token",            c.Guid,         n.NotNull)//
               .Column("set_password_token_expires_at", c.DateTime2,    n.NotNull)//
               .Column("set_email_token",               c.Guid,         n.NotNull)//
               .Column("set_email_token_expires_at",    c.DateTime2,    n.NotNull)//
               .Column("failed_signin_count",           c.Int,          n.NotNull)//
               .Column("last_failed_signin_at",         c.DateTime2,    n.NotNull)//
               .Column("signin_lock_expires_at",        c.DateTime2,    n.NotNull)//
               .Column("remaining_invites",             c.Int,          n.NotNull)//
               .Column("role",                          c.Int,          n.NotNull)//
               .Column("email",                         c.String,       n.NotNull, 50)
               .Column("name",                          c.String,       n.NotNull, 50)
               .Column("password",                      c.String,       n.NotNull, 1024)
               .End();

            sb.AlterTable(table_name)
                .AddPK("id")
                .AddUniqueIndex("email")
                .AddUniqueIndex("session_token")
                .AddUniqueIndex("set_password_token")
                .AddUniqueIndex("set_email_token")
                      .AddIndex("name")
                      .AddIndex("created_at")
                    .AddDefault("uid", d.NewGuid)
                    .AddDefault("session_token", d.NewGuid)
                    .AddDefault("set_email_token", d.NewGuid)
                    .AddDefault("set_password_token", d.NewGuid)
                    .AddDefault("activated", d.Zero)
                    .AddDefault("suspended", d.Zero)
                    .AddDefault("suspended_reason", d.Zero)
                    .AddDefault("failed_signin_count", d.Zero)
                    .AddDefault("role", d.Zero)
                    .AddDefault("created_at",d.GetUtcDate2)
                    .AddDefault("last_signin_at",d.GetUtcDate2)
                    .AddDefault("last_activity_at",d.GetUtcDate2)
                    .AddDefault("session_token_expires_at",d.GetUtcDate2)
                    .AddDefault("activated_at",d.GetUtcDate2)
                    .AddDefault("suspended_at",d.GetUtcDate2)
                    .AddDefault("last_accepted_terms_at",d.GetUtcDate2)
                    .AddDefault("verify_email_token_expires_at", d.GetUtcDate2)
                    .AddDefault("set_password_token_expires_at",d.GetUtcDate2)
                    .AddDefault("set_email_token_expires_at",d.GetUtcDate2)
                    .AddDefault("last_failed_signin_at",d.GetUtcDate2)
                    .AddDefault("signin_lock_expires_at",d.GetUtcDate2);
		}


		protected override void _Down()
		{
            sb.DropTable(table_name);
		}
	}
}
