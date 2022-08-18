CREATE TABLE [dbo].[Photos] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]    DATETIME       NOT NULL,
    [PublicId]     NVARCHAR (200) NOT NULL,
    [Version]      INT            NOT NULL,
    [BookId]       INT            NULL,
    [Signature]    NVARCHAR (500) NOT NULL,
    [Width]        INT            NOT NULL,
    [Height]       INT            NOT NULL,
    [Format]       NVARCHAR (100) NOT NULL,
    [ResourceType] NVARCHAR (100) NOT NULL,
    [Bytes]        INT            NOT NULL,
    [Type]         NVARCHAR (50)  NOT NULL,
    [Url]          NVARCHAR (500) NOT NULL,
    [SecureUrl]    NVARCHAR (500) NOT NULL,
    [Path]         NVARCHAR (500) NULL,
    CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

