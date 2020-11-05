USE [master]
IF db_id('Infinitus') IS NULl
  CREATE DATABASE [Infinitus]
GO
USE [Infinitus]
GO
DROP TABLE IF EXISTS [UserFriend];
DROP TABLE IF EXISTS Result;
DROP TABLE IF EXISTS Comment;
DROP TABLE IF EXISTS Comments;
DROP TABLE IF EXISTS HistoryDefaultRitual;
DROP TABLE IF EXISTS HistoryRitual;
DROP TABLE IF EXISTS History;
DROP TABLE IF EXISTS Ritual;
DROP TABLE IF EXISTS Rituals;
DROP TABLE IF EXISTS DefaultRitual;
DROP TABLE IF EXISTS [UserProfile];
GO
CREATE TABLE [UserProfile] (
  [Id] integer PRIMARY KEY IDENTITY,
  [FirebaseUserId] NVARCHAR(28) NOT NULL,
  [FirstName] nvarchar(50) NOT NULL,
  [LastName] nvarchar(50) NOT NULL,
  [Email] nvarchar(555) NOT NULL,
  [CreateDateTime] datetime NOT NULL,
  [ImageLocation] nvarchar(255),
  [UserTypeId] integer NOT NULL,
  [isActive] bit NOT NULL,
  CONSTRAINT UQ_FirebaseUserId UNIQUE(FirebaseUserId)
)
GO
CREATE TABLE [Ritual] (
  [id] int Primary Key IDENTITY,
  [userId] int NOT NULL,
  [name] nvarchar(255) NOT NULL,
  [weight] float NOT NULL,
  [isActive] bit NOT NULL
)
GO
CREATE TABLE [Comment] (
  [id] int,
  [ritualId] int NOT NULL,
  [comment] nvarchar(255) NOT NULL
  PRIMARY KEY (Id),
  FOREIGN KEY (ritualId) REFERENCES Ritual(iD)
)
GO
CREATE TABLE [History] (
  [id] int,
  [date] datetime NOT NULL,
  [physical] bit NOT NULL,
  [mental] bit NOT NULL,
  [spiritual] bit NOT NULL,
  [ritualId] int NOT NULL,
  [result] int NULL,
  [userId] int NOT NULL,
  [note] nvarchar(255)
  PRIMARY KEY (Id),
  FOREIGN KEY (ritualId) REFERENCES Ritual(id),
  FOREIGN KEY (userId) REFERENCES UserProfile(id)
)
