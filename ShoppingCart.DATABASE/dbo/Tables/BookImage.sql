CREATE TABLE [dbo].[BookImage] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [BookId]    INT            NULL,
    [ImageUrl]  NVARCHAR (500) NULL,
    [ImageName] NVARCHAR (100) NULL,
    CONSTRAINT [PK_BookImage] PRIMARY KEY CLUSTERED ([Id] ASC)
);

