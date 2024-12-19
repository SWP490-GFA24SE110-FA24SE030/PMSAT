-- Create Database
CREATE DATABASE PMSAT;
GO

-- Use the created database
USE PMSAT;
GO

-- Table: Users
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255),
    Email NVARCHAR(255),
    Password NVARCHAR(255),
    Role NVARCHAR(50), -- user,admin
    Status NVARCHAR(50)
);

-- Table: Project
CREATE TABLE Project (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
);

-- Table: ProjectMember
CREATE TABLE ProjectMember (
    Role NVARCHAR(50), --leader, ...
    UserId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

-- Table: AnalysisResult
CREATE TABLE AnalysisResult (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Coverage NVARCHAR(50),
    CodeComplexity NVARCHAR(50),
    QualityMetrics NVARCHAR(50),
    RequirementError NVARCHAR(50),
    SecurityError NVARCHAR(50),
    SyntaxError NVARCHAR(50),
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

-- Table: Repository
CREATE TABLE Repository (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255),
    Owner NVARCHAR(255),
    Url NVARCHAR(255),
    UserId UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

-- Table: Commits
CREATE TABLE Commits (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    CreatedAt DATETIME,
    Message NVARCHAR(255),
    RepositoryId UNIQUEIDENTIFIER,
    FOREIGN KEY (RepositoryId) REFERENCES Repository(Id) ON DELETE CASCADE
);

-- Table: PullRequest
CREATE TABLE PullRequest (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
    Body NVARCHAR(255),
    Url NVARCHAR(255),
    CreatedAt DATETIME,
    MergedAt DATETIME,
    Status NVARCHAR(50),
    RepositoryId UNIQUEIDENTIFIER,
    FOREIGN KEY (RepositoryId) REFERENCES Repository(Id) ON DELETE CASCADE
);

-- Table: Sprint
CREATE TABLE Sprint (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255),
    StartDate DATETIME,
    EndDate DATETIME,
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

CREATE TABLE Board (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
	Status NVARCHAR(50), --todo,....
	ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

-- Table: TaskP
CREATE TABLE TaskP (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
	Title NVARCHAR(50),
    Description NVARCHAR(255),
	Priority INT,
    Created DATETIME,
    Updated DATETIME,
    ReporterId UNIQUEIDENTIFIER,
	AssigneeId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
	SprintId UNIQUEIDENTIFIER,
	StatusId UNIQUEIDENTIFIER,
    FOREIGN KEY (ReporterId) REFERENCES Users(Id) ON DELETE NO ACTION,
	FOREIGN KEY (AssigneeId) REFERENCES Users(Id) ON DELETE NO ACTION,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE, -- Cascade delete for Project
	FOREIGN KEY (SprintId) REFERENCES Sprint(Id) ON DELETE NO ACTION,
	FOREIGN KEY (StatusId) REFERENCES Board(Id) ON DELETE NO ACTION
);

-- Table: Tag
CREATE TABLE Tag (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
	Name NVARCHAR(255), 
);

-- Table: TaskTag
CREATE TABLE TaskTag (
	TaskId UNIQUEIDENTIFIER,
	TagId UNIQUEIDENTIFIER,
    FOREIGN KEY (TaskId) REFERENCES TaskP(Id) ON DELETE SET NULL,
	FOREIGN KEY (TagId) REFERENCES Tag(Id) ON DELETE SET NULL
);

-- Table: Issue
CREATE TABLE Issue (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Type NVARCHAR(50),
    Detail NVARCHAR(255),
    TaskId UNIQUEIDENTIFIER,
    FOREIGN KEY (TaskId) REFERENCES TaskP(Id) ON DELETE CASCADE
);

-- Table: Workflow
CREATE TABLE Workflow (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    OldStatus NVARCHAR(50),
    CurrentStatus NVARCHAR(50),
    NewStatus NVARCHAR(50),
    UpdatedAt DATETIME,
    TaskId UNIQUEIDENTIFIER,
    FOREIGN KEY (TaskId) REFERENCES TaskP(Id) ON DELETE CASCADE
);
