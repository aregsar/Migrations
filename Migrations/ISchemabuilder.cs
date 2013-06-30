using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations
{
    /* Templates
    
  sb.Table("tablename")
  .IdColumn("idcolumnname", c.Int, 1, 1)
  .Column("columnname",c.Int,n.NotNull)
  .Column("columnnamw2",c.String,n.Null,50)
  .Column("columnname3",c.String,n.Null,int.MaxValue).End()
  .AddPK("idcolumnname")
  .AddIndex("columnname");

  sb.AlterTable("tablename")
  .AddPK("idcolumnname")
  .AddIndex("columnname");

  sb.DropTable("tablename");
    
  */

    public interface ISchemaBuilder
    {
        IColumnType c { get;  }
        INullabilityType n { get;  }
        IDefaultValue d { get;  }

         string GetScript();
        
         void Clear();

         void BeginTransaction(); 
   
         void CommitTransaction();

         //Script("update tbla set  abcd = 0");
         void Script(string script);

         //ScriptLine("update tbla set  abcd = 0");
         void ScriptLine(string script);

         //Table("dbo.abcd")
         ISchemaBuilder  Table(string tablename);

         //IdColumn("Id",CT.Int,1,1);
         //IdColumn("Id",CT.Long,1,1);
         ISchemaBuilder IdColumn(string columnName, string columnType, int seed, int increment);

         //IdColumn("Id",CT.Guid);
         ISchemaBuilder IdColumn(string columnName, string columnType);

         //Column("abc",    CT.Int,     Nt.Null);    
         //Column("abc",    CT.StringFixedSize,  Nt.NotNull);
         ISchemaBuilder Column(string columnName, string columnType, string nullabilityType);

         //Column("abc",    CT.String,  Nt.NotNull, 50);
         //Column("abc",    CT.String,  Nt.NotNull,  int.MaxValue);
         ISchemaBuilder Column(string columnName, string columnType, string nullabilityType, int size);
         ISchemaBuilder  End();

         //AlterTable("dbo.abcd");
         ISchemaBuilder AlterTable(string tablename);

         //AddPK("abcd");
         //AddPK("abcd desc",clustered: false);
         ISchemaBuilder  AddPK(string columnName,bool clustered = true);
         ISchemaBuilder  AddFK(string columnName,string FTableDotFColumn,bool cascade_delete = false, bool cascade_update = false);
         ISchemaBuilder  AddIndex(string indexColumnsAndDir,bool clustered = false);
         ISchemaBuilder  AddUniqueIndex(string indexColumnsAndDir,bool clustered = false);
         ISchemaBuilder  AddDefault(string columnName,string defaultValue);
         ISchemaBuilder  AddColumn(string columnName, string columnType, string nullabilityType);
         ISchemaBuilder  AddColumn(string columnName, string columnType, string nullabilityType, int size);
         ISchemaBuilder  AlterColumn(string columnName, string columnType, string nullabilityType);
         ISchemaBuilder  AlterColumn(string columnName, string columnType, string nullabilityType, int size);
         ISchemaBuilder  DropColumn(string columnName);
         ISchemaBuilder  DropIndex(string indexcolumnName);
         //ISchemaBuilder  DropUniqueIndex(string indexcolumnName);
         ISchemaBuilder  DropDefault(string defaultcolumnName);
         ISchemaBuilder  DropPK(string pkcolumnName);
         ISchemaBuilder  DropFK(string fkcolumnName, string FTableDotFColumn);
         ISchemaBuilder  RenameColumn(string columnName,string newcolumnName);

         //DropTable("dbo.abcd");
         void DropTable(string tableName);

         //TruncateTable("dbo.abcd");
         void TruncateTable(string tableName);

         //SetColumnValue("xyz","0");      
         void SetColumnValue(string columnName,string columnValue);

         //SetColumnValueIfNull("xyz","getDate()");
         void SetColumnValueIfNull(string columnName,string columnValue);



    }

  
}
