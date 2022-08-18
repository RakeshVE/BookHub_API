CREATE TABLE [dbo].[CorpSales] (
    [CorpSalesId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)  NOT NULL,
    [LastName]    NVARCHAR (50)  NOT NULL,
    [Phone]       NVARCHAR (12)  NOT NULL,
    [Email]       NVARCHAR (50)  NOT NULL,
    [CompanyName] NVARCHAR (200) NOT NULL,
    [State]       NVARCHAR (50)  NOT NULL,
    [Country]     NVARCHAR (50)  NOT NULL,
    [Purpose]     NVARCHAR (200) NOT NULL,
    [Details]     NVARCHAR (500) NULL,
    [Createdn]    DATETIME       NULL,
    [CreatedBy]   INT            NULL,
    [ModifiedOn]  DATETIME       NULL,
    [ModifiedBy]  INT            NULL,
    CONSTRAINT [PK_CorpSales] PRIMARY KEY CLUSTERED ([CorpSalesId] ASC)
);

