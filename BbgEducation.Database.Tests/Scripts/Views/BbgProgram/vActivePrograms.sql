CREATE VIEW [dbo].[vActivePrograms]
	AS 
	
	SELECT  program_id, name, description
	FROM dbo.[Program]
	WHERE inactivated_datetime is null
