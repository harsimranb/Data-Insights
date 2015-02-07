CREATE TABLE [dbo].[tblDataConnectionInfo]
(
	[data_connection_info_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [server_name] NVARCHAR(500) NOT NULL, 
    [database_name] NVARCHAR(500) NOT NULL, 
    [user_name] NVARCHAR(500) NOT NULL, 
    [password] NVARCHAR(500) NOT NULL, 
    [created_datetime] DATETIME NOT NULL, 
    [modified_datetime] DATETIME NOT NULL, 
    [data_connection_info_type_id] SMALLINT NOT NULL, 
    CONSTRAINT [FK_tblDataConnectionInfo_TotblDataConnectionInfoType] FOREIGN KEY ([data_connection_info_type_id]) REFERENCES [tblDataConnectionInfoType]([data_connection_info_type_id])
)
