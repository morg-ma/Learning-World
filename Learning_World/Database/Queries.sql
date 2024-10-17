-- For testing the Courses Overview
INSERT INTO Courses 
    (Title, Description, DifficultyLevel, CreationDate, LastUpdateDate, PublicationStatus, MaxEnrollment, Image, AverageRating, Price) 
VALUES  
    ('Introduction to Data Engineering', 'Learn the fundamentals of data engineering, pipelines, and databases.', 'Intermediate', '2024-07-05', '2024-07-20', 'Published', 150, 'data-engineering-course.png', 4.7, 74.99),
    ('AI for Everyone', 'An accessible introduction to the world of AI for non-technical learners.', 'Beginner', '2024-01-15', '2024-02-10', 'Published', 300, 'ai-course.png', 4.9, 39.99),
    ('Cloud Computing with AWS', 'Master cloud services and deployment with Amazon Web Services.', 'Intermediate', '2024-02-20', '2024-03-05', 'Published', 180, 'aws-course.png', 4.8, 99.99),
    ('Blockchain Basics', 'Understand the foundations of blockchain technology and its applications.', 'Beginner', '2024-03-10', '2024-04-01', 'Draft', 100, 'blockchain-course.png', NULL, 69.99),
    ('React.js for Web Development', 'Learn React.js for creating dynamic web applications.', 'Intermediate', '2024-04-15', '2024-05-05', 'Published', 250, 'react-course.png', 4.5, 49.99),
    ('Advanced SQL Techniques', 'Enhance your SQL skills with advanced querying techniques.', 'Advanced', '2024-05-10', '2024-05-25', 'Published', 80, 'sql-course.png', 4.8, 59.99),
    ('UI/UX Design Fundamentals', 'Learn the principles of user interface and user experience design.', 'Beginner', '2024-06-01', '2024-06-20', 'Published', 150, 'uiux-course.png', 4.6, 44.99),
    ('DevOps Essentials', 'Understand the core concepts of DevOps and continuous integration.', 'Intermediate', '2024-07-15', '2024-08-01', 'Draft', 90, 'devops-course.png', NULL, 89.99),
    ('Big Data with Hadoop', 'Learn how to process and analyze big data using Hadoop.', 'Advanced', '2024-08-01', '2024-08-20', 'Published', 120, 'hadoop-course.png', 4.7, 129.99),
    ('Natural Language Processing', 'Explore techniques for processing and understanding human language.', 'Advanced', '2024-09-01', '2024-09-15', 'Published', 100, 'nlp-course.png', 4.9, 119.99);
            --------------------------------------------------------------------------------------------------------------------
-- For testing the My Learning Courses
INSERT INTO Courses 
    (Title, Description, DifficultyLevel, CreationDate, LastUpdateDate, PublicationStatus, MaxEnrollment, Image, AverageRating, Price) 
VALUES  
    ('Introduction to Data Engineering', 'Learn the fundamentals of data engineering, pipelines, and databases.', 'Intermediate', '2024-07-05', '2024-07-20', 'Published', 150, 'data-engineering-course.png', 4.7, 74.99),
    ('AI for Everyone', 'An accessible introduction to the world of AI for non-technical learners.', 'Beginner', '2024-01-15', '2024-02-10', 'Published', 300, 'ai-course.png', 4.9, 39.99);

DECLARE @CourseID INT = SCOPE_IDENTITY();

-- Insert lesson types
INSERT INTO LessonTypes (TypeName) VALUES 
('Video'),
('Text'),
('Quiz');

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
            --------------------------------------------------------------------------------------------------------------------
