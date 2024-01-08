CREATE TABLE [dbo].[Program]
(
	[program_id] INT NOT NULL  IDENTITY, 
    [name] NCHAR(100) NOT NULL, 
    [description] NCHAR(255
	) NULL, 
    [created_datetime] DATETIME NULL DEFAULT GETDATE(),
    [updated_datetime] DATETIME NULL DEFAULT GETDATE(),
    [inactivated_datetime] DATETIME NULL,
    CONSTRAINT PK_Program_Program_Id PRIMARY KEY CLUSTERED (program_id),
    CONSTRAINT AK_Program_Name UNIQUE(name)
)
