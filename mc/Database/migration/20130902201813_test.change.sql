begin transaction 
create table dbo.test_table( 
id int identity(1,1) not null
,name nvarchar(50) null
 ) 
update dbo.SchemaVersion set  VersionTimestamp = '20130902201813' 
commit transaction
