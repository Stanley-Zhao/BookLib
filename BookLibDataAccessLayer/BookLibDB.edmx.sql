
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/18/2016 22:06:36
-- Generated from EDMX file: D:\Workspace\VS2015\BookLib\BookLibDAL\BookLibDB.edmx
-- --------------------------------------------------

USE master
GO

IF NOT EXISTS (SELECT name FROM sys.databases d WHERE d.name = 'BookLibDb')
CREATE DATABASE BookLibDb
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookLibDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StatusBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_StatusBook];
GO
IF OBJECT_ID(N'[dbo].[FK_BookTypeBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Books] DROP CONSTRAINT [FK_BookTypeBook];
GO
IF OBJECT_ID(N'[dbo].[FK_BookHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_BookHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_UserHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_UserHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RoleUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[BookTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookTypes];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[Histories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Histories];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(72)  NOT NULL,
    [Email] nvarchar(72)  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(72)  NOT NULL,
    [Description] nvarchar(512)  NOT NULL,
    [StatusId] int  NOT NULL,
    [BookTypeId] int  NOT NULL
);
GO

-- Creating table 'BookTypes'
CREATE TABLE [dbo].[BookTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(72)  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(72)  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartTime] datetime  NOT NULL,
    [ReturnTime] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [BookId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(72)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookTypes'
ALTER TABLE [dbo].[BookTypes]
ADD CONSTRAINT [PK_BookTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StatusId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_StatusBook]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusBook'
CREATE INDEX [IX_FK_StatusBook]
ON [dbo].[Books]
    ([StatusId]);
GO

-- Creating foreign key on [BookTypeId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_BookTypeBook]
    FOREIGN KEY ([BookTypeId])
    REFERENCES [dbo].[BookTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookTypeBook'
CREATE INDEX [IX_FK_BookTypeBook]
ON [dbo].[Books]
    ([BookTypeId]);
GO

-- Creating foreign key on [BookId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_BookHistory]
    FOREIGN KEY ([BookId])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookHistory'
CREATE INDEX [IX_FK_BookHistory]
ON [dbo].[Histories]
    ([BookId]);
GO

-- Creating foreign key on [UserId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_UserHistory]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHistory'
CREATE INDEX [IX_FK_UserHistory]
ON [dbo].[Histories]
    ([UserId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Creating all UNIQUE NONCLUSTERED constraints
-- --------------------------------------------------

-- Creating unique column on [Email] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [UNIQUE_EMAIL] 
	UNIQUE NONCLUSTERED ([Email] ASC);
GO

-- Creating unique column on [Name] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [UNIQUE_BOOKNAME] 
	UNIQUE NONCLUSTERED ([Name]);
GO

-- Creating unique column on [Name] in table 'BookTypes'
ALTER TABLE [dbo].[BookTypes]
ADD CONSTRAINT [UNIQUE_BOOKTYPENAME] 
	UNIQUE NONCLUSTERED ([Name]);
GO

-- Creating unique column on [Name] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [UNIQUE_STATUSNAME] 
	UNIQUE NONCLUSTERED ([Name]);
GO

-- Creating unique column on [Name] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [UNIQUE_ROLE] 
	UNIQUE NONCLUSTERED ([Name] ASC);
GO

-- --------------------------------------------------
-- Seeding data into database
-- --------------------------------------------------
-- BookTypes
INSERT INTO dbo.BookTypes (Name) VALUES (N'English')
INSERT INTO dbo.BookTypes (Name) VALUES (N'Management')
INSERT INTO dbo.BookTypes (Name) VALUES (N'Program')
GO

-- Status
INSERT INTO dbo.Status(Name) VALUES (N'Lending')
INSERT INTO dbo.Status(Name) VALUES (N'Ready')
INSERT INTO dbo.Status(Name) VALUES (N'Removed')
GO

-- Role
INSERT INTO dbo.Roles(Name) VALUES (N'Admin')
INSERT INTO dbo.Roles(Name) VALUES (N'User')
GO

-- User for root admin
INSERT INTO dbo.Users(Name, Email, RoleId) VALUES (N'Susi Su', N'susi.su@advent.com', 1)
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------