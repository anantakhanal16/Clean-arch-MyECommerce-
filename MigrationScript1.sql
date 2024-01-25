BEGIN TRANSACTION;
GO

ALTER TABLE [Order] DROP CONSTRAINT [FK_Order_AppUser_CustomerId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Order]') AND [c].[name] = N'OrderNumber');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Order] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Order] DROP COLUMN [OrderNumber];
GO

EXEC sp_rename N'[Order].[CustomerId]', N'UserId', N'COLUMN';
GO

EXEC sp_rename N'[Order].[IX_Order_CustomerId]', N'IX_Order_UserId', N'INDEX';
GO

ALTER TABLE [Order] ADD CONSTRAINT [FK_Order_AppUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AppUser] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240124141307_ConfiguredOrdertablev2', N'6.0.0');
GO

COMMIT;
GO

