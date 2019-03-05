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

