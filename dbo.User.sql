CREATE TABLE [dbo].[User] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (30)  NOT NULL,
    [PasswordHash] NVARCHAR (MAX)  NOT NULL,
    [FirstName]    NVARCHAR (150) NOT NULL,
    [LastName]     NVARCHAR (150) NOT NULL,
    [Phone]        NVARCHAR (20)  NULL,
    [Email]        NVARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

