CREATE TYPE [dbo].[ColumnInfoTableType] AS TABLE
(
	ColumnId		INT,
	DataTypeId		TINYINT,
	Id				BIGINT,
	IsComputed		BIT,
	IsIdentity		BIT,
	IsNullable		BIT,
	[MaxLength]		SMALLINT,
	Name			NVARCHAR(500),
	[Precision]		BIT,
	TableInfoId		BIGINT
)
