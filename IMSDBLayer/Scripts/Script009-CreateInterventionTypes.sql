
CREATE TABLE [dbo].[InterventionTypes] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Name]  NVARCHAR (MAX)   NOT NULL,
    [Hours] DECIMAL (18, 2)  NULL,
    [Costs] DECIMAL (18, 2)  NULL
);


