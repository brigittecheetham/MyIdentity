IF NOT EXISTS (SELECT 1 FROM ApplicationRole WHERE Name = 'Administrator')
BEGIN
	INSERT INTO ApplicationRole (Name) VALUES ('Administrator')
END

IF NOT EXISTS (SELECT 1 FROM ApplicationRole WHERE Name = 'General User')
BEGIN
	INSERT INTO ApplicationRole (Name) VALUES ('General User')
END