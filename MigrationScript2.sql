BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240124145505_ConfiguredOrdertablev3', N'6.0.0');
GO

COMMIT;
GO

