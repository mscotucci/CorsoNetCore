CREATE TABLE [dbo].[Authors] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (256) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);