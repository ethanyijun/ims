
CREATE TABLE [dbo].[Users] (
    [Id]              UNIQUEIDENTIFIER NOT NULL,
    [Name]            NVARCHAR (50)    NOT NULL,
    [Type]            INT              NULL,
    [AuthorisedHours] DECIMAL (18, 2)  NULL,
    [AuthorisedCosts] DECIMAL (18, 2)  NULL,
    [IdentityId]      NVARCHAR (128)   NOT NULL,
    [DistrictId]      UNIQUEIDENTIFIER NULL
);


