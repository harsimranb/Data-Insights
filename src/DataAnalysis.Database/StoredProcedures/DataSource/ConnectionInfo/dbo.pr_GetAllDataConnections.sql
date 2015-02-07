CREATE PROCEDURE [dbo].[pr_GetAllDataConnections]
AS

	SELECT
		data_connection_info_id,
		data_connection_info_type_id,
		server_name,
		database_name,
		user_name,
		password
	FROM
		tblDataConnectionInfo WITH(NOLOCK)

RETURN 0
