CREATE TABLE [dbo].[Menu] (
    [MenuId]   INT            IDENTITY (1, 1) NOT NULL,
    [MenuName] NVARCHAR (100) NOT NULL,
    [SubMenu]  NVARCHAR (100) NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

