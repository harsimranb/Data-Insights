CREATE TABLE [dbo].[tblTableInfo]
(
	[table_info_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [created_datetime] DATETIME NOT NULL, 
    [name] NVARCHAR(500) NOT NULL, 
    [object_id] INT NOT NULL, 
    [modified_datetime] DATETIME NOT NULL, 
    [data_source_id] BIGINT NOT NULL, 
    [table_type_id] TINYINT NOT NULL, 
    CONSTRAINT [FK_tblTableInfo_TotblDataSource] FOREIGN KEY ([data_source_id]) REFERENCES [tblDataSource]([data_source_id]), 
    CONSTRAINT [FK_tblTableInfo_TotblTableType] FOREIGN KEY ([table_type_id]) REFERENCES [tblTableType]([table_type_id])
)
