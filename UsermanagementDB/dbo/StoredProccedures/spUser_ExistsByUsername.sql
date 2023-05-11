CREATE PROCEDURE [dbo].[spUser_ExistsByUsername]
	@Username INT
AS
BEGIN 
	SELECT CASE WHEN EXISTS
	(
		SELECT *
		FROM [User]
		WHERE Username = @Username
	)
	THEN CAST(1 AS BIT)
	ELSE CAST(0 AS BIT) END 
END
