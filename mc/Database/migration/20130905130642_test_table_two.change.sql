begin transaction 
create table dbo.test_table_two( 
id int identity(1,1) not null
,age nvarchar(50) null
 ) 
update dbo.SchemaVersion set  VersionTimestamp = '20130905130642' 
commit transaction
