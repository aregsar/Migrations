using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using Migrations.Schemas;

namespace Migrations
{

   

    public abstract class MigrationFactory
    {


        public abstract Migration GetMigration(string migrationFilePath, string migrationLibraryAssemblyPath);

      
        public static MigrationFactory GetFactoryForDatabaseType(string databaseType)
        {     
            return new SqlSvr2012MigrationFactory();
        }

        public static string GetMigrationFilePathForVersion(string version, string migrationClassFileDirectory)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(migrationClassFileDirectory);

            foreach (var file in files)
            {

                if (file.Contains(version))
                {
                    return file;
                }

            }
            return null;
        }
        public static IEnumerable<string> GetMigrationFilePaths(string migrationClassFileDirectory)
        {
            return Directory.EnumerateFiles(migrationClassFileDirectory);
        }

        public static SortedList GetSortedMigrationFileVersionsFromMigrationFiles(string migrationClassFileDirectory)
        {
            IEnumerable<string> filesPaths = Directory.EnumerateFiles(migrationClassFileDirectory);

            //sorted list is sorted by keys
            SortedList list = new SortedList();

            foreach (string filePath in filesPaths)
            {
                string[] pathparts = filePath.Split('\\');
                string[] fileparts = pathparts[pathparts.Length - 1].Replace(".cs", "").Split('_');
                string version = fileparts[0];
                list.Add(version, version);
            }

            return list;
        }

        public static string GetVersionFromMigrationFilePath(string filePath)
        {
            string[] pathparts = filePath.Split('\\');
            string[] fileparts = pathparts[pathparts.Length - 1].Replace(".cs", "").Split('_');
            string version = fileparts[0];

            return version;
        }
    }
}
