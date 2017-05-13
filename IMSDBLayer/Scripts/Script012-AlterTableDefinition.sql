ALTER TABLE [dbo].[Clients] ADD DEFAULT newId() FOR [Id];
go
ALTER TABLE [dbo].[Interventions] ADD DEFAULT newId() FOR [Id];