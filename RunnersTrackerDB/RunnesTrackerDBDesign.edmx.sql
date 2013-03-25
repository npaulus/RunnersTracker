
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/25/2013 16:16:01
-- Generated from EDMX file: C:\Users\Nate\Documents\GitHub\RunnersTracker\RunnersTrackerDB\RunnesTrackerDBDesign.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RunnersTracker];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActivityTypesLogEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogEntries] DROP CONSTRAINT [FK_ActivityTypesLogEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoeLogEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogEntries] DROP CONSTRAINT [FK_ShoeLogEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_UserShoe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shoes] DROP CONSTRAINT [FK_UserShoe];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLogEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogEntries] DROP CONSTRAINT [FK_UserLogEntry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Shoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Shoes];
GO
IF OBJECT_ID(N'[dbo].[LogEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogEntries];
GO
IF OBJECT_ID(N'[dbo].[ActivityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] varbinary(max)  NOT NULL,
    [TimeZone] nvarchar(max)  NOT NULL,
    [DistanceType] nvarchar(max)  NOT NULL,
    [AccountConfirmed] bit  NOT NULL,
    [ConfirmCode] nvarchar(max)  NULL,
    [PassResetCode] nvarchar(max)  NULL,
    [PassResetExpire] datetime  NULL,
    [Salt] varbinary(max)  NOT NULL
);
GO

-- Creating table 'Shoes'
CREATE TABLE [dbo].[Shoes] (
    [ShoeId] int IDENTITY(1,1) NOT NULL,
    [ShoeName] nvarchar(max)  NOT NULL,
    [ShoeDistance] decimal(18,0)  NOT NULL,
    [ShoeUserId] int  NOT NULL,
    [ShoeBrand] nvarchar(max)  NOT NULL,
    [ShoeModel] nvarchar(max)  NOT NULL,
    [PurchaseDate] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'LogEntries'
CREATE TABLE [dbo].[LogEntries] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [ActivityName] nvarchar(max)  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [TimeZone] nvarchar(max)  NOT NULL,
    [Duration] int  NOT NULL,
    [Distance] decimal(18,0)  NOT NULL,
    [Calories] int  NULL,
    [Description] nvarchar(max)  NULL,
    [Tags] nvarchar(max)  NULL,
    [ActivityTypesId] int  NOT NULL,
    [ShoeId] int  NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'ActivityTypes'
CREATE TABLE [dbo].[ActivityTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActivityType_Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ShoeId] in table 'Shoes'
ALTER TABLE [dbo].[Shoes]
ADD CONSTRAINT [PK_Shoes]
    PRIMARY KEY CLUSTERED ([ShoeId] ASC);
GO

-- Creating primary key on [LogId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [PK_LogEntries]
    PRIMARY KEY CLUSTERED ([LogId] ASC);
GO

-- Creating primary key on [Id] in table 'ActivityTypes'
ALTER TABLE [dbo].[ActivityTypes]
ADD CONSTRAINT [PK_ActivityTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActivityTypesId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_ActivityTypesLogEntry]
    FOREIGN KEY ([ActivityTypesId])
    REFERENCES [dbo].[ActivityTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityTypesLogEntry'
CREATE INDEX [IX_FK_ActivityTypesLogEntry]
ON [dbo].[LogEntries]
    ([ActivityTypesId]);
GO

-- Creating foreign key on [ShoeId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_ShoeLogEntry]
    FOREIGN KEY ([ShoeId])
    REFERENCES [dbo].[Shoes]
        ([ShoeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoeLogEntry'
CREATE INDEX [IX_FK_ShoeLogEntry]
ON [dbo].[LogEntries]
    ([ShoeId]);
GO

-- Creating foreign key on [UserId] in table 'Shoes'
ALTER TABLE [dbo].[Shoes]
ADD CONSTRAINT [FK_UserShoe]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShoe'
CREATE INDEX [IX_FK_UserShoe]
ON [dbo].[Shoes]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_UserLogEntry]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLogEntry'
CREATE INDEX [IX_FK_UserLogEntry]
ON [dbo].[LogEntries]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------