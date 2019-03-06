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

