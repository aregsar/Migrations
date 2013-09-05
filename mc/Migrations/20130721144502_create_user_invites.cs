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

	public class _20130721144502_create_user_invites : Migration
	{
		protected string table_name = "dbo.table_name";

		protected override void _Up()
		{
            
//                     this.sb.Table(table_name)

//                         - invited_by_id
//- invitor_by_name
//- invited_at
//- invite_email
//- signup_token
//- signup_token_expires_at
//- signedup_user_id





//                 .Column("created_by_server", c.String, n.NotNull, 50)
//                 .Column("processed_by_server", c.String, n.NotNull, 50)
//                 .Column("status", c.Int, n.NotNull)//0=pending,1=processing,2=processed,3=failed
//                 .Column("status_updated_at", c.DateTime, n.NotNull)
//                 .Column("failed_attempt_count", c.Int, n.NotNull)
//                 .Column("template_name", c.String, n.NotNull, 50)
               
//                 .Column("mailer", c.String, n.NotNull, int.MaxValue)
//                 .End();

//            this.sb.AlterTable(table_name)
//               .AddPK("uid")
//               .AddIndex("processed_by")
//               .AddIndex("created_at")
//               .AddIndex("priority")
//               .AddIndex("queue_name")
//               .AddIndex("created_by")
//               .AddDefault("priority", d.Zero)
//               .AddDefault("failed_attempt_count", d.Zero)
//               .AddDefault("created_at", d.GetUtcDate2)
//               .AddDefault("status_updated_at", d.GetUtcDate2)
//               .AddDefault("processed_by", d.EmptyString)
//               .AddDefault("status", d.One);

		}


		protected override void _Down()
		{
            this.sb.DropTable(table_name);
		}
	}
}
