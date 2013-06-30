
Installation Instructions:
1-Set the path of the Migration Console2 application to the Consolue2 directory location
2-Add a migrations folder to a VS project (or use your own name)
3-Add a Sql folder to the VS project (or use your own name)
4-Add a Libs folder to the VS project (or use your own name)
5-Drop the Migrations.dll file into the Libs directory
5-Add a reference to the Migrations.dll file in the project that contains the migrations folder
6-In the mc.exe.config file  in the Console2 directory as needed: 
set the migrationLibraryAssemblyPath key value to the path for of the Migrations.dll file in the Libs folder
set the migrationClassPath key value to the path for of the Migrations folder (note: if you use a different name for the folder, reflect that here)
set the migrationScriptPath key value to the path for of the Sql folder (note: if you use a different name for the folder, reflect that here)
Set the databaseName key value to the database that will be the target of the migrations. This could be a preexisting database or a new one that will be created via the console.
Set the connectionString key value to a connection string for the database server. Be sure to Leave the Database name specified in the connection string as "Master".
set the providerName to the appropriate .net sql server provider name for your database server.
set the serverType to the database server type that you will be using. Current supported types are SqlServe2012 and SqlServer2008

exampleof mc.exe.config key/values are listed below:
<add key="serverType" value="SqlServer2012"/><!-- valid ServerType values are: SqlServer2008 SqlServer2011 MySql6 PostGres90 SqlLite -->
<add key="providerName" value="System.Data.SqlClient"/><!-- valid ServerType values are: System.Data.SqlClient -->
<add key="connectionString" value= "Server=PC-AREGS7\LOCAL;Database=Master;Trusted_Connection=True;"/>
<add key="databaseName" value="Autos"/>
<add key="migrationClassPath" value="C:\MvcProjects\Autos\Autos\Migrations"/>
<add key="migrationScriptPath" value="C:\MvcProjects\Autos\Autos\Sql"/>
<add key="migrationLibraryAssemblyPath" value="C:\MvcProjects\Autos\Autos\Libs\Migrations.dll"/>

7*-to use an alternate config in the config subfolder, rename from database.mc.exe.config to mc.exe.config and drop in the parent directory ovewriting the existig one)



Rules for using the migration SchemaBuilder API 

SchemaBuilder uses method chaing to implement a conside schema building api.
SchemaBuilder is database agnostic and will generate the appropriate schema for the database type that it is configured for.
Schema builder also uses ColumnType (CT)  and NullabilityType (NT) and DefaultValue (DV) helper components to specify the type and nullabilty of table columns
ColumnType component column types are specified as native dotnet types that will get mapped to the appropriate matching database column types for the configured database type

1- New Tables.

To create a new table use the Table("tablename") method. You must complete the new table script by using the End() method.
The End() method can only be used with the Table().
the only methods that can be called between Table() amd End() are Column() and IdColumn()
The IdColumn() generates an Identity column and can only be used once between the Table() and End() methods
The Column() method generates a column. Multiple Column() methods can be used between the Table() and End() methods
There must be at least one IdColumn() or Column() method between the Table() and End() methods 
The only place that the IdColumn() and Column() methods can be used is between the Table() and End() methods 

 
Here is an example of creating a new table with an integer Identity column 

sb.Table("dbo.abcd")
.IdColumn("xg",CT.Int,1,1)
.Column("abcd",CT.int_,NT.notnull_)
.Column("abcd",CT.string_,NT.null_,50)
.Column("abcd",CT.string_,NT.null_,int.max).End();

After the End() method any of the other AddXXX(), AlterColumn() and RenameColumn() methods can be used to alter the new table.
Here is an example of creating a new table with additional methodes that alter the table to add keys and Indecies

sb.Table("abcd")
.IdColumn("xg",CT.Guid)
.Column("abcd",CT.INT,NT.NotNil)
.Column("abcd",CT.String,NT.Nil,50)
.Column("abcd",CT.String,NT.Nil,int.max).End()
.AddPK("xyz")
.AddFK("xyz","dbo.FTable.FCol")
.AddIndex("xyz desc")
.AddIndex("xyz, abc");


To Drop a table use the sb.DropTable("abcd") method.

2 - Tables

To alter an existing table use the AlterTable("tablename") method. All  methods except the Column(), IdColumn() methods can be used 
with the AlterTable() method. AlterTable() can be used multiple times for the same table.


sb.Table("dbo.abcd")
.IdColumn(	"xg",		CT.Guid										)
.Column(	"abcd",		CT.INT,			NT.NotNil					)
.Column(	"abcd",		CT.String,		NT.Nil,		50				)
.Column(	"abcd",		CT.String,		NT.Nil,		int.max			).End();

AlterTable("dbo.abcd")
.AddPK(		"xyz")
.AddFK(		"xyz",		"dbo.FTable.FCol"	)
.AddIndex(	"xyz desc"						)
.AddIndex(	"xyz, abc"						);

When creating a new table you can alter it without using the AlterTable() method

For example the following  equialent to the above seperate AlterTable() call

sb.Table("dbo.abcd")

.IdColumn("xg",CT.Guid)
.Column("abcd",CT.INT,NT.NotNil)
.Column("abcd",CT.String,NT.Nil,50)
.Column("abcd",CT.String,NT.Nil,int.max).End()
.AddPK("xyz")
.AddFK("xyz","dbo.FTable.FCol")
.AddIndex("xyz desc")
.AddIndex("xyz, abc");

For existing tables the only way to alter them is with the AlterTable() method

Here is another example equivalent to above that uses two seperate AlterTable commands

sb.Table("dbo.abcd")
.IdColumn("xg",CT.Guid)
.Column("abcd",CT.INT,NT.NotNil)
.Column("abcd",CT.String,NT.Nil,50)
.Column("abcd",CT.String,NT.Nil,int.max)
.End();


AlterTable("dbo.abcd")
.AddPK("xyz")
.AddFK("xyz","dbo.FTable.FCol");

AlterTable("dbo.abcd")
.AddIndex("xyz desc")
.AddIndex("xyz, abc");


All AddXXX() methods have equivalent DropXXX() methods

Here is an example that reverses the above AlterTable() scripts

AlterTable("dbo.abcd")
.DropPK("xyz")
.DropFK("xyz","dbo.FTable.FCol")
.DropIndex("xyz desc")
.DropIndex("xyz, abc");


Here is a complete migration example that demonstartes the use of migration methods in the context of a real migration
The example shows two migrations files that will run in sequence. 
The first migration creates a new table and alters it to add a primary key. 
The second migration alters the table that was created in the first migration addin a foreign key and two indecees.
The migration files also include all drop methods to properly rollback the changes withing each corresponding migration file.

This is illustrate below

class _000000000_Add_Table_ABCD : Migration
{


	void Up()
	{
			sb.Table("abcd")
			.IdColumn("xg",CT.Guid)
			.Column("abcd",CT.INT,NT.NotNil)
			.Column("abcd",CT.String,NT.Nil,50)
			.Column("abcd",CT.String,NT.Nil,int.max).End()
			.AddPK("xyz");
	}

	void Down()
	{
			sb.DropTable("absd");
	}

}


class _000000000_Alter_Table_ABCD : Migration
{


	void Up()
	{
			sb.AlterTable("abcd")
			.AddFK("xyz","FTable.FCol")
			.AddIndex("xyz desc")
			.AddIndex("xyz, abc");
	}

	void Down()
	{
			sb.AlterTable("abcd")
			.DropFK("xyz","FTable.FCol")
			.DropIndex("xyz desc")
			.DropIndex("xyz, abc");
	}

}



 
3- Special methods

These two methods are handy when you want to alter a table to add a new non nullable column or alter an existing nullable column to nullable
This can be done by first adding a nullable column and using these methods to set a default value before changing the column to non nullable
The methods can also be use when you need to 
SetColumnValue(string columnName,string columnValue);
SetColumnValueIfNull(string columnName,string columnValue);
sb.SetColumnValueIfNull("xyz","0");
sb.SetColumnValueIfNull("xyz",DV.GetDate);

These two methods can be used to generate ad hoc script in a migration
void Script(string script);
void ScriptLine(string script);

Below is the full SchemaBuilder API:

         
void Script(string script);

description:
Ad-hoc script

Example:
Script("update tbla set  abcd = 0")

   

void ScriptLine(string script);

description:
Ad-hoc script ending with a newline
    
Example:
ScriptLine("update tbla set  abcd = 0");

        
         
ISchemaBuilder  Table(string tablename)

description:
Create a new table builder

Example:
Table("dbo.abcd")

      
         
ISchemaBuilder  IdColumn(string columnName,IColumnType columnType,int seed,int increment)

description:
Create an ID column for a new table

Examples:
IdColumn("Id",CT.Int,1,1)
IdColumn("Id",CT.Long,0,1)
IdColumn("Id",CT.Guid)

         //IdColumn("Id",CT.Guid);
         ISchemaBuilder  IdColumn(string columnName, IColumnType columnType);

         //Column("abc",    CT.Int,     Nt.Null);    
         //Column("abc",    CT.StringFixedSize,  Nt.NotNull);
         ISchemaBuilder  Column(string columnName,IColumnType columnType,INullabilityType nullabilityType);

         //Column("abc",    CT.String,  Nt.NotNull, 50);
         //Column("abc",    CT.String,  Nt.NotNull,  int.MaxValue);
         ISchemaBuilder  Column(string columnName,IColumnType columnType,INullabilityType nullabilityType,int size);
         ISchemaBuilder  End();

         //AlterTable("dbo.abcd");
         ISchemaBuilder  AlterTable(string tablename);

         //AddPK("abcd");
         //AddPK("abcd desc",clustered: false);
         ISchemaBuilder  AddPK(string columnName,bool clustered = true);
         ISchemaBuilder  AddFK(string columnName,string FTableDotFColumn,bool cascade_delete = false, bool cascade_update = false);
         ISchemaBuilder  AddIndex(string indexColumnsAndDir,bool clustered = false);
         ISchemaBuilder  AddUniqueIndex(string indexColumnsAndDir,bool clustered = false);
         ISchemaBuilder  AddDefault(string columnName,string defaultValue);
         ISchemaBuilder  AddColumn(string columnName,IColumnType columnType,INullabilityType nullabilityType);
         ISchemaBuilder  AddColumn(string columnName,IColumnType columnType,INullabilityType nullabilityType,int size);
         ISchemaBuilder  AlterColumn(string columnName,IColumnType columnType,INullabilityType nullabilityType);
         ISchemaBuilder  AlterColumn(string columnName,IColumnType columnType,INullabilityType nullabilityType,int size);
         ISchemaBuilder  DropColumn(string columnName);
         ISchemaBuilder  DropIndex(string indexcolumnName);
         ISchemaBuilder  DropDefault(string defaultcolumnName);
         ISchemaBuilder  DropPK(string pkcolumnName);
         ISchemaBuilder  DropFK(string fkcolumnName, string FTableDotFColumn);
         ISchemaBuilder  RenameColumn(string columnName,string newcolumnName);

         //DropTable("dbo.abcd");
         void DropTable(string tableName);

         //TruncateTable("dbo.abcd");
         void TruncateTable(string tableName);

         //SetColumnValue("xyz","0");      
         ISchemaBuilder SetColumnValue(string columnName,string columnValue);

         //SetColumnValueIfNull("xyz","getDate()");
         ISchemaBuilder SetColumnValueIfNull(string columnName,string columnValue);
















<! Note:valid ServerType are SqlServer2008 SqlServer2011 MySql6 PostGres90 SqlLite !>
<key name = ServerType value="SqlServer2011"
<key name = MigrationClassFilesPath value = "c:\\aaa\\Migrations">
<key name = MigrationScriptFilesPath value = "c:\\aaa\\Migration\Scripts">
<key name = DatabaseName value = "Cars">
<! Note:connectionstring name should be set to value of DatabaseName !>
<connectionstring name = "cars" value = "connectionstring">



create
script
migrate
version
history
help

create database (return database name)
create Schema (return table name)
create migration add_car (return migration file name)
script all
script up 20120104221403 (returns script)
script down 20120104221403 (returns script)
run up 20120104221403 (returns script)
run down 20120104221403 (returns script)
migrate up (from current version)
migrate down  (from current version)
migrate to 20120104221403  (from current version)
migrate up all  (from current version)
migrate down all  (from current version)
version
history
help


create database
--Uses DatabaseName configuration value to create the database

create Schema 
--Uses DatabaseName configuration value to create <databasename>SchemaVersion and <databasename>SchemaMigrationHistory tables

create migration <add_car>
--uses MigrationClassFilesPath to create a new timestamped migration file in the format 20120104221403_add_car.cs

script all
--uses MigrationScriptFilesPath to script out all up and down migrations to files in the format  20120104221403_up_add_car.sql and 20120104221403_down_add_car.sql

script up 20120104221403 (returns script)
--uses MigrationScriptFilesPath to script out in the format  20120104221403_up_add_car.sql

script down 20120104221403 (returns script)
--uses MigrationScriptFilesPath to script out in the format  20120104221403_down_add_car.sql

run up 20120104221403 (returns script)
--uses MigrationClassFilesPath to find, load and run migration  class method _20120104221403_add_car.Up()

run down 20120104221403 (returns script)
--uses MigrationClassFilesPath to load and run migration class method _20120104221403_add_car.Down()

migrate up (from current version)

migrate down  (from current version)
migrate to 20120104221403  (from current version)
migrate up all  (from current version)
migrate down all  (from current version)
Lock 20120104221403
Unlock 20120104221403
version
history
help

