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

	public class _20130902201813_test : Migration
	{
		protected string table_name = "dbo.test_table";

		protected override void _Up()
		{
            sb.Table(table_name)
            .IdColumn("id", c.Int, 1, 1)
            .Column("name", c.String, n.Null, 50)
            .End();
                   
		}


		protected override void _Down()
		{
            sb.DropTable(table_name);
		}
	}
}
