CREATE TABLE [dbo].[UploadResult] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [UploadResultAsJson] NVARCHAR (MAX) NOT NULL,
    [BookId]             INT            NULL,
    CONSTRAINT [PK_UploadResult] PRIMARY KEY CLUSTERED ([Id] ASC)
);

