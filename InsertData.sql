INSERT INTO Courses (Title, Description, DifficultyLevel, PublicationStatus)
VALUES ('Introduction to Programming', 'A comprehensive course for beginners to learn programming basics', 'Beginner', 'Published');
DECLARE @CourseID INT = SCOPE_IDENTITY();

-- Insert lesson types
INSERT INTO LessonTypes (TypeName) VALUES 
('Video'),
('Text'),
('Quiz');

-- Insert roles
INSERT INTO Roles (RoleName) VALUES 
('Student'),
('Instructor'),
('Admin');

-- Insert users
INSERT INTO Users (Name, Email, PasswordHash) VALUES
('John Doe', 'john.doe@example.com', 'hashed_password_1'),
('Jane Smith', 'jane.smith@example.com', 'hashed_password_2'),
('Admin User', 'admin@example.com', 'hashed_password_3');

-- Assign roles to users
INSERT INTO UserRoles (UserID, RoleID)
VALUES
(1, 1), -- John Doe as Student
(2, 2), -- Jane Smith as Instructor
(3, 3); -- Admin User as Admin

-- Assign instructor to the course
INSERT INTO CourseInstructors (CourseID, UserID)
VALUES (@CourseID, 2); -- Assigning Jane Smith as the instructor for the course

-- Insert modules, parts, and lessons
DECLARE @ModuleCounter INT = 1;
WHILE @ModuleCounter <= 3
BEGIN
    -- Insert module
    INSERT INTO Modules (CourseID, Title, Description, OrderInCourse)
    VALUES (@CourseID, 'Module ' + CAST(@ModuleCounter AS NVARCHAR), 'Description for Module ' + CAST(@ModuleCounter AS NVARCHAR), @ModuleCounter);
    
    DECLARE @ModuleID INT = SCOPE_IDENTITY();
    
    DECLARE @PartCounter INT = 1;
    WHILE @PartCounter <= 3
    BEGIN
        -- Insert part
        INSERT INTO Parts (ModuleID, Title, Description, OrderInModule)
        VALUES (@ModuleID, 'Part ' + CAST(@PartCounter AS NVARCHAR), 'Description for Part ' + CAST(@PartCounter AS NVARCHAR), @PartCounter);
        
        DECLARE @PartID INT = SCOPE_IDENTITY();
        
        DECLARE @LessonCounter INT = 1;
        WHILE @LessonCounter <= 2
        BEGIN
            -- Determine lesson type (alternate between Video, Text, and Quiz)
            DECLARE @LessonTypeID INT = ((@LessonCounter + @PartCounter + @ModuleCounter) % 3) + 1;
            
            -- Insert lesson
            INSERT INTO Lessons (PartID, LessonTypeID, Title, Description, OrderInPart)
            VALUES (@PartID, @LessonTypeID, 'Lesson ' + CAST(@LessonCounter AS NVARCHAR), 'Description for Lesson ' + CAST(@LessonCounter AS NVARCHAR), @LessonCounter);
            
            DECLARE @LessonID INT = SCOPE_IDENTITY();
            
            -- Insert lesson content based on type
            IF @LessonTypeID = 1 -- Video
            BEGIN
                INSERT INTO LessonVideos (LessonID, VideoURL, Duration)
                VALUES (@LessonID, 'https://example.com/video' + CAST(@LessonID AS NVARCHAR) + '.mp4', 600);
            END
            ELSE IF @LessonTypeID = 2 -- Text
            BEGIN
                INSERT INTO LessonTexts (LessonID, Content)
                VALUES (@LessonID, 'This is the content for text lesson ' + CAST(@LessonID AS NVARCHAR));
            END
            ELSE IF @LessonTypeID = 3 -- Quiz
            BEGIN
                INSERT INTO LessonQuizzes (LessonID, PassingScore)
                VALUES (@LessonID, 70);
                
                -- Insert sample quiz questions and answers
                INSERT INTO QuizQuestions (LessonID, QuestionText)
                VALUES (@LessonID, 'Sample question 1 for lesson ' + CAST(@LessonID AS NVARCHAR));
                
                DECLARE @QuestionID INT = SCOPE_IDENTITY();
                
                INSERT INTO QuizAnswers (QuestionID, AnswerText, IsCorrect)
                VALUES 
                (@QuestionID, 'Answer 1', 1),
                (@QuestionID, 'Answer 2', 0),
                (@QuestionID, 'Answer 3', 0);
            END
            
            SET @LessonCounter = @LessonCounter + 1;
        END
        
        SET @PartCounter = @PartCounter + 1;
    END
    
    SET @ModuleCounter = @ModuleCounter + 1;
END
select * from Courses;
select * from Modules;
select * from Parts;
select * from Lessons;
select * from LessonTypes;
select * from LessonVideos;
select * from Users;
select * from Roles;
select * from UserRoles;
select * from CourseInstructors;
