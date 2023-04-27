CREATE TABLE [dbo].[Books] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [AuthorId]    INT             NOT NULL,
    [Title]       VARCHAR (256)   NOT NULL,
    [Genre]       VARCHAR (256)   NULL,
    [Price]       DECIMAL (18, 2) DEFAULT ((0)) NOT NULL,
    [PublishDate] DATETIME        NULL,
    [Description] VARCHAR (512)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Books_Authors] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

