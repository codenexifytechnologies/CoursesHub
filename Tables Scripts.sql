CREATE TABLE [dbo].[MenuMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MainMenu] [varchar](max) NULL,
	[SubMenu] [varchar](max) NULL,
	[IsVisible] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](500) NULL,
	[isActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_UserRegistration](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](500) NULL,
	[emailId] [varchar](500) NULL,
	[password] [varchar](500) NULL,
	[mobileNo] [varchar](100) NULL,
	[isActive] [int] NULL,
	[role] [varchar](500) NULL,
	[createdDate] [datetime] NULL,
	[modifiedDate] [datetime] NULL,
	[ImagePath] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[UserMenuMapping](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NULL,
	[LoginId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_Blogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [varchar](max) NULL,
	[OnelinerDiscription] [varchar](max) NULL,
	[Quote] [varchar](max) NULL,
	[FullDiscription] [varchar](max) NULL,
	[SubTitle] [varchar](max) NULL,
	[SubTitleDiscription] [varchar](max) NULL,
	[BlogThumbnail] [varchar](max) NULL,
	[BlogSmallThumbnail1] [varchar](max) NULL,
	[BlogSmallThumbnail2] [varchar](max) NULL,
	[BlogSmallThumbnail3] [varchar](max) NULL,
	[BlogVerified] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
	[ModifyDate] [datetime] NULL,
	[ModifyBy] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_category_master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryValue] [varchar](100) NULL,
	[CategoryType] [varchar](100) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ImagePath] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_ContactUs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[Email] [varchar](max) NULL,
	[Subject] [varchar](max) NULL,
	[Message] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_CourseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniversityId] [int] NULL,
	[RTO_ID] [varchar](500) NULL,
	[CourseName] [varchar](500) NULL,
	[CourseProvider] [varchar](500) NULL,
	[AboutCourse] [varchar](1000) NULL,
	[StudyMethod] [varchar](500) NULL,
	[CourseCategory] [varchar](500) NULL,
	[CoursePhoto] [varchar](500) NULL,
	[CourseVideo] [varchar](500) NULL,
	[CourseStartDate] [varchar](50) NULL,
	[CourseDuration] [varchar](50) NULL,
	[CourseOverview] [varchar](1000) NULL,
	[CourseOutcomes] [varchar](1000) NULL,
	[CourseLearn] [varchar](1000) NULL,
	[CourseRequirements] [varchar](1000) NULL,
	[WorkPlacement] [varchar](1000) NULL,
	[RPL] [varchar](1000) NULL,
	[CourseGuide] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [int] NULL,
	[isReject] [int] NULL,
	[RejectionReason] [varchar](max) NULL,
	[CourseVideo2] [varchar](max) NULL,
	[CourseVideo3] [varchar](max) NULL,
	[TrendingCourse] [varchar](max) NULL,
	[CourseLevel] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_CourseDetails_archive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NULL,
	[UniversityId] [int] NULL,
	[RTO_ID] [varchar](500) NULL,
	[CourseName] [varchar](500) NULL,
	[CourseProvider] [varchar](500) NULL,
	[AboutCourse] [varchar](1000) NULL,
	[StudyMethod] [varchar](500) NULL,
	[CourseCategory] [varchar](500) NULL,
	[CoursePhoto] [varchar](500) NULL,
	[CourseVideo] [varchar](500) NULL,
	[CourseStartDate] [varchar](50) NULL,
	[CourseDuration] [varchar](50) NULL,
	[CourseOverview] [varchar](1000) NULL,
	[CourseOutcomes] [varchar](1000) NULL,
	[CourseLearn] [varchar](1000) NULL,
	[CourseRequirements] [varchar](1000) NULL,
	[WorkPlacement] [varchar](1000) NULL,
	[RPL] [varchar](1000) NULL,
	[CourseGuide] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [int] NULL,
	[isReject] [int] NULL,
	[RejectionReason] [varchar](max) NULL,
	[CourseVideo2] [varchar](max) NULL,
	[CourseVideo3] [varchar](max) NULL,
	[TrendingCourse] [varchar](max) NULL,
	[CourseLevel] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_CourseLevel_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseLevel] [varchar](max) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_CourseTestimonial](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Course_Id] [int] NULL,
	[Person_Name] [varchar](500) NULL,
	[Designation] [varchar](500) NULL,
	[Comment] [varchar](5000) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Student_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_CourseTestimonial_Archive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestimonialId] [int] NULL,
	[Course_Id] [int] NULL,
	[Person_Name] [varchar](500) NULL,
	[Designation] [varchar](500) NULL,
	[Comment] [varchar](5000) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Student_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_Lead](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Course_ID] [int] NULL,
	[Student_ID] [int] NULL,
	[University_ID] [int] NULL,
	[isEmailSent] [int] NULL,
	[EmailSentOn] [datetime] NULL,
	[createddate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_Premium_Lead](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Course_ID] [int] NULL,
	[Student_ID] [int] NULL,
	[University_ID] [int] NULL,
	[isEmailSent] [int] NULL,
	[EmailSentOn] [datetime] NULL,
	[createddate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_QualificationLevel_master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QualificationType] [varchar](max) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_student_details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](500) NULL,
	[MiddleName] [varchar](500) NULL,
	[LastName] [varchar](500) NULL,
	[EmailId] [varchar](500) NULL,
	[Mobile] [varchar](20) NULL,
	[Password] [varchar](50) NULL,
	[IsActive] [int] NULL,
	[CreateDate] [datetime] NULL,
	[isAustraliaResident] [int] NULL,
	[Zipcode] [varchar](5000) NULL,
	[State] [varchar](5000) NULL,
	[Country] [varchar](5000) NULL,
	[VerifyLink] [varchar](1000) NULL,
	[Area] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
CREATE TABLE [dbo].[tbl_TrendingCourses_Master](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](max) NULL,
	[IsActive] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_university_details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RTO_ID] [varchar](500) NULL,
	[RTO_Status] [int] NULL,
	[CRICOS_Code] [varchar](500) NULL,
	[Legal_Name_Organisation] [varchar](500) NULL,
	[Trading_Name] [varchar](500) NULL,
	[ABN_ACN] [varchar](50) NULL,
	[Business_Address] [varchar](500) NULL,
	[Postal_Address] [varchar](500) NULL,
	[Website] [varchar](500) NULL,
	[Contact_Number] [varchar](50) NULL,
	[General_Contact_Email] [varchar](50) NULL,
	[Full_Name] [varchar](500) NULL,
	[Position] [varchar](500) NULL,
	[Direct_Phone] [varchar](50) NULL,
	[Email] [varchar](500) NULL,
	[Relevant_Courses] [varchar](500) NULL,
	[Signature] [varchar](500) NULL,
	[Logo] [varchar](500) NULL,
	[Password] [varchar](50) NULL,
	[IsActive] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ZipCode] [varchar](max) NULL,
	[State] [varchar](max) NULL,
	[IsReject] [int] NULL,
	[RejectionReason] [varchar](500) NULL,
	[isUniversityActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
CREATE TABLE [dbo].[tbl_university_details_archive](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnivercityId] [int] NULL,
	[RTO_ID] [varchar](500) NULL,
	[RTO_Status] [int] NULL,
	[CRICOS_Code] [varchar](500) NULL,
	[Legal_Name_Organisation] [varchar](500) NULL,
	[Trading_Name] [varchar](500) NULL,
	[ABN_ACN] [varchar](50) NULL,
	[Business_Address] [varchar](500) NULL,
	[Postal_Address] [varchar](500) NULL,
	[Website] [varchar](500) NULL,
	[Contact_Number] [varchar](50) NULL,
	[General_Contact_Email] [varchar](50) NULL,
	[Full_Name] [varchar](500) NULL,
	[Position] [varchar](500) NULL,
	[Direct_Phone] [varchar](50) NULL,
	[Email] [varchar](500) NULL,
	[Relevant_Courses] [varchar](500) NULL,
	[Signature] [varchar](500) NULL,
	[Logo] [varchar](500) NULL,
	[Password] [varchar](50) NULL,
	[IsActive] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ZipCode] [varchar](max) NULL,
	[State] [varchar](max) NULL,
	[IsReject] [int] NULL,
	[RejectionReason] [varchar](500) NULL,
	[isUniversityActive] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[MenuMaster] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[MenuMaster] ADD  DEFAULT (NULL) FOR [ModifiedDate]
ALTER TABLE [dbo].[tbl_Role] ADD  DEFAULT ((1)) FOR [isActive]
ALTER TABLE [dbo].[tbl_Role] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[UserMenuMapping] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[UserMenuMapping] ADD  DEFAULT (NULL) FOR [ModifiedDate]
ALTER TABLE [dbo].[tbl_Blogs] ADD  DEFAULT ((0)) FOR [BlogVerified]
ALTER TABLE [dbo].[tbl_Blogs] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_category_master] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_ContactUs] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_CourseDetails] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_CourseDetails] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_CourseDetails_archive] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_CourseDetails_archive] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_CourseLevel_Master] ADD  DEFAULT ((1)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_CourseLevel_Master] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_CourseLevel_Master] ADD  DEFAULT ('SYSTEM') FOR [CreatedBy]
ALTER TABLE [dbo].[tbl_CourseTestimonial] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_CourseTestimonial] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_CourseTestimonial_Archive] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_CourseTestimonial_Archive] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_Lead] ADD  DEFAULT (getdate()) FOR [createddate]
ALTER TABLE [dbo].[tbl_Premium_Lead] ADD  DEFAULT (getdate()) FOR [createddate]
ALTER TABLE [dbo].[tbl_QualificationLevel_master] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_student_details] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_student_details] ADD  DEFAULT (getdate()) FOR [CreateDate]
ALTER TABLE [dbo].[tbl_TrendingCourses_Master] ADD  DEFAULT ((1)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_TrendingCourses_Master] ADD  DEFAULT (getdate()) FOR [CreatedDate]
ALTER TABLE [dbo].[tbl_TrendingCourses_Master] ADD  DEFAULT ('SYSTEM') FOR [CreatedBy]
ALTER TABLE [dbo].[tbl_university_details] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_university_details] ADD  DEFAULT (getdate()) FOR [CreateDate]
ALTER TABLE [dbo].[tbl_university_details] ADD  DEFAULT ((1)) FOR [isUniversityActive]
ALTER TABLE [dbo].[tbl_university_details_archive] ADD  DEFAULT ((0)) FOR [IsActive]
ALTER TABLE [dbo].[tbl_university_details_archive] ADD  DEFAULT (getdate()) FOR [CreateDate]
ALTER TABLE [dbo].[tbl_university_details_archive] ADD  DEFAULT ((1)) FOR [isUniversityActive]