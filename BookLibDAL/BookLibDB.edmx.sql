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
IF SCHEMA_ID(N'BL') IS NULL EXECUTE(N'CREATE SCHEMA [BL]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------
IF OBJECT_ID('[BL].[Books]', 'U') IS NOT NULL
ALTER TABLE [BL].[Books]
DROP CONSTRAINT [FK_BookTypeBook],
CONSTRAINT [FK_StatusBook]
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
IF OBJECT_ID('[BL].[BookTypes]', 'U') IS NOT NULL
DROP TABLE [BL].[BookTypes]
GO

IF OBJECT_ID('[BL].[Status]', 'U') IS NOT NULL
DROP TABLE [BL].[Status]
GO

IF OBJECT_ID('[BL].[Histories]', 'U') IS NOT NULL
DROP TABLE [BL].[Histories]
GO

IF OBJECT_ID('[BL].[Users]', 'U') IS NOT NULL
DROP TABLE [BL].[Users]
GO

IF OBJECT_ID('[BL].[Books]', 'U') IS NOT NULL
DROP TABLE [BL].[Books]
GO
-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [BL].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [BL].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TypeID] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status_Id] int  NOT NULL,
    [BookType_Id] int  NOT NULL
);
GO

-- Creating table 'BookTypes'
CREATE TABLE [BL].[BookTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [BL].[Status] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [BL].[Histories] (
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
ALTER TABLE [BL].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [BL].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BookTypes'
ALTER TABLE [BL].[BookTypes]
ADD CONSTRAINT [PK_BookTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Status'
ALTER TABLE [BL].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Histories'
ALTER TABLE [BL].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Status_Id] in table 'Books'
ALTER TABLE [BL].[Books]
ADD CONSTRAINT [FK_StatusBook]
    FOREIGN KEY ([Status_Id])
    REFERENCES [BL].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatusBook'
CREATE INDEX [IX_FK_StatusBook]
ON [BL].[Books]
    ([Status_Id]);
GO

-- Creating foreign key on [BookType_Id] in table 'Books'
ALTER TABLE [BL].[Books]
ADD CONSTRAINT [FK_BookTypeBook]
    FOREIGN KEY ([BookType_Id])
    REFERENCES [BL].[BookTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookTypeBook'
CREATE INDEX [IX_FK_BookTypeBook]
ON [BL].[Books]
    ([BookType_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'Histories'
ALTER TABLE [BL].[Histories]
ADD CONSTRAINT [FK_BookHistory]
    FOREIGN KEY ([Book_Id])
    REFERENCES [BL].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookHistory'
CREATE INDEX [IX_FK_BookHistory]
ON [BL].[Histories]
    ([Book_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Histories'
ALTER TABLE [BL].[Histories]
ADD CONSTRAINT [FK_UserHistory]
    FOREIGN KEY ([User_Id])
    REFERENCES [BL].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserHistory'
CREATE INDEX [IX_FK_UserHistory]
ON [BL].[Histories]
    ([User_Id]);
GO

-- Seeding data into database
-- BookTypes
INSERT INTO BL.BookTypes (Name) VALUES (N'Program')
INSERT INTO BL.BookTypes (Name) VALUES (N'English')
INSERT INTO BL.BookTypes (Name) VALUES (N'Management')
GO

-- Status
INSERT INTO BL.Status(Name) VALUES (N'Ready')
INSERT INTO BL.Status(Name) VALUES (N'Lending')
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------