using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Migrations.Schemas
{
    public class SqlSvr2012SchemaBuilder : ISchemaBuilder
    {

        public SqlSvr2012SchemaBuilder()
        {
            _c = new SqlSvr2012ColumnType();

            _n = new SqlSvr2012NullabilityType();

            _d = new SqlSvr2012DefaultValue();
        }

        public IColumnType _c;
        public INullabilityType _n;
        public IDefaultValue _d;


        public IColumnType c { get{return _c;}  }
        public INullabilityType n { get { return _n; } }
        public IDefaultValue d { get { return _d; } }

        public void Script(string script)
        {
            sb.Append(script);
        }

        public void ScriptLine(string script)
        {
            sb.AppendLine(script);
        }

        public void SetColumnValue(string columnName, string columnValue)
        {
            sb.AppendLine("update " + currentTable + " set " + columnName + " = " + columnValue);
        }

        public void SetColumnValueIfNull(string columnName, string columnValue)
        {
            sb.AppendLine("update " + currentTable + " set " + columnName + " = " + columnValue + " where " + columnName + " is null");
        }

        string currentTable;

        StringBuilder sb = new StringBuilder();

        public void Clear()
        {
            sb.Clear();
        }

        public string GetScript()
        {
            return sb.ToString();
        }

        public ISchemaBuilder Table(string tablename)
        {
            currentTable = tablename;
            sb.AppendLine("create table " + currentTable + "( ");
            return this;
        }

        public ISchemaBuilder End()
        {
            sb.Remove(sb.Length - 1, 1);
            sb.AppendLine(" ) ");
            return this;
        }

        public ISchemaBuilder AlterTable(string tablename)
        {
            currentTable = tablename;
            return this;
        }

        public void DropTable(string tableName)
        {
            sb.AppendLine("drop table " + tableName);
        }

        public void TruncateTable(string tableName)
        {
            sb.AppendLine("truncate table " + tableName);

        }



        public void BeginTransaction()
        {
            sb.AppendLine("begin transaction ");
        }

        public void CommitTransaction()
        {
            sb.AppendLine("commit transaction ");
        }


        public ISchemaBuilder IdColumn(string columnName, string columnType, int seed, int increment)
        {
            sb.AppendLine(columnName + " " + columnType + " identity(" + seed + "," + increment + ") not null");
            sb.Append(",");
            return this;
        }

        public ISchemaBuilder IdColumn(string columnName, string columnType)
        {
            if (columnType == c.Guid)
            {
                sb.AppendLine(columnName + " uniqueidentifier not null default(newid())");
            }
            else
            {
                sb.AppendLine(columnName + " " + columnType + " identity(1,1) not null");
            }
            sb.Append(",");

            return this;
        }
        public ISchemaBuilder Column(string columnName, string columnType, string nullabilityType)
        {
            sb.AppendLine(columnName + " " + columnType + " " + nullabilityType);
            sb.Append(",");

            return this;
        }
        public ISchemaBuilder Column(string columnName, string columnType, string nullabilityType, int size)
        {
            
            string sizestr = size.ToString();
            if (size == int.MaxValue) sizestr = "max";
            sb.AppendLine(columnName + " " + columnType + "(" + sizestr + ") " + nullabilityType);
            sb.Append(",");

            return this;
        }


        public ISchemaBuilder AlterColumn(string columnName, string columnType, string nullabilityType)
        {
            sb.AppendLine("alter table " + currentTable + " alter column " + columnName + " " + columnType + " " + nullabilityType);

            return this;
        }
        public ISchemaBuilder AlterColumn(string columnName, string columnType, string nullabilityType, int size)
        {
            string sizestr = size.ToString();
            if (size == int.MaxValue) sizestr = "max";
            sb.AppendLine("alter table " + currentTable + " alter column " + columnName + " " + columnType + "(" + sizestr + ") " + nullabilityType);

            return this;
        }

        public ISchemaBuilder RenameColumn(string columnName, string newcolumnName)
        {
            sb.AppendLine("exec sp_rename '" + currentTable + "." + columnName + "', '" + newcolumnName + "'");
            return this;
        }

        public ISchemaBuilder AddColumn(string columnName, string columnType, string nullabilityType)
        {
            sb.AppendLine("alter table " + currentTable + " add " + columnName + " " + columnType + " " + nullabilityType);

            return this;
        }
        public ISchemaBuilder AddColumn(string columnName, string columnType, string nullabilityType, int size)
        {
            string sizestr = size.ToString();
            if (size == int.MaxValue) sizestr = "max";
            sb.AppendLine("alter table " + currentTable + " add " + columnName + " " + columnType + "(" + sizestr + ") " + nullabilityType);

            return this;
        }

        public ISchemaBuilder AddPK(string columnName, bool clustered = true)
        {
            sb.AppendLine("alter table " + currentTable + " add constraint pk_" + currentTable.Replace("dbo.", "") + "_" + columnName + (clustered ? " primary key clustered( " : " primary key nonclustered( ") + columnName + " )");
            return this;
        }

     

        public ISchemaBuilder AddFK(string columnName, string FTableDotFColumn, bool cascade_delete = false, bool cascade_update = false)
        {
            string cascadeDeletetext = cascade_delete ? " cascade on delete" : "";

            //parts 1=dbo 2=table 3=column
            string[] FTableDotFColumnParts = FTableDotFColumn.Split('.');

            string constraintName = GetFKConstraintName(columnName,FTableDotFColumn);

            if (FTableDotFColumnParts.Length == 3)
            {
                sb.AppendLine("alter table " + currentTable + " add constraint " + constraintName
                    + " foreign key (" + columnName + ") references " + FTableDotFColumnParts[0] + "." + FTableDotFColumnParts[1]
                    + " (" + FTableDotFColumnParts[2] + ")");
            }
            else
            {
                sb.AppendLine("alter table " + currentTable + " add constraint " + constraintName
                   + " foreign key (" + columnName + ") references " + FTableDotFColumnParts[0]
                   + " (" + FTableDotFColumnParts[1] + ")");
            }

            return this;
        }

        private string GetFKConstraintName(string columnName,string FTableDotFColumn)
        {
            string constraintName = "";

            string[] FTableDotFColumnParts = FTableDotFColumn.Split('.');

            //parts 1=dbo 2=table 3=column or parts 1=table 2=column
            if (FTableDotFColumnParts.Length == 3)
            {
                constraintName = "fk_" + currentTable.Replace("dbo.", "") + "__" + columnName + "_FTABLE_" + FTableDotFColumnParts[1] + "__" + FTableDotFColumnParts[2];

            }
            else 
            {
                constraintName = "fk_" + currentTable.Replace("dbo.", "") + "__" + columnName + "_FTABLE_" + FTableDotFColumnParts[0] + "__" + FTableDotFColumnParts[1];
            }

            return constraintName;
        }


        private string GetKeyName(string prefix, string indexColumnsAndDir)
        {
            string[] columnsAndDir = indexColumnsAndDir.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string indexName = prefix + currentTable.Replace("dbo.", "");

            foreach (string indexColumnAndDir in columnsAndDir)
            {
                string[] columnNameparts = indexColumnAndDir.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                indexName = indexName + "_"  + columnNameparts[0];
            }

            return indexName;
        }

        public ISchemaBuilder AddIndex(string indexColumnsAndDir, bool clustered = false)
        {
            //translate "asc" and "desc" to native script for each supported database type
            //dir = TranslateForDbType(dir);
        
            indexColumnsAndDir = indexColumnsAndDir.Trim();

            string clusteredtext = clustered ? "clustered " : "";

            string indexName= GetKeyName( "ix_",indexColumnsAndDir);

            sb.AppendLine("create " + clusteredtext + "index " + indexName + " on " + currentTable + "(" + indexColumnsAndDir + ")");
         
            return this;
        }


        public ISchemaBuilder AddUniqueIndex(string indexColumnsAndDir, bool clustered = false)
        {
            indexColumnsAndDir = indexColumnsAndDir.Trim();

            string clusteredtext = clustered ? "clustered " : "";

            string indexName = GetKeyName("ix_", indexColumnsAndDir);

            sb.AppendLine("create unique " + clusteredtext + "index " + indexName + " on " + currentTable + "(" + indexColumnsAndDir + ")");

            return this;
        }

   

        public ISchemaBuilder AddDefault(string columnName, string defaultValue)
        {
            string constraintName = "df_" + currentTable.Replace("dbo.", "") + "_" + columnName;

            sb.AppendLine("alter table " + currentTable + " add constraint " + constraintName + " default (" + defaultValue + ") for " + columnName);

            return this;
        }

        public ISchemaBuilder DropColumn(string columnName)
        {
            sb.AppendLine("alter table " + currentTable + " drop column " + columnName);

            return this;
        }

        public ISchemaBuilder DropIndex(string indexcolumnName)
        {
            indexcolumnName = indexcolumnName.Trim();

            string indexname = GetKeyName("ix_", indexcolumnName);

            sb.AppendLine("drop index " + currentTable + "." + indexname);

            return this;
        }

        //public ISchemaBuilder DropUniqueIndex(string indexcolumnName)
        //{
        //    //string indexname = "ux_" + currentTable.Replace("dbo.", "") + "_" + indexcolumnName;
        //    string indexname = GetKeyName("ux_", indexcolumnName);

        //    sb.AppendLine("drop index " + currentTable + "." + indexname);
        //    return this;
        //}
       
        public ISchemaBuilder DropPK(string pkcolumnName)
        {
            string pkname = "pk_" + currentTable.Replace("dbo.", "") + "_" + pkcolumnName;

            DropConstraint(pkname);

            return this;
        }
        public ISchemaBuilder DropFK(string fkcolumnName, string FTableDotFColumn)
        {
            string fkname = "fk_" + currentTable.Replace("dbo.", "") + "__" + fkcolumnName + "_FTABLE_" + FTableDotFColumn.Replace("dbo.", "").Replace(".", "__");

            DropConstraint(fkname);
            return this;
        }
       

        public ISchemaBuilder DropDefault(string defaultcolumnName)
        {
            string constraintName = "df_" + currentTable.Replace("dbo.", "") + "_" + defaultcolumnName;

            DropConstraint(constraintName);

            return this;
        }
       

        private ISchemaBuilder DropConstraint(string constraintName)
        {
            sb.AppendLine("alter table " + currentTable + " drop constraint " + constraintName);
            return this;
        }

       


    }
}
