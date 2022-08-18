CREATE TABLE [dbo].[Users] (
    [UserID]       INT             IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (100)  NOT NULL,
    [PasswordHash] VARBINARY (MAX) NOT NULL,
    [PasswordSalt] VARBINARY (MAX) NOT NULL,
    [FirstName]    NVARCHAR (100)  NOT NULL,
    [LastName]     NVARCHAR (100)  NOT NULL,
    [Email]        NVARCHAR (50)   NOT NULL,
    [Phone]        NVARCHAR (12)   NOT NULL,
    [IsActive]     BIT             NULL,
    [CreatedOn]    DATETIME        NULL,
    [CreatedBy]    INT             NULL,
    [ModifiedOn]   DATETIME        NULL,
    [ModifiedBy]   INT             NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

