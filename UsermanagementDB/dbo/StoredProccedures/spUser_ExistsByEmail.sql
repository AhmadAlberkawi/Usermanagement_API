﻿CREATE PROCEDURE [dbo].[spUser_ExistsByEmail]
	@Email INT
AS
BEGIN 
	SELECT CASE WHEN EXISTS
	(
		SELECT *
		FROM [User]
		WHERE Email = @Email
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END 
END
