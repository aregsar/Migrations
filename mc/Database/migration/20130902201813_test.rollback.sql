begin transaction 
drop table dbo.test_table
update dbo.SchemaVersion set  VersionTimestamp = '20130902201813' 
commit transaction
