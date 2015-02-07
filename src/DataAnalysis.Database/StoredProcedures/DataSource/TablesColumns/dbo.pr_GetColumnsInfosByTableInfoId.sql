CREATE PROCEDURE [dbo].[pr_GetColumnsInfosByTableInfoId]
	@table_info_id		BIGINT
AS

	SELECT
		column_info_id,
		name,
		is_computed,
		is_identity,
		is_nullable,
		max_length,
		precision,
		db_column_id,
		column_data_type_id,
		table_info_id
	FROM
		tblColumnInfo WITH(NOLOCK)
	WHERE
		table_info_id = @table_info_id

RETURN 0
