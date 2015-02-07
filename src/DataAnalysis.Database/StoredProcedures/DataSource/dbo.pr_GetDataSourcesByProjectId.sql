CREATE PROCEDURE [dbo].[pr_GetDataSourcesByProjectId]
	@project_id		INT
AS

	SELECT
		data_source_id,
		name,
		description,
		modified_datetime,
		created_datetime,
		data_connection_info_id
	FROM
		tblDataSource WITH(NOLOCK)
	WHERE
		project_id = @project_id

RETURN 0
