CREATE TABLE [dbo].[Article]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [CreatedById] INT NOT NULL, 
    CONSTRAINT [FK_Article_Creator] FOREIGN KEY ([CreatedById]) REFERENCES [User]([Id])
)
