begin transaction 
create table dbo.test_table( 
id int identity(1,1) not null
,name nvarchar(50) null
 ) 
commit transaction 
