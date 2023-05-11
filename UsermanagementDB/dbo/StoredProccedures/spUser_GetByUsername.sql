CREATE PROCEDURE [dbo].[spUser_GetByUsername]
	@Username NVARCHAR(50)
AS
BEGIN 
	SELECT *
	FROM dbo.[User]
	WHERE Username = @Username
END
