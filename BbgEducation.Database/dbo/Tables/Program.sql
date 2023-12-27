CREATE TABLE [dbo].[Program]
(
	[program_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NCHAR(100) NOT NULL, 
    [description] NCHAR(255
	) NULL, 
    [created_datetime] DATETIME NULL,
    [updated_datetime] DATETIME NULL,
    [inactivated_datetime] DATETIME NULL,
)
