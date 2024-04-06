CREATE TABLE [dbo].[Session] (
    [session_id]       INT         IDENTITY (1, 1) NOT NULL,
    [program_id]       INT         NOT NULL,
    [name]             NCHAR (50)  NOT NULL,
    [description]      NCHAR (255) NULL,
    [start_date]       DATE    NOT NULL,
    [end_date]         DATE    NOT NULL,
    [created_datetime] DATETIME    NULL DEFAULT GETDATE(),
    [updated_datetime] DATETIME    NULL DEFAULT GETDATE(),
    [inactivated_datetime] DATETIME    NULL,
    [inactivated_user_id] INT NULL, 
    CONSTRAINT PK_Session_Session_Id PRIMARY KEY CLUSTERED ([session_id]), 
    CONSTRAINT [FK_Session_Program] FOREIGN KEY ([program_id]) REFERENCES [Program]([program_id]),
    CONSTRAINT AK_Session_Name UNIQUE(name)
    

);
