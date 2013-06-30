using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace Migrations.Schemas
{
    public class SqlSvr2012MigrationFactory : MigrationFactory
    {




        public override Migration GetMigration(string migrationFilePath, string migrationLibraryAssemblyPath)
        {

            Migration migration = null;

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add(migrationLibraryAssemblyPath);
            cp.IncludeDebugInformation = true;

            CompilerResults results = codeProvider.CompileAssemblyFromFile(cp, migrationFilePath);

            if (results.Errors.Count == 0)
            {
                string[] parts = migrationFilePath.Split('\\');

                string migrationClass = "Migrations._" + parts[parts.Length - 1].Replace(".cs", "");

                migration = (Migration)results.CompiledAssembly.CreateInstance(migrationClass);
            }
            else
            {
                throw new Exception("Error in CompileAssemblyFromFile");
            }

            migration.sb = new SqlSvr2012SchemaBuilder();

            migration.c = new SqlSvr2012ColumnType();

            migration.n = new SqlSvr2012NullabilityType();

            migration.d = new SqlSvr2012DefaultValue();

            return migration;

        }


        
    }
}
