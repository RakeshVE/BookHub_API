CREATE TABLE [dbo].[LoginLog] (
    [Id]         BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserID]     INT            NULL,
    [Provider]   NVARCHAR (50)  NULL,
    [ProviderId] NVARCHAR (MAX) NULL,
    [LoginTime]  DATETIME       NULL,
    [LogoutTime] DATETIME       NULL,
    [CreatedOn]  DATETIME       NULL,
    [CreatedBy]  INT            NULL,
    [ModifiedBy] INT            NULL,
    [ModifiedOn] DATETIME       NULL,
    CONSTRAINT [PK_LoginLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoginLog_LoginLog] FOREIGN KEY ([Id]) REFERENCES [dbo].[LoginLog] ([Id])
);

