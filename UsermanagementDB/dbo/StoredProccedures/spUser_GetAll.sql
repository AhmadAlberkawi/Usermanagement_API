CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN 
	SELECT Id, FirstName, LastName, Email, Photo
	FROM dbo.[User]
END
