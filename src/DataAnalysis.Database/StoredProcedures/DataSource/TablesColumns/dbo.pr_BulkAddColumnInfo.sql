CREATE PROCEDURE [dbo].[pr_BulkAddColumnInfo]
	@table_info_id		BIGINT,
	@table_column_info	ColumnInfoTableType	READONLY
AS
	INSERT INTO tblColumnInfo
		(column_data_type_id
		,db_column_id
		,is_computed
		,is_identity
		,is_nullable
		,max_length
		,name
		,precision
		,table_info_id)
	SELECT
		DataTypeId,
		ColumnId,
		IsComputed,
		IsIdentity,
		IsNullable,
		MaxLength,
		Name,
		Precision,
		@table_info_id
	FROM
		@table_column_info

RETURN 0
