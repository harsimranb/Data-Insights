CREATE PROCEDURE [dbo].[pr_CreateDataSource]
	@name						NVARCHAR(250),
	@description				NVARCHAR(1000),
	@project_id					INT,
	@data_connection_info_id	INT
AS

	DECLARE @now DATETIME = GETDATE()

	INSERT INTO tblDataSource
		(name
		,description
		,project_id
		,created_datetime
		,modified_datetime
		,data_connection_info_id)
	VALUES
		(@name
		,@description
		,@project_id
		,@now
		,@now
		,@data_connection_info_id)

RETURN 0
