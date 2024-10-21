-- Create database
CREATE DATABASE ELearningPlatform;
GO

USE ELearningPlatform;
GO

-- Create Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50), -- New column to store preferred payment method (CreditCard, PayPal, BankTransfer)
    CardName NVARCHAR(100), -- Additional fields for credit card info
    CardNumber NVARCHAR(20),
    ExpiryDate NVARCHAR(10),
    CVC NVARCHAR(4),
    PayPalEmail NVARCHAR(255), -- PayPal email field
    BankAccountNumber NVARCHAR(50), -- Bank transfer information
    BankName NVARCHAR(100),
    Country NVARCHAR(100)
);

-- Create Roles table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

-- Create UserRoles table (for many-to-many relationship between Users and Roles)
CREATE TABLE UserRoles (
    UserID INT,
    RoleID INT,
    PRIMARY KEY (UserID, RoleID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Create Categories table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL UNIQUE
);

-- Create Courses table
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Description NTEXT,
    DifficultyLevel NVARCHAR(20) NOT NULL,
    CreationDate DATETIME NOT NULL DEFAULT GETDATE(),
    LastUpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    PublicationStatus NVARCHAR(20) NOT NULL,
    MaxEnrollment INT,
    AverageRating DECIMAL(3,2)
);

-- Create CourseCategories table (for many-to-many relationship between Courses and Categories)
CREATE TABLE CourseCategories (
    CourseID INT,
    CategoryID INT,
    PRIMARY KEY (CourseID, CategoryID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- Create CourseInstructors table (for many-to-many relationship between Courses and Instructors)
CREATE TABLE CourseInstructors (
    CourseID INT,
    UserID INT,
    PRIMARY KEY (CourseID, UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Create Modules table
CREATE TABLE Modules (
    ModuleID INT PRIMARY KEY IDENTITY(1,1),
    CourseID INT,
    Title NVARCHAR(255) NOT NULL,
    Description NTEXT,
    OrderInCourse INT NOT NULL,
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Create Parts table
CREATE TABLE Parts (
    PartID INT PRIMARY KEY IDENTITY(1,1),
    ModuleID INT,
    Title NVARCHAR(255) NOT NULL,
    Description NTEXT,
    OrderInModule INT NOT NULL,
    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID)
);

-- Create LessonTypes table
CREATE TABLE LessonTypes (
    LessonTypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50) NOT NULL UNIQUE
);

-- Create Lessons table
CREATE TABLE Lessons (
    LessonID INT PRIMARY KEY IDENTITY(1,1),
    PartID INT,
    LessonTypeID INT,
    Title NVARCHAR(255) NOT NULL,
    Description NTEXT,
    OrderInPart INT NOT NULL,
    FOREIGN KEY (PartID) REFERENCES Parts(PartID),
    FOREIGN KEY (LessonTypeID) REFERENCES LessonTypes(LessonTypeID)
);

-- Create LessonVideos table
CREATE TABLE LessonVideos (
    LessonID INT PRIMARY KEY,
    VideoURL NVARCHAR(255) NOT NULL,
    Duration INT NOT NULL, -- Duration in seconds
    FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID)
);

-- Create LessonTexts table
CREATE TABLE LessonTexts (
    LessonID INT PRIMARY KEY,
    Content NTEXT NOT NULL,
    FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID)
);

-- Create LessonQuizzes table
CREATE TABLE LessonQuizzes (
    LessonID INT PRIMARY KEY,
    PassingScore INT NOT NULL,
    FOREIGN KEY (LessonID) REFERENCES Lessons(LessonID)
);

-- Create QuizQuestions table
CREATE TABLE QuizQuestions (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    LessonID INT,
    QuestionText NTEXT NOT NULL,
    FOREIGN KEY (LessonID) REFERENCES LessonQuizzes(LessonID)
);

-- Create QuizAnswers table
CREATE TABLE QuizAnswers (
    AnswerID INT PRIMARY KEY IDENTITY(1,1),
    QuestionID INT,
    AnswerText NTEXT NOT NULL,
    IsCorrect BIT NOT NULL,
    FOREIGN KEY (QuestionID) REFERENCES QuizQuestions(QuestionID)
);

-- Create Enrollments table
CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    CourseID INT,
    EnrollmentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentStatus NVARCHAR(50) NOT NULL, -- Payment status column added
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Create Progresses table
CREATE TABLE Progresses (
    ProgressID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    PartID INT,
    CompletionStatus BIT NOT NULL DEFAULT 0,
    CompletionDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (PartID) REFERENCES Parts(PartID)
);

-- Add a composite unique constraint to ensure a user can't have multiple progress records for the same part
ALTER TABLE Progresses
ADD CONSTRAINT UQ_UserPart UNIQUE (UserID, PartID);

-- Create Transactions table
CREATE TABLE Transactions (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    CourseID INT,
    Amount DECIMAL(10,2) NOT NULL,
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Create Certificates table
CREATE TABLE Certificates (
    CertificateID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    CourseID INT,
    IssueDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Create AdminLogs table
CREATE TABLE AdminLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    AdminID INT,
    ActionType NVARCHAR(100) NOT NULL,
    ActionDescription NTEXT NOT NULL,
    ActionDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AdminID) REFERENCES Users(UserID)
);
