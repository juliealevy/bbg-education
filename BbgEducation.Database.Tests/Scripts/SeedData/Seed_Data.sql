TRUNCATE TABLE [dbo].[User] 

GO

INSERT INTO [dbo].[User] (name, email)
VALUES ('julie', 'test@test.com');

GO

TRUNCATE TABLE [dbo].[Session]

GO

DELETE FROM [dbo].[Program]

GO

INSERT INTO [dbo].[Program] (name, description)
VALUES ('program1', 'description 1');

INSERT INTO [dbo].[Session] (name, description, program_id,start_date, end_date)
VALUES ('session1', 'description 1', 1, '2024-05-01', '2024-09-01')


