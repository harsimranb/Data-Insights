CREATE PROCEDURE [dbo].[pr_GetSingleProject]
	@project_id		INT
AS

	SELECT
		project_id,
		name,
		description,
		modified_datetime,
		created_datetime
	FROM
		tblProject WITH(NOLOCK)
	WHERE
		project_id = @project_id

RETURN 0
