
CREATE TABLE [dbo].[Clients] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Name]       NVARCHAR (50)    NOT NULL,
    [Location]   NVARCHAR (MAX)   NOT NULL,
    [DistrictId] UNIQUEIDENTIFIER NOT NULL
);


