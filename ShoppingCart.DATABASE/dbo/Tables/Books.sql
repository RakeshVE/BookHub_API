﻿CREATE TABLE [dbo].[Books] (
    [BookId]        INT             IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (200)  NOT NULL,
    [Image]         VARBINARY (MAX) NULL,
    [ListPrice]     DECIMAL (18)    NOT NULL,
    [OurPrice]      DECIMAL (18)    NOT NULL,
    [Rating]        INT             NULL,
    [ReviewCount]   INT             NULL,
    [Details]       NVARCHAR (500)  NULL,
    [ProductType]   NVARCHAR (50)   NULL,
    [Description]   NVARCHAR (MAX)  NULL,
    [SystemReq]     NVARCHAR (MAX)  NULL,
    [Demo]          NVARCHAR (MAX)  NULL,
    [IsActive]      BIT             NULL,
    [MenuId]        INT             NOT NULL,
    [IsBook]        BIT             NOT NULL,
    [CreatedOn]     DATETIME        NULL,
    [CreatedBy]     INT             NULL,
    [ModifiedOn]    DATETIME        NULL,
    [ModifiedBy]    INT             NULL,
    [ContentType]   NVARCHAR (50)   NULL,
    [Certification] NVARCHAR (100)  NULL,
    [Publisher]     NVARCHAR (50)   NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookId] ASC),
    CONSTRAINT [FK_Books_Menu] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([MenuId])
);
