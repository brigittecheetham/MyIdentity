IF NOT EXISTS(	SELECT 1 
				FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_NAME = 'ApplicationRole'
				AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE ApplicationRole
	(
		Id int identity(1,1) not null,
		Name nvarchar(256) not null,
	)

	ALTER TABLE ApplicationRole ADD CONSTRAINT pkApplicationRole PRIMARY KEY(Id)

END

GO

IF NOT EXISTS(	SELECT 1 
				FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_NAME = 'ApplicationUser'
				AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE ApplicationUser
	(
		Id int identity(1,1) not null,
		UserName nvarchar(256) not null,
		Title varchar(10),
		Name nvarchar(100),
		Surname nvarchar(100),
		Cellphone varchar(10),
		HomeAddress nvarchar(500),
		PasswordHash nvarchar(max),
		SecurityStamp nvarchar(max)
	)

	CREATE CLUSTERED INDEX ixcApplicationUser ON ApplicationUser(UserName)

	ALTER TABLE ApplicationUser ADD CONSTRAINT pkApplicationUser PRIMARY KEY(Id)

END

GO

IF NOT EXISTS(	SELECT 1 
				FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_NAME = 'ApplicationUserRole'
				AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE ApplicationUserRole
	(
		UserId int not null,
		RoleId int not null
	)
END

GO

IF NOT EXISTS (SELECT 1 FROM ApplicationRole WHERE Name = 'Administrator')
BEGIN
	INSERT INTO ApplicationRole (Name) VALUES ('Administrator')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationRole WHERE Name = 'General User')
BEGIN
	INSERT INTO ApplicationRole (Name) VALUES ('General User')
END

GO

DECLARE @Result TABLE (UserId INT)
DECLARE @AdminRoleID INT = (SELECT Id FROM ApplicationRole WHERE Name = 'Administrator')

INSERT INTO [dbo].[ApplicationUser]
           ([UserName]
           ,[Title]
           ,[Name]
           ,[Surname]
           ,[Cellphone]
           ,[HomeAddress]
           ,[PasswordHash]
           ,[SecurityStamp])
	OUTPUT Inserted.Id INTO @Result
    VALUES 
           ('admin@test.com'
           ,'Mr'
           ,'Admin'
           ,'Test'
           ,'0800000'
           ,'Somewhere'
           ,'AIgUfeZl1rbguLV7xr9mOluP4CP3CXX+t50haP+AezVa6Al44Ep4V4iTB7+isUz/vw=='
           ,'')

INSERT INTO ApplicationUserRole 
(
	UserId,
	RoleId
)
SELECT 
	UserId,
	@AdminRoleID 
FROM @Result


