/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF (SELECT COUNT(*) FROM tblTableType) = 0
BEGIN

	INSERT INTO tblTableType
		(table_type_id,table_type_name)
	VALUES
		(1, 'User Table')
	INSERT INTO tblTableType
		(table_type_id,table_type_name)
	VALUES
		(2, 'View')
END
GO

IF (SELECT COUNT(*) FROM tblDataConnectionInfoType) = 0
BEGIN

	INSERT INTO tblDataConnectionInfoType
		(data_connection_info_type_id,data_connection_info_type_name)
	VALUES
		(1, 'Microsoft Sql Server')
END
GO

IF (SELECT COUNT(*) FROM tblColumnDataType) = 0
BEGIN

	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(0, 'Big Int')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(1, 'Binary')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(2, 'Bits')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(3, 'Char')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(4, 'DateTime')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(5, 'Decimal')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(6, 'Float')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(7, 'Image')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(8, 'Int')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(9, 'Money')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(10, 'NChar')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(11, 'NText')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(12, 'NVarChar')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(13, 'Real')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(14, 'Unique Identifier')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(15, 'Small DateTime')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(16, 'Small Int')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(17, 'Small Money')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(18, 'Text')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(19, 'Timestamp')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(20, 'Tiny Int')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(21, 'VarBinary')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(22, 'VarChar')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(23, 'Variant')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(25, 'Xml')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(29, 'Udt')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(30, 'Structured')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(31, 'Date')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(32, 'Time')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(33, 'DateTime2')
	INSERT INTO tblColumnDataType
		(column_data_type_id,column_data_type_name)
	VALUES
		(34, 'DateTimeOffset')
END
GO

