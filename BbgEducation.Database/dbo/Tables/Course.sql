CREATE TABLE [dbo].[Course]
(
	[course_id] INT NOT NULL  IDENTITY, 
    [name] NCHAR(100) NOT NULL, 
    [description] NCHAR(255
	) NULL, 
    [created_datetime] DATETIME NULL DEFAULT GETDATE(),
    [updated_datetime] DATETIME NULL DEFAULT GETDATE(),
    [inactivated_datetime] DATETIME NULL,
    [is_public] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT PK_Course_Course_Id PRIMARY KEY CLUSTERED (course_id),
    CONSTRAINT AK_Course_Name UNIQUE(name)
)
