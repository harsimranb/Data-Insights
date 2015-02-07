CREATE PROCEDURE [dbo].[pr_CreateTableInfo]
	@name				NVARCHAR(500),
	@object_id			INT,
	@data_source_id		BIGINT,
	@table_type_id		TINYINT
AS

	DECLARE @now DATETIME = GETDATE()

	INSERT INTO tblTableInfo
		(created_datetime
		,data_source_id
		,modified_datetime
		,name
		,object_id
		,table_type_id)
	VALUES
		(@now
		,@data_source_id
		,@now
		,@name
		,@object_id
		,@table_type_id)
	
	SELECT CAST(SCOPE_IDENTITY() AS BIGINT);
RETURN 0
