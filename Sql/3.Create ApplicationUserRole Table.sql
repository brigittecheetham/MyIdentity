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

