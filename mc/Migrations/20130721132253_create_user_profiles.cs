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

	public class _20130721132253_create_user_profiles : Migration
	{
        protected string table_name = "dbo.user_profiles";

        protected override void _Up()
        {
            this.sb.Table(this.table_name)
             .IdColumn("id", c.Int, 1, 1)
             .Column("user_id", c.Int, n.NotNull)
             .Column("user_uid", c.Guid, n.NotNull)
             .Column("created_at", c.DateTime, n.NotNull)
             .Column("removed", c.Bool, n.NotNull)
             .Column("business_name", c.String, n.NotNull, 140)
             .Column("job_title", c.String, n.NotNull, 140)
             .Column("business_url", c.String, n.NotNull, 140)
             .Column("business_zipcode", c.String, n.NotNull, 9)
             .Column("business_city", c.String, n.NotNull, 50)
             .Column("business_state", c.String, n.NotNull, 2)
             .Column("business_email", c.String, n.NotNull, 100)
             .Column("business_phone", c.String, n.NotNull, 10)
             .Column("tags", c.String, n.NotNull, 140)
             .End();


            this.sb.AlterTable(this.table_name)
            .AddPK("id")
            .AddUniqueIndex("user_id")
            .AddUniqueIndex("user_uid")
            .AddUniqueIndex("business_url")
            .AddIndex("business_zipcode")
            .AddIndex("business_state")
            .AddDefault("created_at", d.GetUtcDate2)
            .AddDefault("removed", d.Zero)
            .AddDefault("business_name", d.EmptyString)
            .AddDefault("job_title", d.EmptyString)
            .AddDefault("business_url", d.EmptyString)
            .AddDefault("business_zipcode", d.EmptyString)
            .AddDefault("business_city", d.EmptyString)
            .AddDefault("business_state", d.EmptyString)
            .AddDefault("business_email", d.EmptyString)
            .AddDefault("business_phone", d.EmptyString)
            .AddDefault("tags", d.EmptyString);


        }


        protected override void _Down()
        {
            this.sb.DropTable(this.table_name);
        }
	}
}
