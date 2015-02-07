CREATE TABLE [dbo].[tblColumnInfo]
(
	[column_info_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(500) NOT NULL, 
    [is_computed] BIT NOT NULL, 
    [is_identity] BIT NOT NULL, 
    [is_nullable] BIT NOT NULL, 
    [max_length] SMALLINT NOT NULL, 
    [precision] TINYINT NOT NULL, 
    [db_column_id] INT NOT NULL, 
    [table_info_id] BIGINT NOT NULL,
    [column_data_type_id] TINYINT NOT NULL, 
    CONSTRAINT [FK_tblColumnInfo_TotblTableInfo] FOREIGN KEY ([table_info_id]) REFERENCES [tblTableInfo]([table_info_id]), 
    CONSTRAINT [FK_tblColumnInfo_TotblColumnDataType] FOREIGN KEY ([column_data_type_id]) REFERENCES [tblColumnDataType]([column_data_type_id])
)
