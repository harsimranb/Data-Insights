CREATE TYPE [dbo].[TableInfoTableType] AS TABLE
(
	Id				BIGINT,
	ObjectId		INT,
	CreatedOn		DATETIME,
	ModifiedOn		DATETIME,
	TableType		TINYINT,
	Name			NVARCHAR(100),
	DataSourceId	BIGINT
)
