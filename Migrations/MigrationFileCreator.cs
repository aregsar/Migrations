using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Migrations
{
    public class MigrationFileCreator
    {

        public static void CreateMigrationFile(string migrationClassDirectory,string migrationName)
        {
            DateTime now = DateTime.Now;
            string timestamp = "" + now.Year + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + now.Hour.ToString().PadLeft(2, '0') + now.Minute.ToString().PadLeft(2, '0') + now.Second.ToString().PadLeft(2, '0');

            string migrationFileName = timestamp + "_" + migrationName;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace Migrations");
            sb.AppendLine("{");
     
          
          
            //write commented out template
            sb.Append("\t"); sb.AppendLine("/* Templates");
            sb.AppendLine();
            sb.Append("\t"); sb.AppendLine(@"sb.Table(""tablename"")");

            sb.Append("\t"); sb.AppendLine(@".IdColumn(""idcolumnname"", c.Int, 1, 1)");
            sb.Append("\t"); sb.AppendLine(@".Column(""columnname"",c.Int,n.NotNull)");
            sb.Append("\t"); sb.AppendLine(@".Column(""columnnamw2"",c.String,n.Null,50)");
            sb.Append("\t"); sb.Append(@".Column(""columnname3"",c.String,n.Null,int.MaxValue)");sb.AppendLine(@".End()");
            sb.Append("\t"); sb.AppendLine(@".AddPK(""idcolumnname"")");
            sb.Append("\t"); sb.AppendLine(@".AddUniqueIndex(""columnname"")");
            sb.Append("\t"); sb.AppendLine(@".AddIndex(""columnname"");");
            sb.AppendLine();
            sb.Append("\t"); sb.AppendLine(@"sb.AlterTable(""tablename"")");
            sb.Append("\t"); sb.AppendLine(@".AddFK(""columnname"",""FTable.FColumn"")");
            sb.Append("\t"); sb.AppendLine(@".AddIndex(""columnname"")");
            sb.Append("\t"); sb.AppendLine(@".AddUniqueIndex(""columnname"")");
            sb.Append("\t"); sb.AppendLine(@".AddPK(""idcolumnname"");");
            sb.AppendLine();
            sb.Append("\t"); sb.AppendLine(@"sb.DropTable(""tablename"");");
            sb.AppendLine();
            sb.Append("\t"); sb.AppendLine("*/");

            sb.AppendLine();

            //write migration class
            sb.Append("\t");sb.AppendLine("public class _" + migrationFileName + " : Migration");
            sb.Append("\t"); sb.AppendLine("{");
            sb.Append("\t\t"); sb.AppendLine(@"protected string table_name = ""dbo.table_name"";");
            sb.AppendLine();
            sb.Append("\t\t");sb.AppendLine("protected override void _Up()");
            sb.Append("\t\t"); sb.AppendLine("{");
            sb.AppendLine();
            sb.Append("\t\t"); sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine();
            sb.Append("\t\t"); sb.AppendLine("protected override void _Down()");
            sb.Append("\t\t"); sb.AppendLine("{");
            sb.AppendLine();
            sb.Append("\t\t"); sb.AppendLine("}");
            sb.Append("\t"); sb.AppendLine("}");
            sb.AppendLine("}");


            migrationFileName = migrationFileName + ".cs";

            File.WriteAllText(Path.Combine(migrationClassDirectory, migrationFileName), sb.ToString());
        }
    }
}
