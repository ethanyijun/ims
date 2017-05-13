
CREATE TABLE [dbo].[Interventions] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Hours]              DECIMAL (18, 2)  NULL,
    [Costs]              DECIMAL (18, 2)  NULL,
    [LifeRemaining]      INT              NULL,
    [Comments]           NVARCHAR (MAX)   NULL,
    [State]              INT              NULL,
    [DateCreate]         DATETIME         NULL,
    [DateFinish]         DATETIME         NULL,
    [DateRecentVisit]    DATETIME         NULL,
    [InterventionTypeId] UNIQUEIDENTIFIER NULL,
    [ClientId]           UNIQUEIDENTIFIER NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NULL,
    [ApprovedBy]         UNIQUEIDENTIFIER NULL
);


