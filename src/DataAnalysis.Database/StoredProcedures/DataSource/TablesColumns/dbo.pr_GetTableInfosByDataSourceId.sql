CREATE PROCEDURE [dbo].[pr_GetTableInfosByDataSourceId]
	@data_source_id		BIGINT
AS

	SELECT
		table_info_id,
		table_type_id,
		name,
		object_id,
		created_datetime,
		modified_datetime,
		data_source_id
	FROM
		tblTableInfo WITH(NOLOCK)
	WHERE
		data_source_id = @data_source_id

RETURN 0
