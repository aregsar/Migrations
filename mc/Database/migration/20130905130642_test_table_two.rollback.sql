begin transaction 
drop table dbo.test_table_two
update dbo.SchemaVersion set  VersionTimestamp = '20130905130642' 
commit transaction
