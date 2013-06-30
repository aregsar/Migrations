
Project setup Instructions:

1-Launch the Console.exe and from the menu set the path of the Console.exe application to the Consolue2 folder location.

2-Add a Migrations folder anywhere in a VS project (or use your own name)
The Migrations directory should only contain migration files. It can however contain subdirectories.

3-Add a Scripts folder anywhere in the VS project (or use your own name)

3-Add a Lib folder anywhere in the VS project (or use your own name)

4-Copy the Migrations.dll file contained in theConsole2 folder into the Libs directory.
Note: adding it to VS solution is not required.

5-Add a reference to the Migrations.dll file from the VS project that the Migrations folder was added to.
Alternatively you can reference the same dll in the Console2 folder.

6-Set the following key values in the mc.exe.config file inside the Console2 directory: 
A template of the key/values are contained in the mc.exe.config.txt file in the console2 folder.

	a-set the migrationLibraryAssemblyPath key value to the path for of the Migrations.dll file in the Libs folder

	b-set the migrationClassPath key value to the path for of the Migrations folder 
	  (note: if you use a different name for the folder, reflect that here)
	
	c-set the migrationScriptPath key value to the path for of the Sql folder (note: if you use a different name 
	  for the folder, reflect that here)
	
	d-Set the databaseName key value to the database that will be the target of the migrations. 
	  This could be a preexisting database or a new one that will be created via the console.
	
	e-Set the connectionString key value to a connection string for the database server. 
	  Be sure to Leave the Database name specified in the connection string as "Master".
	
	f-set the providerName to the appropriate .net sql server provider name for your database server.
	
	g-set the serverType to the database server type that you will be using. 
	  Current supported types are SqlServe2012 and SqlServer2008




Tip: Add a copy of the mc.exe.config to your VS project to easlily switch migration databases by copying the config file from 
the prioject into the console2 folder. 

7-launch console 2 and type mc then hit return to run the migration console (alternatively you can execute mc.exe directly from windows explorer)





