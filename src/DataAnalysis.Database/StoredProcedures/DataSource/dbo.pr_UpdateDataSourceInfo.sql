CREATE PROCEDURE [dbo].[pr_UpdateDataSourceInfo]
	@data_source_id				BIGINT,
	@name						NVARCHAR(250),
	@description				NVARCHAR(1000),
	@data_connection_info_id	INT
AS

	UPDATE tblDataSource
	SET
		name = @name,
		description = @description,
		data_connection_info_id = @data_connection_info_id,
		modified_datetime = GETDATE()
	WHERE
		data_source_id = @data_source_id

RETURN 0
