CREATE PROCEDURE [dbo].[pr_GetAllProjects]

AS

	SELECT
		project_id,
		name,
		description,
		modified_datetime,
		created_datetime
	FROM
		tblProject WITH(NOLOCK)

RETURN 0
