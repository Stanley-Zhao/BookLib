USE BookLibDb
GO

-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2016 17:55:04
-- Generated from EDMX file: D:\Workspace\VS2015\BookLib\BookLibDAL\BookLibDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookLibDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------
IF OBJECT_ID('[dbo].[Books]', 'U') IS NOT NULL
ALTER TABLE [dbo].[Books]
DROP CONSTRAINT [FK_BookTypeBook],
CONSTRAINT [FK_StatusBook]
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
IF OBJECT_ID('[dbo].[BookTypes]', 'U') IS NOT NULL
DROP TABLE [dbo].[BookTypes]
GO

IF OBJECT_ID('[dbo].[Status]', 'U') IS NOT NULL
DROP TABLE [dbo].[Status]
GO

IF OBJECT_ID('[dbo].[Histories]', 'U') IS NOT NULL
DROP TABLE [dbo].[Histories]
GO

IF OBJECT_ID('[dbo].[Users]', 'U') IS NOT NULL
DROP TABLE [dbo].[Users]
GO

IF OBJECT_ID('[dbo].[Books]', 'U') IS NOT NULL
DROP TABLE [dbo].[Books]
GO
-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TypeID] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status_Id] int  NOT NULL,
    [BookType_Id] int  NOT NULL
);
GO

-- Creating table 'BookTypes'
CREATE TABLE [dbo].[BookTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartTime] nvarchar(max)  NOT NULL,
    [ReturnTime] nvarchar(max)  NOT NULL,
    [Book_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Status_Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_StatusBook]
    FOREIGN KEY ([Status_Id])
    REFERENCES [dbo].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusBook'
CREATE INDEX [IX_FK_StatusBook]
ON [dbo].[Books]
    ([Status_Id]);
GO

-- Creating foreign key on [BookType_Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_BookTypeBook]
    FOREIGN KEY ([BookType_Id])
    REFERENCES [dbo].[BookTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookTypeBook'
CREATE INDEX [IX_FK_BookTypeBook]
ON [dbo].[Books]
    ([BookType_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_BookHistory]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookHistory'
CREATE INDEX [IX_FK_BookHistory]
ON [dbo].[Histories]
    ([Book_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_UserHistory]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHistory'
CREATE INDEX [IX_FK_UserHistory]
ON [dbo].[Histories]
    ([User_Id]);
GO

-- Seeding data into database
-- BookTypes
INSERT INTO dbo.BookTypes (Name) VALUES (N'English')
INSERT INTO dbo.BookTypes (Name) VALUES (N'Management')
INSERT INTO dbo.BookTypes (Name) VALUES (N'Program')
GO

-- Status
INSERT INTO dbo.Status(Name) VALUES (N'Lending')
INSERT INTO dbo.Status(Name) VALUES (N'Ready')
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------