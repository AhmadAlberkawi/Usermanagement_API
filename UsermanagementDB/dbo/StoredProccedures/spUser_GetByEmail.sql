CREATE PROCEDURE [dbo].[spUser_GetByEmail]
	@Email NVARCHAR(50)
AS
BEGIN
	SELECT *
	FROM dbo.[User]
	WHERE Email = @Email
end
