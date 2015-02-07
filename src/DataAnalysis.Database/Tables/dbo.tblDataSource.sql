CREATE TABLE [dbo].[tblDataSource]
(
	[data_source_id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(250) NOT NULL, 
    [description] NVARCHAR(1000) NULL, 
    [created_datetime] DATETIME NOT NULL, 
    [modified_datetime] DATETIME NOT NULL, 
    [data_connection_info_id] INT NOT NULL, 
    [project_id] INT NOT NULL, 
    CONSTRAINT [FK_tblDataSource_TotblDataConnectionInfo] FOREIGN KEY ([data_connection_info_id]) REFERENCES [tblDataConnectionInfo]([data_connection_info_id]), 
    CONSTRAINT [FK_tblDataSource_TotblProject] FOREIGN KEY ([project_id]) REFERENCES [tblProject]([project_id])
)
