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
    Role NVARCHAR(50),
    Status NVARCHAR(50)
);

-- Table: Project
CREATE TABLE Project (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Title NVARCHAR(255),
    Description NVARCHAR(255),
    CreatedAt DATETIME,
    Status NVARCHAR(50)
);

-- Table: ProjectMember
CREATE TABLE ProjectMember (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Role NVARCHAR(50),
    UserId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

-- Table: Feedback
CREATE TABLE Feedback (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Type NVARCHAR(50),
    Detail NVARCHAR(255),
    UserId UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
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

-- Table: EvaluationResult
CREATE TABLE EvaluationResult (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Score INT,
    WorkTrendAnalysis NVARCHAR(255),
    ReviewerComments NVARCHAR(255),
    Strengths NVARCHAR(255),
    AreasForImprovement NVARCHAR(255),
    ProjectMemberId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectMemberId) REFERENCES ProjectMember(Id) ON DELETE CASCADE
);

-- Table: Repository
CREATE TABLE Repository (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255),
    Owner NVARCHAR(255),
    Url NVARCHAR(255),
    ProjectMemberId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectMemberId) REFERENCES ProjectMember(Id) ON DELETE CASCADE
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
    Status NVARCHAR(50),
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);

-- Table: TaskP
CREATE TABLE TaskP (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Status NVARCHAR(50),
	Name NVARCHAR(50),
    Description NVARCHAR(255),
	Priority INT,
    StartDate DATETIME,
    EndDate DATETIME,
    ProjectMemberId UNIQUEIDENTIFIER,
    ProjectId UNIQUEIDENTIFIER,
    FOREIGN KEY (ProjectMemberId) REFERENCES ProjectMember(Id) ON DELETE NO ACTION,
    FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE -- Cascade delete for Project
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

-- Table: TaskSprint
CREATE TABLE TaskSprint (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UpdateStartDate DATETIME,
    UpdatedEndDate DATETIME,
    SprintId UNIQUEIDENTIFIER,
    TaskId UNIQUEIDENTIFIER,
    FOREIGN KEY (SprintId) REFERENCES Sprint(Id) ON DELETE CASCADE,
    FOREIGN KEY (TaskId) REFERENCES TaskP(Id) ON DELETE NO ACTION
);
