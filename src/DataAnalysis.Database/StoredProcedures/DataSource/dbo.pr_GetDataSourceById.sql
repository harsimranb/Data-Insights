CREATE PROCEDURE [dbo].[pr_GetDataSourceById]
	@data_source_id		BIGINT
AS

	SELECT
		data_source_id,
		name,
		description,
		modified_datetime,
		created_datetime,
		data_connection_info_id
	FROM
		tblDataSource DS WITH(NOLOCK)
	WHERE
		data_source_id = @data_source_id
		

	SELECT
		DCI.data_connection_info_id,
		data_connection_info_type_id,
		server_name,
		database_name,
		user_name,
		password,
		DCI.modified_datetime,
		DCI.created_datetime
	FROM
		tblDataConnectionInfo DCI WITH(NOLOCK)
		INNER JOIN tblDataSource DS WITH(NOLOCK) -- TODO: Avoid this join
			ON DS.data_connection_info_id = DCI.data_connection_info_id
	WHERE
		data_source_id = @data_source_id

RETURN 0
