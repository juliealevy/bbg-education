CREATE VIEW [dbo].[vSessions]
	AS 
	
	SELECT		
		session_id,
		s.name as session_name,
		s.description,
		s.start_date,
		s.end_date,
		s.inactivated_datetime,
		s.inactivated_user_id as user_id,
		u.name as user_name,
		p.program_id,
		p.name as program_name
	FROM [dbo].[Session] s
		INNER JOIN dbo.Program p on p.program_id = s.program_id
		LEFT JOIN dbo.[User] u on u.user_id = s.inactivated_user_id
		
