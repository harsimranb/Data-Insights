CREATE PROCEDURE [dbo].[pr_DeleteAllTablesAndColumns]
	@data_source_id		BIGINT
AS

	DELETE C
	FROM 
		tblColumnInfo C
		JOIN tblTableInfo T 
			ON C.table_info_id = T.table_info_id
	WHERE
		T.data_source_id = @data_source_id

	DELETE FROM tblTableInfo
	WHERE data_source_id = @data_source_id

RETURN 0
