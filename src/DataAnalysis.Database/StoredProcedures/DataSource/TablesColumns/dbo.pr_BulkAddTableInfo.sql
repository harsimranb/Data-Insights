CREATE PROCEDURE [dbo].[pr_BulkAddTableInfo]
	@data_source_id		BIGINT,
	@table_table_info	TableInfoTableType	READONLY
AS

	DECLARE @now DATETIME = GETDATE()

	INSERT INTO tblTableInfo
		(created_datetime
		,data_source_id
		,modified_datetime
		,name
		,object_id
		,table_type_id)
	SELECT
		@now,
		@data_source_id,
		@now,
		Name,
		ObjectId,
		TableType
	FROM
		@table_table_info

RETURN 0
