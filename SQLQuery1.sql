CREATE DATABASE PortfolioDB;
GO

USE PortfolioDB;
GO


-- 1. ADMIN


CREATE TABLE Admins
(
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE
);
GO



-- 2. PROFILE


CREATE TABLE Profile
(
    ProfileID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    Avatar VARCHAR(500),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address NVARCHAR(200)
);
GO



-- 3. SKILLS


CREATE TABLE Skills
(
    SkillID INT IDENTITY(1,1) PRIMARY KEY,
    SkillName NVARCHAR(100) NOT NULL,
    SkillType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    Icon VARCHAR(500)
);
GO



-- 4. CATEGORIES


CREATE TABLE Categories
(
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE
);
GO



-- 5. PROJECTS


CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Image VARCHAR(500),
    ProjectURL VARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),

    CategoryID INT NOT NULL,

    CONSTRAINT FK_Projects_Categories
        FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
);
GO



-- 6. TAGS


CREATE TABLE Tags
(
    TagID INT IDENTITY(1,1) PRIMARY KEY,
    TagName NVARCHAR(100) NOT NULL UNIQUE
);
GO



-- 7. PROJECT TAGS

CREATE TABLE ProjectTags
(
    ProjectID INT NOT NULL,
    TagID INT NOT NULL,

    PRIMARY KEY (ProjectID, TagID),

    CONSTRAINT FK_ProjectTags_Projects
        FOREIGN KEY (ProjectID)
        REFERENCES Projects(ProjectID)
        ON DELETE CASCADE,

    CONSTRAINT FK_ProjectTags_Tags
        FOREIGN KEY (TagID)
        REFERENCES Tags(TagID)
        ON DELETE CASCADE
);
GO



--8.Experiences

CREATE TABLE Experiences
(
    ExperienceID INT IDENTITY(1,1) PRIMARY KEY,
    Position NVARCHAR(100) NOT NULL,
    Company NVARCHAR(200),
    StartDate DATE,
    EndDate DATE,
    Description NVARCHAR(MAX)
);
GO



-- 9. EDUCATIONS

CREATE TABLE Educations
(
    EducationID INT IDENTITY(1,1) PRIMARY KEY,
    SchoolName NVARCHAR(200) NOT NULL,
    Degree NVARCHAR(100),
    StartDate DATE,
    EndDate DATE,
    Description NVARCHAR(MAX)
);
GO



-- 10. CERTIFICATES


CREATE TABLE Certificates
(
    CertificateID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Organization NVARCHAR(200),
    IssueDate DATE,
    CertificateURL VARCHAR(500),
    Image VARCHAR(500)
);
GO


--11.Achievements

CREATE TABLE Achievements
(
    AchievementID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    AchievementDate DATE,
    Image VARCHAR(500)
);
GO



-- 12. BLOG / NEWS


CREATE TABLE BlogPosts
(
    PostID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(300) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Thumbnail VARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME,
    Status BIT DEFAULT 1
);
GO


-- 13. CONTACT MESSAGES

CREATE TABLE ContactMessages
(
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    Subject NVARCHAR(200),
    Message NVARCHAR(MAX) NOT NULL,
    SentDate DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0
);
GO

CREATE DATABASE PortfolioDB;
GO

USE PortfolioDB;
GO

-- =============================================
-- 1. ADMIN
-- =============================================

CREATE TABLE Admins
(
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE
);
GO


-- =============================================
-- 2. PROFILE
-- =============================================

CREATE TABLE Profile
(
    ProfileID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Title NVARCHAR(200),
    Description NVARCHAR(MAX),
    Avatar VARCHAR(500),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    Address NVARCHAR(200)
);
GO


-- =============================================
-- 3. SKILLS
-- =============================================

CREATE TABLE Skills
(
    SkillID INT IDENTITY(1,1) PRIMARY KEY,
    SkillName NVARCHAR(100) NOT NULL,
    SkillType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    Icon VARCHAR(500)
);
GO


-- =============================================
-- 4. CATEGORIES
-- =============================================

CREATE TABLE Categories
(
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE
);
GO


-- =============================================
-- 5. PROJECTS
-- =============================================

CREATE TABLE Projects
(
    ProjectID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Image VARCHAR(500),
    ProjectURL VARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),

    CategoryID INT NOT NULL,

    CONSTRAINT FK_Projects_Categories
        FOREIGN KEY (CategoryID)
        REFERENCES Categories(CategoryID)
);
GO


-- =============================================
-- 6. TAGS
-- =============================================

CREATE TABLE Tags
(
    TagID INT IDENTITY(1,1) PRIMARY KEY,
    TagName NVARCHAR(100) NOT NULL UNIQUE
);
GO


-- =============================================
-- 7. PROJECT TAGS
-- Quan hệ Many-to-Many
-- =============================================

CREATE TABLE ProjectTags
(
    ProjectID INT NOT NULL,
    TagID INT NOT NULL,

    PRIMARY KEY (ProjectID, TagID),

    CONSTRAINT FK_ProjectTags_Projects
        FOREIGN KEY (ProjectID)
        REFERENCES Projects(ProjectID)
        ON DELETE CASCADE,

    CONSTRAINT FK_ProjectTags_Tags
        FOREIGN KEY (TagID)
        REFERENCES Tags(TagID)
        ON DELETE CASCADE
);
GO


-- =============================================
-- 8. EXPERIENCES
-- =============================================

CREATE TABLE Experiences
(
    ExperienceID INT IDENTITY(1,1) PRIMARY KEY,
    Position NVARCHAR(100) NOT NULL,
    Company NVARCHAR(200),
    StartDate DATE,
    EndDate DATE,
    Description NVARCHAR(MAX)
);
GO


-- =============================================
-- 9. EDUCATIONS
-- =============================================
CREATE TABLE Educations
(
    EducationID INT IDENTITY(1,1) PRIMARY KEY,
    SchoolName NVARCHAR(200) NOT NULL,
    Degree NVARCHAR(100),
    StartDate DATE,
    EndDate DATE,
    Description NVARCHAR(MAX)
);
GO


-- =============================================
-- 10. CERTIFICATES
-- =============================================

CREATE TABLE Certificates
(
    CertificateID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Organization NVARCHAR(200),
    IssueDate DATE,
    CertificateURL VARCHAR(500),
    Image VARCHAR(500)
);
GO


-- =============================================
-- 11. ACHIEVEMENTS
-- =============================================

CREATE TABLE Achievements
(
    AchievementID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    AchievementDate DATE,
    Image VARCHAR(500)
);
GO


-- =============================================
-- 12. BLOG / NEWS
-- =============================================

CREATE TABLE BlogPosts
(
    PostID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(300) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    Thumbnail VARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME,
    Status BIT DEFAULT 1
);
GO


-- =============================================
-- 13. CONTACT MESSAGES
-- =============================================

CREATE TABLE ContactMessages
(
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email VARCHAR(150) NOT NULL,
    Subject NVARCHAR(200),
    Message NVARCHAR(MAX) NOT NULL,
    SentDate DATETIME DEFAULT GETDATE(),
    IsRead BIT DEFAULT 0
);
GO
INSERT INTO Admins (Username, PasswordHash, Email)
VALUES
('dattrinh', '031206', 'dattrinh12369@gmail.com');

INSERT INTO Profile (FullName, Title, Description, Email)
VALUES
(N'Trịnh Tiến Đạt', N'CSDL', N'Tôi là sinh viên CNTT.', 'dattrinh12369@gmail.com');

INSERT INTO Skills (SkillName, SkillType, Description)
VALUES
(N'C#', N'Programming', N'Lập trình C#'),
(N'HTML', N'Frontend', N'Xây dựng giao diện web'),
(N'SQL Server', N'Database', N'Quản lý cơ sở dữ liệu');

INSERT INTO Categories (CategoryName)
VALUES
(N'Web Development'),
(N'Game Development');

INSERT INTO Tags (TagName)
VALUES
(N'C#'),
(N'ASP.NET MVC'),
(N'SQL Server');
