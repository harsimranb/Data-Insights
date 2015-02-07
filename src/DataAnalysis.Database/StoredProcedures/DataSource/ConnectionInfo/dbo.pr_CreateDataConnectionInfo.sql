CREATE PROCEDURE [dbo].[pr_CreateDataConnectionInfo]
	
	@server_name	NVARCHAR(500),
	@database_name	NVARCHAR(500),
	@username		NVARCHAR(500),
	@password		NVARCHAR(500),
	@type			SMALLINT

AS

	DECLARE @now DATETIME = GETDATE()

	INSERT INTO tblDataConnectionInfo
		(server_name
		,database_name
		,user_name
		,password
		,data_connection_info_type_id
		,created_datetime
		,modified_datetime)
	VALUES
		(@server_name
		,@database_name
		,@username
		,@password
		,@type
		,@now
		,@now)

	SELECT CAST(SCOPE_IDENTITY() AS BIGINT);

RETURN 0
