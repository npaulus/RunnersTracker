
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/12/2013 12:28:21
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

IF OBJECT_ID(N'[dbo].[FK_UserShoe]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Shoes] DROP CONSTRAINT [FK_UserShoe];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoeLogEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LogEntries] DROP CONSTRAINT [FK_ShoeLogEntry];
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
    [PurchaseDate] datetime  NOT NULL
);
GO

-- Creating table 'LogEntries'
CREATE TABLE [dbo].[LogEntries] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [ActivityName] nvarchar(max)  NOT NULL,
    [ActivityType] nvarchar(max)  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [TimeZone] nvarchar(max)  NOT NULL,
    [Duration] int  NOT NULL,
    [Distance] decimal(18,0)  NOT NULL,
    [Calories] int  NULL,
    [Description] nvarchar(max)  NULL,
    [Tags] nvarchar(max)  NULL,
    [ShoeShoeId] int  NOT NULL,
    [UserUserId] int  NOT NULL
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ShoeUserId] in table 'Shoes'
ALTER TABLE [dbo].[Shoes]
ADD CONSTRAINT [FK_UserShoe]
    FOREIGN KEY ([ShoeUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShoe'
CREATE INDEX [IX_FK_UserShoe]
ON [dbo].[Shoes]
    ([ShoeUserId]);
GO

-- Creating foreign key on [ShoeShoeId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_ShoeLogEntry]
    FOREIGN KEY ([ShoeShoeId])
    REFERENCES [dbo].[Shoes]
        ([ShoeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoeLogEntry'
CREATE INDEX [IX_FK_ShoeLogEntry]
ON [dbo].[LogEntries]
    ([ShoeShoeId]);
GO

-- Creating foreign key on [UserUserId] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [FK_UserLogEntry]
    FOREIGN KEY ([UserUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLogEntry'
CREATE INDEX [IX_FK_UserLogEntry]
ON [dbo].[LogEntries]
    ([UserUserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------