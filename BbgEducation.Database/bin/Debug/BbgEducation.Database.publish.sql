﻿/*
Deployment script for BbgEducation

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "BbgEducation"
:setvar DefaultFilePrefix "BbgEducation"
:setvar DefaultDataPath "C:\Users\jalev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\jalev\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating Table [dbo].[Program]...';


GO
CREATE TABLE [dbo].[Program] (
    [program_id]  INT         IDENTITY (1, 1) NOT NULL,
    [name]        NCHAR (100) NOT NULL,
    [description] NCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([program_id] ASC)
);


GO
PRINT N'Creating Table [dbo].[Session]...';


GO
CREATE TABLE [dbo].[Session] (
    [session_id]           INT         IDENTITY (1, 1) NOT NULL,
    [program_id]           INT         NOT NULL,
    [name]                 NCHAR (50)  NOT NULL,
    [description]          NCHAR (255) NULL,
    [start_date]           DATE        NOT NULL,
    [end_date]             DATE        NOT NULL,
    [inactivated_datetime] DATETIME    NULL,
    [inactivated_user_id]  INT         NULL,
    PRIMARY KEY CLUSTERED ([session_id] ASC)
);


GO
PRINT N'Creating Table [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [user_id]  INT         IDENTITY (1, 1) NOT NULL,
    [name]     NCHAR (100) NULL,
    [email]    NCHAR (100) NULL,
    [is_admin] BIT         NOT NULL,
    PRIMARY KEY CLUSTERED ([user_id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT 0 FOR [is_admin];


GO
PRINT N'Creating Foreign Key [dbo].[FK_Session_Program]...';


GO
ALTER TABLE [dbo].[Session]
    ADD CONSTRAINT [FK_Session_Program] FOREIGN KEY ([program_id]) REFERENCES [dbo].[Program] ([program_id]);


GO
PRINT N'Creating View [dbo].[vSessions]...';


GO
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
GO
PRINT N'Creating Procedure [dbo].[spProgramAddUpdate]...';


GO
CREATE PROCEDURE [dbo].[spProgramAddUpdate]
	@id int = -1,	
	@program_name nvarchar(50),
	@description nvarchar(255)
AS
	
BEGIN
	IF (@id = -1)
		INSERT INTO [dbo].[Program] (name,description) 
			VALUES (@program_name, @description)
	ELSE
		IF EXISTS (SELECT 1 FROM dbo.Program WHERE program_id = @id)
			UPDATE [dbo].[Program] 
			SET  [name] = @program_name, description = @description
			WHERE program_id = @id

END
GO
PRINT N'Creating Procedure [dbo].[spProgramGet]...';


GO
CREATE PROCEDURE [dbo].[spProgramGet]
	@id int
AS
BEGIN
	select program_id, name as program_name, description
	from dbo.[Program]
	where program_id = @id;

END
GO
PRINT N'Creating Procedure [dbo].[spProgramGetAll]...';


GO
CREATE PROCEDURE [dbo].[spProgramGetAll]
AS
BEGIN

	SELECT program_id, name as program_name, description
	FROM dbo.[Program]


END
GO
PRINT N'Creating Procedure [dbo].[spSessionAddUpdate]...';


GO
CREATE PROCEDURE [dbo].[spSessionAddUpdate]
	@id int = -1,
	@program_id int,
	@name nvarchar(50),
	@description nvarchar(255),
	@start datetime,
	@end datetime
AS
	BEGIN
		IF (@id = -1)
			INSERT INTO [dbo].[Session] (program_id, name,description, start_date, end_date) 
				VALUES (@program_id, @name, @description, @start, @end)
		ELSE
			IF EXISTS (SELECT 1 FROM dbo.Session WHERE session_id = @id)
				UPDATE [dbo].[Session] 
				SET  program_id = @program_id, [name] = @name, description = @description, start_date = @start, end_date = @end
				WHERE session_id = @id

	--TODO:  figure out how to handle program changes.... can it happen here?
	END
GO
PRINT N'Creating Procedure [dbo].[spSessionGet]...';


GO
CREATE PROCEDURE [dbo].[spSessionGet]
	@id int
AS
begin
	select session_id, session_name, description, start_date, end_date, inactivated_datetime, 
		program_id, program_name,
		user_id, user_name
	from dbo.[vSessions]
	where session_id = @id;
end
GO
PRINT N'Creating Procedure [dbo].[spSessionGetAll]...';


GO
CREATE PROCEDURE [dbo].[spSessionGetAll]
	@IncludeInactive bit = 0
AS
BEGIN
	
	DECLARE @SQL nvarchar(255)

	SET @SQL = 
		'SELECT session_id, session_name,description, start_date, end_date, inactivated_datetime, '
		+ 'program_id, program_name, ' 
		+ 'user_id, user_name '
		+ 'FROM dbo.[vSessions]'

	IF @includeInactive = 0
		SET @SQL = @SQL + ' '
			+ 'WHERE inactivated_datetime IS NULL'
	
	EXEC (@SQL)

END
GO
PRINT N'Creating Procedure [dbo].[spSessionInactivate]...';


GO
CREATE PROCEDURE [dbo].[spSessionInactivate]	
	@id int
AS
BEGIN	
	UPDATE [dbo].[Session]
	SET inactivated_datetime = CURRENT_TIMESTAMP
	WHERE session_id = @id
END
GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO