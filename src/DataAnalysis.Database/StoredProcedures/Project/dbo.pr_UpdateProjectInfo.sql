CREATE PROCEDURE [dbo].[pr_UpdateProjectInfo]
	@project_id		INT,
	@name			NVARCHAR(250),
	@description	NVARCHAR(1000)
AS

	UPDATE tblProject
	SET
		name = @name,
		description = @description,
		modified_datetime = GETDATE()
	WHERE
		project_id = @project_id

RETURN 0
