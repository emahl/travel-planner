IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230525015253_InitialCreate')
BEGIN
    CREATE TABLE [TravelPlans] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [Type] int NOT NULL,
        [Budget] int NULL,
        [TravelTo_Notes] nvarchar(max) NOT NULL,
        [TravelTo_Type] int NOT NULL,
        [TravelTo_DepartureDate] datetime2 NULL,
        [TravelTo_DepartureLocation] nvarchar(max) NOT NULL,
        [TravelTo_ArrivalDate] datetime2 NULL,
        [TravelTo_ArrivalLocation] nvarchar(max) NOT NULL,
        [TravelHome_Notes] nvarchar(max) NOT NULL,
        [TravelHome_Type] int NOT NULL,
        [TravelHome_DepartureDate] datetime2 NULL,
        [TravelHome_DepartureLocation] nvarchar(max) NOT NULL,
        [TravelHome_ArrivalDate] datetime2 NULL,
        [TravelHome_ArrivalLocation] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_TravelPlans] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230525015253_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230525015253_InitialCreate', N'7.0.5');
END;
GO

COMMIT;
GO