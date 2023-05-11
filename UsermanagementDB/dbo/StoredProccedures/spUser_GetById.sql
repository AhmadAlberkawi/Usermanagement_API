CREATE PROCEDURE [dbo].[spUser_GetById]
	@Id INT
AS
BEGIN
	SELECT *
	FROM dbo.[User]
	WHERE Id = @Id
END
