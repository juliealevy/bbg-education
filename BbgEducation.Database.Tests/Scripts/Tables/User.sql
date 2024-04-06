CREATE TABLE [dbo].[User]
(
	[user_id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] NCHAR(100) NULL, 
    [email] NCHAR(100) NULL, 
    [is_admin] BIT NOT NULL DEFAULT 0
)
