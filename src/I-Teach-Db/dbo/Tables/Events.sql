CREATE TABLE [dbo].[Events]
(
    [Id] BIGINT NOT NULL IDENTITY, 
    [AggregateId] [uniqueidentifier] NOT NULL ,
    [Type] [varchar](max) NOT NULL,
    [Body] [varchar](max) NOT NULL,
    [SequenceNumber] [int] NOT NULL,
    [Timestamp] [datetime] NOT NULL, 
    PRIMARY KEY ([Id])
)
