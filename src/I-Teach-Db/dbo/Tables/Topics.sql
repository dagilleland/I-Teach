CREATE TABLE [dbo].[Topics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [PlanningCalendarId] UNIQUEIDENTIFIER NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Duration] INT NOT NULL, 
    [Sequence] INT NOT NULL
)
