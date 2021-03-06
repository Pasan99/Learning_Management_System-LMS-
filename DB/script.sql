USE [EastWood]
GO
/****** Object:  Table [dbo].[Assignment]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignment](
	[AssignmentId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentName] [varchar](100) NOT NULL,
	[AssignmentDescription] [varchar](1000) NOT NULL,
	[AssignmentIssuedOn] [datetime] NOT NULL,
	[AssignmentMarksReleaseOn] [datetime] NOT NULL,
	[AssignmentDeadline] [datetime] NOT NULL,
	[AssignmentMaxFileSize] [float] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_Assignment] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignmentUpload]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignmentUpload](
	[AssignmentUploadId] [int] IDENTITY(1,1) NOT NULL,
	[AssignmentUploadSubmittedOn] [datetime] NOT NULL,
	[AssignmentUploadGrade] [varchar](20) NOT NULL,
	[AssignmentUploadMarks] [float] NOT NULL,
	[AssignmentUploadIsAfterDeadline] [bit] NOT NULL,
	[AssignmentId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_AssignmentUpload] PRIMARY KEY CLUSTERED 
(
	[AssignmentUploadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[AttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[AttachmentName] [varchar](100) NOT NULL,
	[AttachmentFileSize] [float] NOT NULL,
	[AttachmentUrl] [varchar](400) NOT NULL,
	[AttachmentReferenceType] [varchar](100) NOT NULL,
	[AttachmentReferenceId] [int] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[AttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coordinator]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coordinator](
	[CoordinatorId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Coordinator] PRIMARY KEY CLUSTERED 
(
	[CoordinatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](100) NOT NULL,
	[CourseDescription] [varchar](1400) NOT NULL,
	[CourseStartsOn] [datetime] NOT NULL,
	[CourseEndsOn] [datetime] NOT NULL,
	[CoordinatorId] [int] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureNote]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureNote](
	[LectureNoteid] [int] IDENTITY(1,1) NOT NULL,
	[LectureNoteName] [varchar](100) NOT NULL,
	[LectureNoteDescription] [varchar](1000) NOT NULL,
	[LectureNoteSubmittedOn] [datetime] NOT NULL,
	[LectureNoteDate] [datetime] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_LectureNote] PRIMARY KEY CLUSTERED 
(
	[LectureNoteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lecturer]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lecturer](
	[LecturerId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Lecturer] PRIMARY KEY CLUSTERED 
(
	[LecturerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semester]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semester](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterStartDate] [datetime] NOT NULL,
	[SemesterEndDate] [datetime] NOT NULL,
	[SemesterName] [varchar](100) NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SemesterResult]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterResult](
	[SemesterResultId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterResultMarks] [float] NOT NULL,
	[SemesterResultGrade] [varchar](20) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_SemesterResult] PRIMARY KEY CLUSTERED 
(
	[SemesterResultId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentCourse]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentCourse](
	[StudentCourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_StudentCourse] PRIMARY KEY CLUSTERED 
(
	[StudentCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentSubject]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentSubject](
	[StudentSubjectId] [int] IDENTITY(1,1) NOT NULL,
	[StudentSubjectMarks] [float] NOT NULL,
	[StudentSubjectGrade] [varchar](20) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_StudentSubject] PRIMARY KEY CLUSTERED 
(
	[StudentSubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyMaterial]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyMaterial](
	[StudyMaterialId] [int] IDENTITY(1,1) NOT NULL,
	[StudyMaterialName] [varchar](100) NOT NULL,
	[StudyMaterialDescription] [varchar](1000) NOT NULL,
	[StudyMaterialSubmittedOn] [datetime] NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_StudyMaterial] PRIMARY KEY CLUSTERED 
(
	[StudyMaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [varchar](100) NOT NULL,
	[SubjectDescription] [varchar](1000) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[LecturerId] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/13/2021 12:06:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserFirstName] [varchar](100) NOT NULL,
	[UserLastName] [varchar](100) NOT NULL,
	[UserPassword] [varchar](150) NOT NULL,
	[UserBio] [varchar](1000) NOT NULL,
	[UserLastLoggedIn] [datetime] NOT NULL,
	[UserEmail] [varchar](250) NOT NULL,
	[UserContactNo] [varchar](10) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Assignment] ON 

INSERT [dbo].[Assignment] ([AssignmentId], [AssignmentName], [AssignmentDescription], [AssignmentIssuedOn], [AssignmentMarksReleaseOn], [AssignmentDeadline], [AssignmentMaxFileSize], [SubjectId]) VALUES (1, N'Course Work 01', N'This is a half-page paragraph in narrative form that describes the assignment purpose, background, main elements, expectations, required format/length, and points possible. Many professors stop at the assignment description paragraph and expect students to be able to pull out the requirements.', CAST(N'2021-01-12T08:04:09.923' AS DateTime), CAST(N'2021-01-12T08:04:09.923' AS DateTime), CAST(N'2021-02-23T08:04:00.000' AS DateTime), 2000000000, 2)
INSERT [dbo].[Assignment] ([AssignmentId], [AssignmentName], [AssignmentDescription], [AssignmentIssuedOn], [AssignmentMarksReleaseOn], [AssignmentDeadline], [AssignmentMaxFileSize], [SubjectId]) VALUES (2, N'Weather App', N'Create weather app using APIs and google maps', CAST(N'2021-01-12T09:10:34.757' AS DateTime), CAST(N'2021-03-25T09:10:00.000' AS DateTime), CAST(N'2021-01-28T09:10:00.000' AS DateTime), 500000, 3)
INSERT [dbo].[Assignment] ([AssignmentId], [AssignmentName], [AssignmentDescription], [AssignmentIssuedOn], [AssignmentMarksReleaseOn], [AssignmentDeadline], [AssignmentMaxFileSize], [SubjectId]) VALUES (3, N'Create Design Diagram', N'Creating networiking diagram', CAST(N'2021-01-13T11:33:40.290' AS DateTime), CAST(N'2021-03-25T11:33:00.000' AS DateTime), CAST(N'2021-01-15T11:33:00.000' AS DateTime), 928349382, 8)
SET IDENTITY_INSERT [dbo].[Assignment] OFF
SET IDENTITY_INSERT [dbo].[AssignmentUpload] ON 

INSERT [dbo].[AssignmentUpload] ([AssignmentUploadId], [AssignmentUploadSubmittedOn], [AssignmentUploadGrade], [AssignmentUploadMarks], [AssignmentUploadIsAfterDeadline], [AssignmentId], [StudentId]) VALUES (4, CAST(N'2021-01-13T09:12:39.340' AS DateTime), N'Distinction', 90, 0, 2, 1)
INSERT [dbo].[AssignmentUpload] ([AssignmentUploadId], [AssignmentUploadSubmittedOn], [AssignmentUploadGrade], [AssignmentUploadMarks], [AssignmentUploadIsAfterDeadline], [AssignmentId], [StudentId]) VALUES (6, CAST(N'2021-01-13T10:04:14.200' AS DateTime), N'Merit', 60, 0, 1, 1)
INSERT [dbo].[AssignmentUpload] ([AssignmentUploadId], [AssignmentUploadSubmittedOn], [AssignmentUploadGrade], [AssignmentUploadMarks], [AssignmentUploadIsAfterDeadline], [AssignmentId], [StudentId]) VALUES (7, CAST(N'2021-01-13T11:17:18.370' AS DateTime), N'Distinction', 75, 0, 2, 2)
INSERT [dbo].[AssignmentUpload] ([AssignmentUploadId], [AssignmentUploadSubmittedOn], [AssignmentUploadGrade], [AssignmentUploadMarks], [AssignmentUploadIsAfterDeadline], [AssignmentId], [StudentId]) VALUES (8, CAST(N'2021-01-13T11:29:19.030' AS DateTime), N'Not Marked', 0, 0, 1, 2)
SET IDENTITY_INSERT [dbo].[AssignmentUpload] OFF
SET IDENTITY_INSERT [dbo].[Attachment] ON 

INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (1, N'Course Work 01_0.sql', 444, N'/Uploads/Course Work 01_0.sql', N'Assignment', 1)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (2, N'Course Work 01_1.zip', 2253029, N'/Uploads/Course Work 01_1.zip', N'Assignment', 1)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (3, N'Weather App_0.zip', 4718907, N'/Uploads/Weather App_0.zip', N'Assignment', 2)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (4, N'Weather App_upload_auid_4_0.sql', 444, N'/Uploads/Weather App_upload_auid_4_0.sql', N'Assignment Uploads', 4)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (5, N'Weather App_upload_auid_5_0.webp', 13318, N'/Uploads/Weather App_upload_auid_5_0.webp', N'Assignment Uploads', 5)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (6, N'Day 01 - Lecture Note_0.JPG', 6534749, N'/Uploads/Day 01 - Lecture Note_0.JPG', N'Lecture Note', 3)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (7, N'Programming Fundamentals_0.sql', 444, N'/Uploads/Programming Fundamentals_0.sql', N'Lecture Note', 4)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (8, N'Introduction to C language_0.png', 68645, N'/Uploads/Introduction to C language_0.png', N'Lecture Note', 5)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (9, N'Java Object Oriented Concepts_0.sql', 276, N'/Uploads/Java Object Oriented Concepts_0.sql', N'Lecture Note', 6)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (10, N'Cross platform with Flutter_0.sql', 1566, N'/Uploads/Cross platform with Flutter_0.sql', N'Lecture Note', 7)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (11, N'Weather App_upload_auid_4_1.sql', 444, N'/Uploads/Weather App_upload_auid_4_1.sql', N'Assignment Uploads', 4)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (12, N'Course Work 01_upload_auid_6_0.JPG', 6534749, N'/Uploads/Course Work 01_upload_auid_6_0.JPG', N'Assignment Uploads', 6)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (13, N'Weather App_upload_auid_7_0.sql', 1566, N'/Uploads/Weather App_upload_auid_7_0.sql', N'Assignment Uploads', 7)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (14, N'Course Work 01_upload_auid_8_0.zip', 2253029, N'/Uploads/Course Work 01_upload_auid_8_0.zip', N'Assignment Uploads', 8)
INSERT [dbo].[Attachment] ([AttachmentId], [AttachmentName], [AttachmentFileSize], [AttachmentUrl], [AttachmentReferenceType], [AttachmentReferenceId]) VALUES (15, N'Create Design Diagram_0.pdf', 1054829, N'/Uploads/Create Design Diagram_0.pdf', N'Assignment', 3)
SET IDENTITY_INSERT [dbo].[Attachment] OFF
SET IDENTITY_INSERT [dbo].[Coordinator] ON 

INSERT [dbo].[Coordinator] ([CoordinatorId], [UserId]) VALUES (1, 2)
INSERT [dbo].[Coordinator] ([CoordinatorId], [UserId]) VALUES (2, 4)
INSERT [dbo].[Coordinator] ([CoordinatorId], [UserId]) VALUES (3, 5)
INSERT [dbo].[Coordinator] ([CoordinatorId], [UserId]) VALUES (4, 6)
SET IDENTITY_INSERT [dbo].[Coordinator] OFF
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([CourseId], [CourseName], [CourseDescription], [CourseStartsOn], [CourseEndsOn], [CoordinatorId]) VALUES (1, N'Software Engineering', N'Software engineering is the discipline of designing, creating and maintaining software by applying technologies and practices from computer science, project management, engineering, application domains, interface design, digital assets management and other fields.', CAST(N'2021-01-13T00:00:00.000' AS DateTime), CAST(N'2021-01-21T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Course] ([CourseId], [CourseName], [CourseDescription], [CourseStartsOn], [CourseEndsOn], [CoordinatorId]) VALUES (2, N'Photography And Videography', N'Street Photography. Photographic Society of Sri Lanka - PSSL. Certificate in Photography. Academy of Photography. National Diploma in Digital Photography. Postgraduate Diploma in Photography. Diploma in Digital Photography. Photography. Advanced Certificate in Photography. Certificate in Digital Photography.', CAST(N'2021-01-01T12:58:00.000' AS DateTime), CAST(N'2022-12-12T12:58:00.000' AS DateTime), 1)
INSERT [dbo].[Course] ([CourseId], [CourseName], [CourseDescription], [CourseStartsOn], [CourseEndsOn], [CoordinatorId]) VALUES (3, N'Bsc in Computer Networking', N'A computer network is a system in which multiple computers are connected to each other to share information and resources. Computer Networks', CAST(N'2021-01-08T10:24:00.000' AS DateTime), CAST(N'2023-01-13T10:24:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Course] OFF
SET IDENTITY_INSERT [dbo].[LectureNote] ON 

INSERT [dbo].[LectureNote] ([LectureNoteid], [LectureNoteName], [LectureNoteDescription], [LectureNoteSubmittedOn], [LectureNoteDate], [SubjectId]) VALUES (3, N'Day 01 - Lecture Note', N'Introduction ', CAST(N'2021-01-12T13:39:34.197' AS DateTime), CAST(N'2021-01-15T13:38:00.000' AS DateTime), 3)
INSERT [dbo].[LectureNote] ([LectureNoteid], [LectureNoteName], [LectureNoteDescription], [LectureNoteSubmittedOn], [LectureNoteDate], [SubjectId]) VALUES (4, N'Programming Fundamentals', N'Introduction ', CAST(N'2021-01-12T13:49:15.673' AS DateTime), CAST(N'2021-01-14T13:49:00.000' AS DateTime), 2)
INSERT [dbo].[LectureNote] ([LectureNoteid], [LectureNoteName], [LectureNoteDescription], [LectureNoteSubmittedOn], [LectureNoteDate], [SubjectId]) VALUES (5, N'Introduction to C language', N'Description', CAST(N'2021-01-12T13:49:47.780' AS DateTime), CAST(N'2021-01-19T13:49:00.000' AS DateTime), 2)
INSERT [dbo].[LectureNote] ([LectureNoteid], [LectureNoteName], [LectureNoteDescription], [LectureNoteSubmittedOn], [LectureNoteDate], [SubjectId]) VALUES (6, N'Java Object Oriented Concepts', N'OOP Concepts', CAST(N'2021-01-12T13:50:21.457' AS DateTime), CAST(N'2021-01-26T13:50:00.000' AS DateTime), 2)
INSERT [dbo].[LectureNote] ([LectureNoteid], [LectureNoteName], [LectureNoteDescription], [LectureNoteSubmittedOn], [LectureNoteDate], [SubjectId]) VALUES (7, N'Cross platform with Flutter', N'Introduction ', CAST(N'2021-01-12T13:51:05.110' AS DateTime), CAST(N'2021-01-28T13:50:00.000' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[LectureNote] OFF
SET IDENTITY_INSERT [dbo].[Lecturer] ON 

INSERT [dbo].[Lecturer] ([LecturerId], [UserId]) VALUES (1, 7)
INSERT [dbo].[Lecturer] ([LecturerId], [UserId]) VALUES (2, 10)
INSERT [dbo].[Lecturer] ([LecturerId], [UserId]) VALUES (3, 11)
INSERT [dbo].[Lecturer] ([LecturerId], [UserId]) VALUES (4, 13)
INSERT [dbo].[Lecturer] ([LecturerId], [UserId]) VALUES (5, 14)
SET IDENTITY_INSERT [dbo].[Lecturer] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Student')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Coordinator')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'Lecturer')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (4, N'Manager')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Semester] ON 

INSERT [dbo].[Semester] ([SemesterId], [SemesterStartDate], [SemesterEndDate], [SemesterName], [CourseId]) VALUES (2, CAST(N'2021-01-11T00:00:00.000' AS DateTime), CAST(N'2021-04-11T00:00:00.000' AS DateTime), N'1st Semester', 1)
INSERT [dbo].[Semester] ([SemesterId], [SemesterStartDate], [SemesterEndDate], [SemesterName], [CourseId]) VALUES (3, CAST(N'2021-05-01T00:00:00.000' AS DateTime), CAST(N'2021-08-08T00:00:00.000' AS DateTime), N'2nd Semester', 1)
INSERT [dbo].[Semester] ([SemesterId], [SemesterStartDate], [SemesterEndDate], [SemesterName], [CourseId]) VALUES (4, CAST(N'2022-01-12T00:00:00.000' AS DateTime), CAST(N'2022-05-12T00:00:00.000' AS DateTime), N'3rd Semester', 1)
INSERT [dbo].[Semester] ([SemesterId], [SemesterStartDate], [SemesterEndDate], [SemesterName], [CourseId]) VALUES (5, CAST(N'2021-01-03T00:00:00.000' AS DateTime), CAST(N'2021-08-26T00:00:00.000' AS DateTime), N'Fundamental Photography', 2)
INSERT [dbo].[Semester] ([SemesterId], [SemesterStartDate], [SemesterEndDate], [SemesterName], [CourseId]) VALUES (6, CAST(N'2021-09-01T00:00:00.000' AS DateTime), CAST(N'2022-11-12T00:00:00.000' AS DateTime), N'Fundamental Videography', 2)
SET IDENTITY_INSERT [dbo].[Semester] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentId], [UserId]) VALUES (1, 12)
INSERT [dbo].[Student] ([StudentId], [UserId]) VALUES (2, 15)
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[StudentCourse] ON 

INSERT [dbo].[StudentCourse] ([StudentCourseId], [CourseId], [StudentId]) VALUES (1, 1, 1)
INSERT [dbo].[StudentCourse] ([StudentCourseId], [CourseId], [StudentId]) VALUES (2, 2, 1)
INSERT [dbo].[StudentCourse] ([StudentCourseId], [CourseId], [StudentId]) VALUES (3, 1, 2)
SET IDENTITY_INSERT [dbo].[StudentCourse] OFF
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (2, N'Programming 01', N'ABC', 2, 3)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (3, N'Mobile Application Development Beginner', N'This class only teach the fundamentals in android development', 2, 1)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (5, N'System Analysis & Design', N'Creating SRS & UML Diagrams', 2, 2)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (6, N'System Analysis & Design 2', N'Creating SRS & UML Diagrams', 3, 2)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (7, N'Software Design Patterns', N'This class only teach the fundamentals in android development', 3, 1)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (8, N'Computer Networks', N'Best netwroking techs', 3, 1)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (9, N'Final Year Project', N'Research is okay', 4, 1)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (10, N'Street Photography', N'Since each and every industry needs a skillful photographer, we as a leading academic institute in Sri Lanka decided to mak', 5, 1)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (11, N'Graphic Design', N'Graphic design is a craft where professionals create visual content to communicate messages. By applying visual hierarchy and page layout techniques, designers use typography and pictures to meet users'' specific needs and focus on the logic of displaying elements in interactive designs, to optimize the user experience.', 5, 4)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (12, N'After Effects', N'Adobe After Effects is a digital visual effects, motion graphics, and compositing application developed by Adobe Systems and used in the post-production process of film making', 6, 5)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (13, N'Adobe Primier Pro', N'Adobe Premiere Pro is a timeline-based video editing software application developed by Adobe Systems and published', 6, 4)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (14, N'Graphic Design', N'This class only teach the fundamentals in android development', 2, 4)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [SubjectDescription], [SemesterId], [LecturerId]) VALUES (15, N'Database Systems', N'This class only teach the fundamentals in android development', 2, 3)
SET IDENTITY_INSERT [dbo].[Subject] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (2, N'Ama', N'Kulathilake', N'12345', N'Hello', CAST(N'2021-01-10T00:00:00.000' AS DateTime), N'ama@gmail.com', N'0719283883', 2)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (4, N'Thilak', N'De Silva', N'12345', N'Hello', CAST(N'2021-01-10T00:00:00.000' AS DateTime), N'thilak@gmail.com', N'0718283838', 2)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (5, N'Ryan', N'Tendk', N'12345', N'Hello', CAST(N'2021-01-10T00:00:00.000' AS DateTime), N'ryan@gmail.com', N'0778282281', 2)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (6, N'Pasan', N'Manohara', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'My Self', CAST(N'2021-01-11T09:00:10.167' AS DateTime), N'pasanmanohara2000@gmail.com', N'0715634989', 4)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (7, N'Rubbel', N'Perera', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'fdafd', CAST(N'2021-01-10T00:00:00.000' AS DateTime), N'rubbel@gmail.com', N'2311111113', 3)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (10, N'Domashi', N'Ravihari', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'afds', CAST(N'2021-01-10T00:00:00.000' AS DateTime), N'domashi@gmail.com', N'2324332432', 3)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (11, N'Ms. Vishmi', N'Ekanayake', N'E30AC0DB05C4CF869D89E12529ACAD018A6051562F04F18FAC825C053BBE1B3C', N'About', CAST(N'2021-01-12T08:22:50.650' AS DateTime), N'vishmi@gmail.com', N'0715634089', 3)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (12, N'Ilmee', N'Vijeysekara', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'About', CAST(N'2021-01-12T09:26:35.913' AS DateTime), N'ilmee@gmail.com', N'0789374892', 1)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (13, N'Peter', N'Mackinon', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'About', CAST(N'2021-01-12T13:00:42.490' AS DateTime), N'peter@gmail.com', N'0715634089', 3)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (14, N'Matti', N'Hapodja', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'About', CAST(N'2021-01-12T13:01:05.003' AS DateTime), N'matti@gmail.com', N'0715634089', 3)
INSERT [dbo].[User] ([UserId], [UserFirstName], [UserLastName], [UserPassword], [UserBio], [UserLastLoggedIn], [UserEmail], [UserContactNo], [RoleId]) VALUES (15, N'Demo', N'Student', N'5994471ABB01112AFCC18159F6CC74B4F511B99806DA59B3CAF5A9C173CACFC5', N'About', CAST(N'2021-01-13T10:42:19.840' AS DateTime), N'student@gmail.com', N'0715634089', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Assignment]  WITH CHECK ADD  CONSTRAINT [FK_Assignment_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Assignment] CHECK CONSTRAINT [FK_Assignment_Subject]
GO
ALTER TABLE [dbo].[AssignmentUpload]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentUpload_Assignment] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignment] ([AssignmentId])
GO
ALTER TABLE [dbo].[AssignmentUpload] CHECK CONSTRAINT [FK_AssignmentUpload_Assignment]
GO
ALTER TABLE [dbo].[AssignmentUpload]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentUpload_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[AssignmentUpload] CHECK CONSTRAINT [FK_AssignmentUpload_Student]
GO
ALTER TABLE [dbo].[Coordinator]  WITH CHECK ADD  CONSTRAINT [FK_Coordinator_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Coordinator] CHECK CONSTRAINT [FK_Coordinator_User]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Coordinator] FOREIGN KEY([CoordinatorId])
REFERENCES [dbo].[Coordinator] ([CoordinatorId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Coordinator]
GO
ALTER TABLE [dbo].[LectureNote]  WITH CHECK ADD  CONSTRAINT [FK_LectureNote_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[LectureNote] CHECK CONSTRAINT [FK_LectureNote_Subject]
GO
ALTER TABLE [dbo].[Lecturer]  WITH CHECK ADD  CONSTRAINT [FK_Lecturer_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Lecturer] CHECK CONSTRAINT [FK_Lecturer_User]
GO
ALTER TABLE [dbo].[Semester]  WITH CHECK ADD  CONSTRAINT [FK_Semester_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Semester] CHECK CONSTRAINT [FK_Semester_Course]
GO
ALTER TABLE [dbo].[SemesterResult]  WITH CHECK ADD  CONSTRAINT [FK_SemesterResult_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([SemesterId])
GO
ALTER TABLE [dbo].[SemesterResult] CHECK CONSTRAINT [FK_SemesterResult_Semester]
GO
ALTER TABLE [dbo].[SemesterResult]  WITH CHECK ADD  CONSTRAINT [FK_SemesterResult_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[SemesterResult] CHECK CONSTRAINT [FK_SemesterResult_Student]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_User]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_Course]
GO
ALTER TABLE [dbo].[StudentCourse]  WITH CHECK ADD  CONSTRAINT [FK_StudentCourse_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentCourse] CHECK CONSTRAINT [FK_StudentCourse_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Student]
GO
ALTER TABLE [dbo].[StudentSubject]  WITH CHECK ADD  CONSTRAINT [FK_StudentSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[StudentSubject] CHECK CONSTRAINT [FK_StudentSubject_Subject]
GO
ALTER TABLE [dbo].[StudyMaterial]  WITH CHECK ADD  CONSTRAINT [FK_StudyMaterial_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[StudyMaterial] CHECK CONSTRAINT [FK_StudyMaterial_Subject]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Lecturer] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[Lecturer] ([LecturerId])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Lecturer]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Semester] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semester] ([SemesterId])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Semester]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [EastWood] SET  READ_WRITE 
GO
