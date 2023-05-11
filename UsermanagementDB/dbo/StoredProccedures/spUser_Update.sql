CREATE PROCEDURE [dbo].[spUser_Update]
	@Username NVARCHAR (50),
	@FirstName NVARCHAR (50),
	@LastName NVARCHAR (50),
	@Email NVARCHAR (50),
	@Photo NVARCHAR (MAX)
AS
BEGIN
	UPDATE dbo.[User]
	SET Username = @Username,
		FirstName = @FirstName,
		LastName = @LastName,
		Photo = @Photo
	WHERE Email = @Email
END
