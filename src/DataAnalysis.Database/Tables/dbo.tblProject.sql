CREATE TABLE [dbo].[tblProject]
(
	[project_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NVARCHAR(250) NOT NULL, 
    [description] NVARCHAR(1000) NULL, 
    [created_datetime] DATETIME NOT NULL, 
    [modified_datetime] DATETIME NOT NULL
)
