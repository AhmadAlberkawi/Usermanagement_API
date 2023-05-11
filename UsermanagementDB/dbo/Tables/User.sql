CREATE TABLE [dbo].[User]
(
	[Id]           INT             IDENTITY (1, 1) NOT NULL,
	[Username]     NVARCHAR (50)  NOT NULL,
	[FirstName]    NVARCHAR (50)  NOT NULL,
	[LastName]     NVARCHAR (50)  NOT NULL,
	[Email]        NVARCHAR (50)  NOT NULL,
	[Photo]        NVARCHAR (MAX)  NULL,
	[Role]         NVARCHAR (50)  NULL,
	[PasswordHash] VARBINARY(MAX) NOT NULL,
	[PasswordSalt] VARBINARY (MAX) NOT NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

