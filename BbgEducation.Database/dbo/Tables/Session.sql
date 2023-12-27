CREATE TABLE [dbo].[Session] (
    [session_id]       INT         IDENTITY (1, 1) NOT NULL,
    [program_id]       INT         NOT NULL,
    [name]             NCHAR (50)  NOT NULL,
    [description]      NCHAR (255) NULL,
    [start_date]       DATE    NOT NULL,
    [end_date]         DATE    NOT NULL,
    [created_datetime] DATETIME    NULL,
    [updated_datetime] DATETIME    NULL,
    [inactivated_datetime] DATETIME    NULL,
    [inactivated_user_id] INT NULL, 
    PRIMARY KEY CLUSTERED ([session_id] ASC), 
    CONSTRAINT [FK_Session_Program] FOREIGN KEY ([program_id]) REFERENCES [Program]([program_id])
);
