CREATE PROCEDURE [dbo].[pr_CreateProject]
	 @name			NVARCHAR(250),
	 @description	NVARCHAR(1000)
AS

	DECLARE @now DATETIME = GETDATE()

	INSERT INTO tblProject
		(name
		,description
		,created_datetime
		,modified_datetime)
	VALUES
		(@name
		,@description
		,@now
		,@now)

RETURN 0
