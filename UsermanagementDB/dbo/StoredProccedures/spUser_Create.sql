CREATE PROCEDURE [dbo].[spUser_Create]
	@Username NVARCHAR (50),
	@FirstName NVARCHAR (50),
	@LastName NVARCHAR (50),
	@Email NVARCHAR (50),
	@Photo NVARCHAR (MAX),
	@Role NVARCHAR (50),
	@PasswordHash VARBINARY (MAX),
	@PasswordSalt VARBINARY (MAX)
AS
BEGIN
	INSERT INTO dbo.[User] OUTPUT INSERTED.ID
	VALUES (@Username, @FirstName, @LastName, @Email, @Photo, @Role, @PasswordHash, @PasswordSalt);
END


