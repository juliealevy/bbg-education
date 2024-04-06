IF EXISTS(SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
	DROP TABLE dbo.[User]
END

GO

IF EXISTS(SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Session]') AND type in (N'U'))
BEGIN
	DROP TABLE dbo.[Session]
END

IF EXISTS(SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[Program]') AND type in (N'U'))
BEGIN
	DROP TABLE dbo.[Program]
END

GO

