USE [livedigi_courseshub]

SET IDENTITY_INSERT [dbo].[MenuMaster] ON 

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (1, N'Dashboard', NULL, 1, CAST(N'2025-11-08T19:32:43.650' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (2, N'University Details', NULL, 1, CAST(N'2025-11-08T19:32:54.027' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (3, N'Student Details', NULL, 1, CAST(N'2025-11-08T19:33:00.510' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (4, N'Courses', NULL, 1, CAST(N'2025-11-08T19:33:05.777' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (5, N'Testimonials', NULL, 1, CAST(N'2025-11-08T19:33:13.060' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (6, N'Menu Management', NULL, 1, CAST(N'2025-11-08T20:16:09.113' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (7, N'User Registration', NULL, 1, CAST(N'2025-11-08T20:54:00.590' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (8, N'Blogs', NULL, 1, CAST(N'2025-11-15T19:51:20.407' AS DateTime), NULL)

INSERT [dbo].[MenuMaster] ([id], [MainMenu], [SubMenu], [IsVisible], [CreatedDate], [ModifiedDate]) VALUES (9, N'Leads', NULL, 1, CAST(N'2025-11-25T20:12:41.630' AS DateTime), NULL)

SET IDENTITY_INSERT [dbo].[MenuMaster] OFF

SET IDENTITY_INSERT [dbo].[tbl_Role] ON 

INSERT [dbo].[tbl_Role] ([id], [RoleName], [isActive], [CreatedDate]) VALUES (1, N'Admin', 1, CAST(N'2025-11-08T19:45:13.283' AS DateTime))

SET IDENTITY_INSERT [dbo].[tbl_Role] OFF

SET IDENTITY_INSERT [dbo].[tbl_UserRegistration] ON 

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (1, N'Sanket Ambare', N'sanketambare54@gmail.com', N'Sanket@123', N'8329483829', 1, N'1', CAST(N'2025-11-08T19:48:28.363' AS DateTime), CAST(N'2025-11-27T22:52:32.940' AS DateTime), NULL)

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (2, N'Ravi Ghiware', N'ravighiware9@gmail.com', N'Ravi@123', N'7208481209', 1, N'1', CAST(N'2025-11-08T21:36:49.537' AS DateTime), NULL, N'E:\GitHub\CoursesHub\CoursesHub\Areas\Admin\Uploads\client-03.png')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (3, N'SWATIADMIN', N'swatigalande1096@gmail.com', N'Swati@123', N'9730035743', 1, N'1', CAST(N'2026-01-11T21:33:16.907' AS DateTime), NULL, N'D:\Inetpub\vhosts\livedigital.in\courseshub.com.au\Areas\Admin\Uploads\Screenshot 2025-10-14 094129.png')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (4, N'Swati Galande', N'swatigalande10@gmail.com', N'Swati@123', N'9730035743', 0, N'1', CAST(N'2026-01-11T21:39:14.373' AS DateTime), NULL, N'D:\Inetpub\vhosts\livedigital.in\courseshub.com.au\Areas\Admin\Uploads\Screenshot 2025-10-09 162852.png')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (5, N'ADMIN TEST1', N'galandeswati123@gmail.com', N'Swati@123', N'9730035743', 0, N'1', CAST(N'2026-01-13T23:12:52.800' AS DateTime), NULL, N'')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (6, N'test test', N'swatigalande1996@gmail.com', N'Swati@123', N'9730035743', 0, N'1', CAST(N'2026-01-13T23:14:37.923' AS DateTime), NULL, N'D:\Inetpub\vhosts\livedigital.in\courseshub.com.au\Areas\Admin\Uploads\Screenshot 2025-10-09 162709.png')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (7, N'soniya', N'soniya@gmail.com', N'Sanket@123', N'7799552122', 1, N'1', CAST(N'2026-01-20T09:31:03.587' AS DateTime), NULL, N'C:\Users\Soniya\CoursesHub 4\CoursesHub\Areas\Admin\Uploads\template_0.jpg')

INSERT [dbo].[tbl_UserRegistration] ([userId], [name], [emailId], [password], [mobileNo], [isActive], [role], [createdDate], [modifiedDate], [ImagePath]) VALUES (8, N'Admin testing', N'galandeswati1996@gmail.com', N'Swati@123', N'9730035743', 1, N'1', CAST(N'2026-01-28T00:34:40.560' AS DateTime), NULL, N'D:\Inetpub\vhosts\livedigital.in\courseshub.com.au\Areas\Admin\Uploads\Screenshot 2025-12-27 120142.png')

SET IDENTITY_INSERT [dbo].[tbl_UserRegistration] OFF

SET IDENTITY_INSERT [dbo].[UserMenuMapping] ON 

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (62, 1, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (63, 2, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (64, 3, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (65, 4, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (66, 5, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (67, 6, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (68, 7, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (99, 1, 2, CAST(N'2026-01-21T09:17:33.020' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (100, 2, 2, CAST(N'2026-01-21T09:17:33.020' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (69, 8, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (70, 9, 1, CAST(N'2026-01-13T23:06:38.617' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (80, 1, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (81, 2, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (82, 3, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (83, 4, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (84, 5, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (85, 6, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (86, 7, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (87, 8, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (88, 9, 4, CAST(N'2026-01-20T11:07:37.303' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (106, 9, 3, CAST(N'2026-01-28T00:32:06.923' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (119, 1, 8, CAST(N'2026-01-28T00:37:40.347' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (120, 2, 8, CAST(N'2026-01-28T00:37:40.347' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (121, 4, 8, CAST(N'2026-01-28T00:37:40.347' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (122, 5, 8, CAST(N'2026-01-28T00:37:40.347' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (123, 6, 8, CAST(N'2026-01-28T00:37:40.347' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (92, 2, 7, CAST(N'2026-01-20T11:11:17.833' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (101, 4, 2, CAST(N'2026-01-21T09:17:33.020' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (93, 3, 7, CAST(N'2026-01-20T11:11:17.833' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (94, 4, 7, CAST(N'2026-01-20T11:11:17.833' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (102, 5, 2, CAST(N'2026-01-21T09:17:33.020' AS DateTime), NULL)

INSERT [dbo].[UserMenuMapping] ([id], [MenuId], [LoginId], [CreatedDate], [ModifiedDate]) VALUES (103, 7, 2, CAST(N'2026-01-21T09:17:33.020' AS DateTime), NULL)

SET IDENTITY_INSERT [dbo].[UserMenuMapping] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Blogs] ON 

INSERT [livedigi_courseshub].[tbl_Blogs] ([id], [BlogTitle], [OnelinerDiscription], [Quote], [FullDiscription], [SubTitle], [SubTitleDiscription], [BlogThumbnail], [BlogSmallThumbnail1], [BlogSmallThumbnail2], [BlogSmallThumbnail3], [BlogVerified], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (1, N'TRAVEL BLOG', N'TRAVELLING ', N'CULTURAL', N'<p>TRAVELLING&nbsp;</p>', N'INDIA TOUR', N'CULTURAL', N'/Areas/Admin/BlogImages/1ecf15eb7-7a7c-46f8-82ed-441a31583cc6.png', N'/Areas/Admin/BlogImages/18a8e79ba-c18a-449c-9f6f-ffeeb60fd34a.png', N'/Areas/Admin/BlogImages/1eb4b3212-5d61-434a-a86f-8ed63902444d.png', N'/Areas/Admin/BlogImages/198cd9c00-62af-4bb5-892c-df56766e0501.png', 1, CAST(N'2026-01-28T00:41:24.993' AS DateTime), N'1', NULL, NULL)

INSERT [livedigi_courseshub].[tbl_Blogs] ([id], [BlogTitle], [OnelinerDiscription], [Quote], [FullDiscription], [SubTitle], [SubTitleDiscription], [BlogThumbnail], [BlogSmallThumbnail1], [BlogSmallThumbnail2], [BlogSmallThumbnail3], [BlogVerified], [CreatedDate], [CreatedBy], [ModifyDate], [ModifyBy]) VALUES (2, N'CIVIL ENGG', N'8 SEM', N'TRAINING', N'<p>TRAINING</p>', N'TRAINING', N'TRAINING', N'/Areas/Admin/BlogImages/14727f60f-6653-4ed0-82cd-f45fab73104d.png', N'/Areas/Admin/BlogImages/10648b5dc-fc27-4fe1-8173-5b53ed65c852.png', N'/Areas/Admin/BlogImages/11904445f-4ab3-4e83-9245-173786b8d81a.png', N'/Areas/Admin/BlogImages/1ee9885ba-d25a-406e-9a55-d306bbbc0e3f.png', 1, CAST(N'2026-01-28T00:46:55.270' AS DateTime), N'1', NULL, NULL)

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Blogs] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_category_master] ON 

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (1, N'Aged Care', N'Popular', 1, CAST(N'2025-11-25T22:50:29.023' AS DateTime), N'/assets/images/category/Aged_Care.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (2, N'Disability Care', N'Popular', 1, CAST(N'2025-11-25T22:50:51.417' AS DateTime), N'/assets/images/category/Disability_Care.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (3, N'Community Services', N'Popular', 1, CAST(N'2025-11-25T22:51:06.057' AS DateTime), N'/assets/images/category/Community_Services.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (4, N'Mental Health', N'Popular', 1, CAST(N'2025-11-25T22:51:19.307' AS DateTime), N'/assets/images/category/Mental_Health.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (5, N'Allied Health', N'Popular', 1, CAST(N'2025-11-25T22:51:40.590' AS DateTime), N'/assets/images/category/Allied_Health.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (6, N'Nursing', N'Popular', 1, CAST(N'2025-11-25T22:51:53.137' AS DateTime), N'/assets/images/category/Nursing.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (7, N'Child Care', N'Popular', 1, CAST(N'2025-11-25T22:52:04.747' AS DateTime), N'/assets/images/category/Child_Care.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (8, N'Carpentry', N'Popular', 1, CAST(N'2025-11-25T22:52:18.200' AS DateTime), N'/assets/images/category/Carpentry.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (9, N'Cookery', N'Popular', 1, CAST(N'2025-11-25T22:52:27.527' AS DateTime), N'/assets/images/category/Cookery.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (10, N'Tourism, Travel and Hospitality', N'Popular', 1, CAST(N'2025-11-25T22:52:40.917' AS DateTime), N'/assets/images/category/Tourism_Travel_Hospitality.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (11, N'Building and Construction', N'Popular', 1, CAST(N'2025-11-25T22:52:55.293' AS DateTime), N'/assets/images/category/Building_Construction.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (12, N'Education', N'Popular', 1, CAST(N'2025-11-25T22:53:13.230' AS DateTime), N'/assets/images/category/Education.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (13, N'Training and Assessment', N'Popular', 1, CAST(N'2025-11-25T22:53:25.670' AS DateTime), N'/assets/images/category/Training_Assessment.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (14, N'Pathology', N'Popular', 1, CAST(N'2025-11-25T22:53:38.097' AS DateTime), N'/assets/images/category/Pathology.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (15, N'Hair and Beauty', N'Popular', 1, CAST(N'2025-11-25T22:53:50.880' AS DateTime), N'/assets/images/category/Hair_Beauty.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (16, N'Business', N'Latest', 1, CAST(N'2025-11-25T22:54:13.367' AS DateTime), N'/assets/images/category/Business.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (17, N'Design and Multimedia', N'Latest', 1, CAST(N'2025-11-25T22:54:23.307' AS DateTime), N'/assets/images/category/Design_Multimedia.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (18, N'Screen and Media', N'Latest', 1, CAST(N'2025-11-25T22:54:39.980' AS DateTime), N'/assets/images/category/Screen_Media.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (19, N'Engineering', N'Latest', 1, CAST(N'2025-11-25T22:54:50.280' AS DateTime), N'/assets/images/category/Engineering.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (20, N'Information Technology', N'Latest', 1, CAST(N'2025-11-25T22:55:00.640' AS DateTime), N'/assets/images/category/Information-Technology.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (21, N'Agriculture', N'Core', 1, CAST(N'2025-11-25T22:55:13.793' AS DateTime), N'/assets/images/category/Agriculture.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (22, N'Animal Welfare and Veterinary', N'Core', 1, CAST(N'2025-11-25T22:55:24.827' AS DateTime), N'/assets/images/category/Animal_Welfare_Veterinary.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (23, N'Manufacturing', N'Core', 1, CAST(N'2025-11-25T22:55:35.733' AS DateTime), N'/assets/images/category/Manufacturing.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (24, N'Trades', N'Core', 1, CAST(N'2025-11-25T22:55:44.953' AS DateTime), N'/assets/images/category/Trade.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (25, N'Transport and Logistics', N'Core', 1, CAST(N'2025-11-25T22:55:56.877' AS DateTime), N'/assets/images/category/Transport_Logistics.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (26, N'Finance', N'Core', 1, CAST(N'2025-11-25T22:56:08.297' AS DateTime), N'/assets/images/category/Finance.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (27, N'Environment and Sustainability', N'Core', 1, CAST(N'2025-11-25T22:56:18.220' AS DateTime), N'/assets/images/category/Environment_Sustainability.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (28, N'Horticulture', N'Core', 1, CAST(N'2025-11-25T22:56:31.610' AS DateTime), N'/assets/images/category/Horticulture.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (29, N'Law and Legal Services', N'Master', 1, CAST(N'2025-11-25T22:56:43.580' AS DateTime), N'/assets/images/category/Law_Legal_Services.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (30, N'Creative Arts, Fashion and Music', N'Master', 1, CAST(N'2025-11-25T22:56:59.597' AS DateTime), N'/assets/images/category/Creative_Arts_Fashion_Music.png')

INSERT [livedigi_courseshub].[tbl_category_master] ([Id], [CategoryValue], [CategoryType], [IsActive], [CreatedDate], [ImagePath]) VALUES (31, N'Electrician', N'Master', 1, CAST(N'2025-11-25T22:57:09.893' AS DateTime), N'/assets/images/category/Electrician.png')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_category_master] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_ContactUs] ON 

INSERT [livedigi_courseshub].[tbl_ContactUs] ([Id], [Name], [Email], [Subject], [Message], [CreatedDate], [ModifiedDate]) VALUES (1, N'AndrewErave', N'no.reply.UweFischer@gmail.com', N'Do you aim to gain more customers for your business?', N'Greetings! courseshub.com.au 
 
Did you know that it is possible to send commercial offers legitimate way? 
When such business offers are sent, no personal data is used, and messages are sent to forms specifically designed to receive messages and appeals safely and securely. Since Feedback Forms messages are deemed essential, they won''t be seen as spam. 
Give our service a  – it won’t cost you a thing! 
We can handle sending up to 50,000 messages for you. 
 
The cost of sending one million messages is $59. 
 
This offer is automatically generated. 
 
Contact us. 
Telegram - https://t.me/FeedbackFormEU 
WhatsApp - +375259112693 
WhatsApp  https://wa.me/+375259112693 
We only use chat for communication.', CAST(N'2026-03-07T06:58:48.607' AS DateTime), NULL)

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_ContactUs] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseDetails] ON 

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (18, 13, N'910000', N'CHC33021 Certificate III in Individual Support - Ageing ', N'National College Australia', N'<h2><strong>Why Study Individual Support - Ageing?</strong></h2><p>Aged care is one of the fastest-growing industries in Australia due to being an ageing population.</p><p>It is the standard entry qualification for personal care work in a residential aged care facility (Nursing Home).</p><p>This course is designed for workers in the community and / or residential setting who provide individualised support to people who need support due to ageing, disability and other reasons.</p>', N'Blended', N'1', N'Uploads/NoImage.jpg', NULL, N'21/02/2026', N'16', N'<h2><strong>What you’ll learn in this Course?</strong></h2><p>You''ll get to learn all aspects of a carer''s role including following an individualised plan to undertake daily personal care tasks to ensure the well-being of older people.</p><p>The Ageing specialisation prepares you for employment in the aged care industry as a :</p><ul><li>Care Assistant</li><li>Assistant in Nursing (AIN)</li><li>Residential Care Worker</li><li>Care Worker</li></ul>', N'<h2><strong>Job Roles</strong></h2><h3><strong>The Ageing specialisation prepares you for employment in the aged care industry as a :</strong></h3><ul><li>Assistant in Nursing (AIN)</li><li>Personal Support Assistant</li><li>Residential Support Worker</li><li>Residential Care Worker</li><li>Aged Care Worker</li></ul>', NULL, N'<h2><strong>What do you need to do in this course?</strong></h2><p>This course does NOT have any formal entry requirements but it is important that you have:</p><ul><li><strong>Access to the internet:</strong> Your reading and assessment materials have been set up online. You will need to have reliable internet to access the resources. If the internet speed is not high enough, you may struggle with uploading the required evidence, including videos, to the platform.</li><li><strong>Access to the computer:</strong> You will need access to a laptop or desktop for completing your course. You will need some basic computer skills to navigate the online platform using the instructions provided and download or upload files where required.</li><li>Basic knowledge of using software such as Microsoft Word, Excel, and Adobe PDF.</li><li>A level of fitness that will allow you to undertake physical tasks involved in a day to day work related to the practical requirements of your course.</li><li>Lang', N'<p>120HRS</p>', N'<p>NA</p>', NULL, CAST(N'2026-02-20T21:45:54.330' AS DateTime), 1, 0, N'', NULL, NULL, N'1', N'1')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (19, 14, N'91001', N'asd', N'asd', N'<p>asd</p>', N'Distance', N'1', N'Uploads/NoImage.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2026-03-16T07:47:53.150' AS DateTime), 0, NULL, NULL, NULL, NULL, NULL, NULL)

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (20, 14, N'91001', N'asd', N'asd', N'<p>asd</p>', NULL, N'1', N'Uploads/NoImage.jpg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2026-03-16T07:52:19.927' AS DateTime), 0, NULL, NULL, NULL, NULL, NULL, NULL)

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (21, 14, N'91001', N'CHC33021 Certificate III in Individual Support - Ageing & Disability', N'NCA', N'<h4><strong>Why study this course?</strong></h4><p>Aged care is one of the fastest-growing industries in Australia due to being an ageing population.</p><p>This course is designed for workers in the community and / or residential setting who provide individualised support to people who need support due to ageing, disability and other reasons.</p><p>This course is a double specialisation, meaning, it is made up of units of study that target two work settings:</p><ul><li>Community and / Residential settings</li><li>Clients'' Homes</li></ul><p>The Ageing specialisation prepares you for employment in the aged care industry as a:</p><ul><li>Care Assistant</li><li>Assistant in Nursing (AIN)</li><li>Residential Care Worker or Home Care Worker.</li></ul><p>It is the standard entry qualification for personal care work in a residential aged care facility (Nursing Home) and the home care environment. It is a recognised pathway to becoming an enrolled nurse (EN). Further university study will lead ', N'Blended', N'1', N'Uploads/NoImage.jpg', NULL, N'18/03/2026', N'18', N'<h3><strong>Who is this course for?</strong></h3><p>This course is for:</p><ul><li>people who have no experience in aged care and are interested in getting into the industry</li><li>people currently working in aged care who do not hold a formal qualification</li></ul><p>&nbsp;</p><p><br>&nbsp;</p>', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2026-03-16T20:08:44.533' AS DateTime), 1, 0, N'', NULL, NULL, N'3', N'2')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (22, 14, N'91001', N'CHC33021 Certificate III in Individual Support - Disability Specialisation', N'NCA', N'<h4><strong>Why Study Individual Support (Disability)?</strong></h4><p>Disability services provide care and support to people with physical or intellectual disabilities, as well as their family and carers. Services are designed to improve the health, wellbeing and independence of people with a disability as well as help them develop life skills and participate in social activities. It can be a very rewarding career with multiple pathways available in the much broader spectrum of community services.</p><p>This course is designed for workers in the community and /or residential setting who provide individualised support to people who need support due to ageing, disability and other reasons.</p><h3><strong>What you’ll learn?</strong></h3><p>You''ll get to learn all aspects of a carer''s role including following an individualised plan to undertake daily personal care tasks to support people with a disability.</p><p>The Disability specialisation prepares you for employment in the aged care in', N'FaceToFace', N'1', N'Uploads/NoImage.jpg', NULL, N'28/03/2026', N'12', N'<p>This course is made up of <strong>15 Units: 9 Core Units and 6 Elective Units.</strong></p><p>It is delivered in blended mode.</p><p>When you successfully complete this course, you will receive a certificate issued with CHC33021 - Certificate III in Individual Support - DISABILITY.</p><ul><li>CHCDIV001 Work with diverse people</li><li>CHCCCS038 Facilitate the empowerment of people receiving support</li><li>CHCLEG001 Work legally and ethically</li><li>HLTWHS002 Follow safe work practices for direct client care</li><li>CHCCOM005 Communicate and work in health or community services</li><li>CHCCCS041 Recognise healthy body systems</li><li>CHCCCS040 Support independence and wellbeing</li><li>CHCCCS031 Provide individualised support</li><li>HLTINF006 Apply basic principles and practices of infection prevention and control</li></ul><p>&nbsp;</p><p>&nbsp;</p><p><br>&nbsp;</p>', N'<h3><strong>Job Roles</strong></h3><ul><li>Personal Support Assistant</li><li>Residential Support Worker</li><li>Care Worker</li><li>Disability Service Officer</li></ul><p>&nbsp;</p><p><br>&nbsp;</p>', NULL, N'<h4><strong>What do you need to do in this course?</strong></h4><p>This course does NOT have any formal entry requirements but it is important that you have:</p><ul><li>National College Australia (NCA) suggests you will need to spend an average of 11 hours of study per week to complete the qualification within the maximum course duration. Your course includes a minimum of 120 hours in a suitable workplace to complete practical tasks.</li><li><strong>Access to the internet:</strong> Your reading and assessment materials have been set up online. You will need to have reliable internet to access the resources. If the internet speed is not high enough, you may struggle with uploading the required evidence, including videos, to the platform.</li><li><strong>Access to the computer:</strong> You will need access to a laptop or desktop for completing your course. You will need some basic computer skills to navigate the online platform using the instructions provided and download or upload file', NULL, NULL, NULL, CAST(N'2026-03-16T20:51:20.977' AS DateTime), 1, 0, N'', NULL, NULL, N'3', N'1')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (23, 14, N'91001', N'CHC43015 Certificate IV in Ageing Support', N'NCA', N'<h4><strong>Why Study Ageing Support?</strong></h4><p>Aged care is one of the fastest-growing industries in Australia due to being an ageing population.</p><p>This course is designed for people who hold a Certificate III qualification, or who have at least a few years'' continual experience working as a personal care / support worker in a residential, aged care facilities or clients'' homes.</p><h3><strong>What you’ll learn?</strong></h3><p>This course is designed for support workers who undertake specialised tasks and functions in residential, home or community based ageing services environments. It is the next level up from basic support level work within the aged care industry. Workers at this level may be required to work autonomously and have limited responsibility for supervising others.</p><h3><strong>Who is this course for?</strong></h3><p>New Entrants including:</p><ul><li>people who hold a Certificate III in Individual Support</li><li>people who do not hold a qualification but ', N'FaceToFace', N'1', N'Uploads/NoImage.jpg', NULL, N'31/03/2026', N'12', N'<p>This course is made up of <strong>18 Units: 15 Core Units and 3 Elective Units.</strong></p><p>It is delivered in blended mode.</p><p>When you successfully complete this course, you will receive a certificate issued with <strong>CHC43015 - Certificate IV in Ageing Support</strong></p><ul><li>CHCDIV001 Work with diverse people</li><li>HLTWHS002 Follow safe work practices for direct client care</li><li>CHCLEG003 Manage legal and ethical compliance</li><li>CHCCCS025 Support relationships with carers and families</li><li>CHCPRP001 Develop and maintain networks and collaborative partnerships</li><li>CHCADV001 Facilitate the interests and rights of clients</li><li>CHCCCS006 Facilitate individual service planning and delivery</li><li>CHCAGE001 Facilitate the empowerment of older people</li><li>CHCAGE004 Implement interventions with older people at risk</li><li>CHCPAL001 Deliver care services using a palliative approach</li><li>HLTAAP001 Recognise healthy body systems</li><li>CHCAGE003 Coor', N'<h3><strong>Job Roles</strong></h3><ul><li>Personal Care Assistant</li><li>Assistant in Nursing (AIN)</li><li>Program Coordinator / Supervisor</li><li>Care Supervisor</li><li>Care Team Leader</li></ul><p>&nbsp;</p><p><br>&nbsp;</p>', NULL, N'<h4><strong>What do you need to do in this course?</strong></h4><h3><strong>Existing Worker - Selection Criteria:</strong></h3><p>Students will need to meet the Selection Criteria requirements to be eligible to enter this course as an existing workers.</p><p>Evidence of the student meets the Selection Criteria will be collected as part of the Enrolment process.</p><ul><li>Must be currently employed within the Ageing sector</li><li>Relevant prior training equivalent to a Certificate III or higher or</li><li>LLN Test with report results achieved at level 4 as a minimum requirement</li><li>Must be over the age of 18 years</li><li>As an industry requirement students working in the industry must hold an HLTAID003 Provide</li></ul><p>First Aid</p><ul><li>Willingness to complete a Police Check Record</li><li>Willingness to complete a Working with Children’s Check</li></ul><h3><strong>New Entrants - Selection Criteria:</strong></h3><p>Students will need to meet the Selection Criteria requireme', NULL, NULL, NULL, CAST(N'2026-03-16T20:54:00.030' AS DateTime), 1, 0, N'', NULL, NULL, N'3', N'1')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (24, 14, N'91001', N'CHC43121 Certificate IV in Disability Support – Advanced Worker Stream', N'NCA', N'<h4><strong>Why Study Disability?</strong></h4><p>Disability work can be extremely rewarding for those who have a genuine interest in helping improve the lives of those in need. The course covers the most fundamental aspects of working with people who have an intellectual or physical disability and empowering them to become more independent and self-reliant so they can participate in community activities.</p><p>You can study this course as a pathway to a position with greater responsibility and opportunities to advance within the disability services sector</p><p>The course prepares you for a role in the disability sector as a:</p><ul><li>Senior Personal Care Assistant</li><li>Lifestyle Support Officer</li><li>Disability Team Leader</li><li>Local Area Coordinator</li><li>Disability Support Facilitator</li></ul><h3><strong>Who is this course for?</strong></h3><p>This course is for:</p><p>&nbsp;</p><p><strong>New Entrants:</strong></p><ul><li>people who have completed a Certificate III in', N'Online', N'1', N'Uploads/NoImage.jpg', NULL, N'31/03/2026', N'24', N'<p>This course is made up of <strong>10 Units: 7 Core Units and 3 Elective Units.</strong></p><p>It is delivered in blended mode.</p><p>The course is built with a single unit delivery mode to help you work through the course at a slightly faster pace.</p><p>When you successfully complete this course, you will receive a certificate issued with: <strong>CHC43121 - Certificate IV in Disability.</strong></p><ul><li>CHCCCS044 Follow established person-centred behaviour supports</li><li>CHCDIS017 Facilitate community participation and social inclusion</li><li>CHCDIS018 Facilitate ongoing skills development using a person-centred approach</li><li>CHCDIS019 Provide person-centred services to people with disability with complex needs</li><li>CHCLEG003 Manage legal and ethical compliance</li><li>CHCMHS001 Work with people with mental health issues</li><li>HLTWHS003 Maintain work health and safety</li></ul><p>&nbsp;</p><p>&nbsp;</p><p><br>&nbsp;</p>', N'<h3><strong>Job Roles</strong></h3><ul><li>Senior Personal Support Assistant</li><li>Disability Service Officer</li><li>Residential Support Worker</li></ul><p>&nbsp;</p><p><br>&nbsp;</p>', NULL, N'<h4><strong>What do you need to do in this course?</strong></h4><p>To be eligible for the CHC43121 Certificate IV in Disability Support (Advanced Disability Worker Stream), students must first complete one of the following qualifications:</p><ul><li>CHC33021 Certificate III in Individual Support (Disability), or</li><li>CHC33015 Certificate III in Individual Support (Disability), or</li><li>CHC30408 Certificate III in Disability</li></ul><p>In addition, they must complete the CHCSS00125 Entry to Certificate IV in Disability Support Skill Set.</p><h3><strong>Existing Worker - Selection Criteria</strong></h3><p>Students will need to meet the Selection Criteria requirements to be eligible to enter into this course as an existing worker.</p><p>Evidence of the student meets the Selection Criteria will be collected as part of the Enrolment process.</p><ul><li>Must be currently employed within the Disability sector</li><li>LLN Test with report results achieved at level 4 as a minimum requirem', NULL, NULL, NULL, CAST(N'2026-03-16T21:33:33.517' AS DateTime), 0, NULL, NULL, NULL, NULL, N'4', N'2')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (25, 14, N'91001', N'CHC43121 Certificate IV in Disability Support (Leadership Stream)', N'NCA', N'<h4><strong>Why Study Disability?</strong></h4><p>Disability work can be extremely rewarding for those who have a genuine interest in helping improve the lives of those in need. The course covers the most fundamental aspects of working with people who have an intellectual or physical disability and empowering them to become more independent and self-reliant so they can participate in community activities.</p><p>You can study this course as a pathway to a position with greater responsibility and opportunities to advance within the disability services sector.</p><p>The course prepares you for a role in the disability sector as a:</p><ul><li>Senior Personal Care Assistant</li><li>Lifestyle Support Officer</li><li>Disability Team Leader</li><li>Local Area Coordinator</li><li>Disability Support Facilitator</li></ul><p>Who is this course for?</p><p>This course is for:</p><ul><li>people who have completed a Certificate III in Individual Support and are interested in taking the next step.</li>', N'Blended', N'1', N'Uploads/NoImage.jpg', NULL, N'01/04/2026', N'4', N'<ul><li>CHCCCS044 Follow established person-centred behaviour supports</li><li>CHCDIS017 Facilitate community participation and social inclusion</li><li>CHCDIS018 Facilitate ongoing skills development using a person-centred approach</li><li>CHCDIS019 Provide person-centred services to people with disability with complex needs</li><li>CHCLEG003 Manage legal and ethical compliance</li><li>CHCMHS001 Work with people with mental health issues</li><li>HLTWHS003 Maintain work health and safety</li></ul><p>&nbsp;</p><p><br>&nbsp;</p>', N'<h3><strong>Job Roles</strong></h3><ul><li>Assistant in Nursing (AIN)</li><li>Personal Support Assistant</li><li>Residential Support Worker</li><li>HResidential Care Worker</li><li>Aged Care Worker</li></ul><p><br>&nbsp;</p>', NULL, N'<h4><strong>What do you need to do in this course?</strong></h4><p>To be eligible for the CHC43121 Certificate IV in Disability Support (Leadership Stream), students must first complete one of the following qualifications:</p><ul><li>CHC33021 Certificate III in Individual Support (Disability),or</li><li>CHC33015 Certificate III in Individual Support (Disability), or</li><li>CHC30408 Certificate III in Disability, or</li></ul><p>In addition, they must complete the CHCSS00125 Entry to Certificate IV in Disability Support Skill Set.</p><h3><strong>Existing Worker - Selection Criteria</strong></h3><p>Students will need to meet the Selection Criteria requirements to be eligible to enter into this course as an existing worker.</p><p>Evidence of the student meets the Selection Criteria will be collected as part of the Enrolment process.</p><ul><li>Must be currently employed within the Disability sector</li><li>LLN Test with report results achieved at level 4 as a minimum requirement</li><li>M', NULL, NULL, NULL, CAST(N'2026-03-16T21:39:56.823' AS DateTime), 0, NULL, NULL, NULL, NULL, N'4', N'1')

INSERT [livedigi_courseshub].[tbl_CourseDetails] ([Id], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (26, 14, N'91001', N'CHC32021 Certificate III in Community Services', N'NCA', N'<h4><strong>Why Study in Community Service?</strong></h4><p>Community Service is one of the fastest growing industry areas in Australia. The community services sector includes services provided by a broad range of national, state, and territory governments, and non-government agencies (Australian Institute of Health and Welfare, Australian Government, ''Community Services Sector'', 2019).</p><p>Working in community services is extremely rewarding and allows you to support local communities by caring for a diverse range of people in different life situations.</p><p>This qualification reflects the role of entry-level community services workers who support individuals through the provision of person-centred services. Work may include day-to-day support of individuals in community settings or support the implementation of specific community-based programs.</p><p>Studying one of our community services courses online is ideal if you are looking for a broad qualification in community services. ', N'Blended', N'1', N'Uploads/NoImage.jpg', NULL, N'25/03/2026', N'3', N'<ul><li>CHCDIV001 Work with diverse people</li><li>HLTWHS002 Follow safe work practices for direct client care</li><li>HLTWHS006 Manage personal stressors in the work place</li><li>CHCCCS016 Respond to client needs</li><li>CHCCOM005 Communicate and work in health or community services</li></ul><p>&nbsp;</p><p>&nbsp;</p><h3>&nbsp;</h3><ul><li>&nbsp;</li></ul><p><br>&nbsp;</p>', N'<h3><strong>Job Roles</strong></h3><ul><li>Community Support Worker</li><li>Case Worker</li><li>Family Support Worker</li><li>Welfare Support Worker</li><li>Community &amp; Personal Service Worker</li></ul><p>&nbsp;</p><h3>&nbsp;</h3><ul><li>&nbsp;</li></ul><p><br>&nbsp;</p>', NULL, N'<h4><strong>What do you need to do in this course?</strong></h4><p>This course does NOT have any formal entry requirements but it is important that you have:</p><ul><li><strong>Access to the internet:</strong> Your reading and assessment materials have been set up online. You will need to have reliable internet to access the resources. If the internet speed is not high enough, you may struggle with uploading the required evidence, including videos, to the platform.</li><li><strong>Access to the computer:</strong> You will need access to a laptop or desktop for completing your course. You will need some basic computer skills to navigate the online platform using the instructions provided and download or upload files where required.</li><li>Basic knowledge of using software such as Microsoft Word, Excel, and Adobe PDF.</li><li>A level of fitness that will allow you to undertake physical tasks involved in a day to day work related to the practical requirements of your course.</li><li>Lang', NULL, NULL, NULL, CAST(N'2026-03-16T21:42:22.440' AS DateTime), 0, NULL, NULL, NULL, NULL, N'5', N'1')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseDetails] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseDetails_archive] ON 

INSERT [livedigi_courseshub].[tbl_CourseDetails_archive] ([Id], [CourseId], [UniversityId], [RTO_ID], [CourseName], [CourseProvider], [AboutCourse], [StudyMethod], [CourseCategory], [CoursePhoto], [CourseVideo], [CourseStartDate], [CourseDuration], [CourseOverview], [CourseOutcomes], [CourseLearn], [CourseRequirements], [WorkPlacement], [RPL], [CourseGuide], [CreatedDate], [IsActive], [isReject], [RejectionReason], [CourseVideo2], [CourseVideo3], [TrendingCourse], [CourseLevel]) VALUES (1, 7, 8, N'342345', N'Full Stack Web Development', N'CodeMasters Academy', N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', N'Online', N'20', N'Uploads/Photo/8/d944287e-7298-453d-9288-13c69ca5d556.png', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'31/01/2026', N'7', N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', NULL, N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', N'<p>Learn front-end &amp; back-end development using HTML, CSS, JS, Node.js &amp; databases.</p>', N'Uploads/Guide/8/0089c728-35a8-4354-9147-85eca196fba4.pdf', CAST(N'2026-01-28T02:42:04.313' AS DateTime), 1, 0, N'', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'1', N'1')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseDetails_archive] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseLevel_Master] ON 

INSERT [livedigi_courseshub].[tbl_CourseLevel_Master] ([Id], [CourseLevel], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1, N'Beginners', 1, CAST(N'2026-01-03T11:33:05.060' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_CourseLevel_Master] ([Id], [CourseLevel], [IsActive], [CreatedDate], [CreatedBy]) VALUES (2, N'Advance', 1, CAST(N'2026-01-03T11:33:17.480' AS DateTime), N'SYSTEM')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseLevel_Master] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseTestimonial] ON 

INSERT [livedigi_courseshub].[tbl_CourseTestimonial] ([Id], [Course_Id], [Person_Name], [Designation], [Comment], [IsActive], [CreatedDate], [Student_Id]) VALUES (3, 5, N'SWATI', N'QA', N'TESTING', 1, CAST(N'2026-01-28T02:32:46.493' AS DateTime), NULL)

INSERT [livedigi_courseshub].[tbl_CourseTestimonial] ([Id], [Course_Id], [Person_Name], [Designation], [Comment], [IsActive], [CreatedDate], [Student_Id]) VALUES (4, 11, N'Sanket AMbare', N'Sr. Software Engg', N'Good Course', 0, CAST(N'2026-01-30T23:16:56.647' AS DateTime), NULL)

INSERT [livedigi_courseshub].[tbl_CourseTestimonial] ([Id], [Course_Id], [Person_Name], [Designation], [Comment], [IsActive], [CreatedDate], [Student_Id]) VALUES (5, 11, N'Ravi Ghiware', N'Team Lead', N'Grt Learnings', 0, CAST(N'2026-01-30T23:16:56.650' AS DateTime), NULL)

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_CourseTestimonial] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Lead] ON 

INSERT [livedigi_courseshub].[tbl_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (1, 3, 1, 2, 1, CAST(N'2026-01-27T09:03:02.793' AS DateTime), CAST(N'2026-01-27T09:03:02.607' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (2, 7, 6, 6, 1, CAST(N'2026-01-28T02:43:12.483' AS DateTime), CAST(N'2026-01-28T02:43:12.127' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (3, 5, 6, 8, 1, CAST(N'2026-01-28T02:48:27.810' AS DateTime), CAST(N'2026-01-28T02:48:27.523' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (4, 7, 7, 6, 1, CAST(N'2026-01-28T08:11:26.570' AS DateTime), CAST(N'2026-01-28T08:11:26.137' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (5, 7, 8, 6, 1, CAST(N'2026-01-28T09:13:16.650' AS DateTime), CAST(N'2026-01-28T09:13:16.507' AS DateTime))

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Lead] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Premium_Lead] ON 

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (1, 3, 1, 1, 1, CAST(N'2026-01-27T09:03:02.727' AS DateTime), CAST(N'2026-01-27T09:03:02.603' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (2, 7, 6, 8, 1, CAST(N'2026-01-28T02:43:12.287' AS DateTime), CAST(N'2026-01-28T02:43:12.127' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (3, 5, 6, 6, 1, CAST(N'2026-01-28T02:48:27.707' AS DateTime), CAST(N'2026-01-28T02:48:27.520' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (4, 7, 7, 8, 1, CAST(N'2026-01-28T08:11:26.500' AS DateTime), CAST(N'2026-01-28T08:11:26.133' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (5, 9, 8, 12, 1, CAST(N'2026-01-28T09:10:56.380' AS DateTime), CAST(N'2026-01-28T09:10:56.260' AS DateTime))

INSERT [livedigi_courseshub].[tbl_Premium_Lead] ([Id], [Course_ID], [Student_ID], [University_ID], [isEmailSent], [EmailSentOn], [createddate]) VALUES (6, 7, 8, 8, 1, CAST(N'2026-01-28T09:13:16.603' AS DateTime), CAST(N'2026-01-28T09:13:16.503' AS DateTime))

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_Premium_Lead] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ON 

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (1, N'Certificate II', 1, CAST(N'2025-11-25T23:01:36.487' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (2, N'Certificate III', 1, CAST(N'2025-11-25T23:01:43.503' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (3, N'Certificate IV', 1, CAST(N'2025-11-25T23:01:50.833' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (4, N'Diploma', 1, CAST(N'2025-11-25T23:02:05.350' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (5, N'Advanced Diploma', 1, CAST(N'2025-11-25T23:02:16.550' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (6, N'Bachelor', 1, CAST(N'2025-11-25T23:03:19.943' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (7, N'Graduate Certificate', 1, CAST(N'2025-11-25T23:03:30.180' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (8, N'Graduate Diploma', 1, CAST(N'2025-11-25T23:03:40.837' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (9, N'Masters', 1, CAST(N'2025-11-25T23:03:51.477' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (10, N'Short', 1, CAST(N'2025-11-25T23:04:01.147' AS DateTime))

INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] ([Id], [QualificationType], [IsActive], [CreatedDate]) VALUES (11, N'Unaccredited', 1, CAST(N'2025-11-25T23:04:17.117' AS DateTime))

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_QualificationLevel_master] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_student_details] ON 

INSERT [livedigi_courseshub].[tbl_student_details] ([Id], [FirstName], [MiddleName], [LastName], [EmailId], [Mobile], [Password], [IsActive], [CreateDate], [isAustraliaResident], [Zipcode], [State], [Country], [VerifyLink], [Area]) VALUES (6, N'STUDENTTEST', N'TEST', N'VERIFY', N'sanketambare54@gmail.com', N'9730035743', N'01E956', 1, CAST(N'2026-01-28T01:20:24.947' AS DateTime), 1, N'4000 ', N'Australian Capital Territory', N'', N'2B2BBD6D-911A-4A08-BFD2-EB1A9011DE00', N' Brisbane')

INSERT [livedigi_courseshub].[tbl_student_details] ([Id], [FirstName], [MiddleName], [LastName], [EmailId], [Mobile], [Password], [IsActive], [CreateDate], [isAustraliaResident], [Zipcode], [State], [Country], [VerifyLink], [Area]) VALUES (7, N'SANKET', N'SANJAY', N'AMBARE', N'galandeswati123@gmail.com', N'9730035743', N'Swati@123', 1, CAST(N'2026-01-28T08:09:36.150' AS DateTime), 1, N'4000 ', N'Queensland', N'', N'2121F353-A3DA-48BE-AA54-5A9193CB4493', N' Spring Hill')

INSERT [livedigi_courseshub].[tbl_student_details] ([Id], [FirstName], [MiddleName], [LastName], [EmailId], [Mobile], [Password], [IsActive], [CreateDate], [isAustraliaResident], [Zipcode], [State], [Country], [VerifyLink], [Area]) VALUES (8, N'TESTSWATI', N'SUDAM', N'GALANDE', N'galandeswati10@gmail.com', N'9730035743', N'Swati@123', 1, CAST(N'2026-01-28T09:02:19.107' AS DateTime), 1, N'4000 ', N'Queensland', N'', N'B9E21613-0820-48FD-9EDF-2C51D717505B', N' Petrie Terrace')

INSERT [livedigi_courseshub].[tbl_student_details] ([Id], [FirstName], [MiddleName], [LastName], [EmailId], [Mobile], [Password], [IsActive], [CreateDate], [isAustraliaResident], [Zipcode], [State], [Country], [VerifyLink], [Area]) VALUES (9, N'Ujala', NULL, N'Sinha', N'ujalasinha@outlook.com', N'0450028047', N'Apt@123', 1, CAST(N'2026-01-30T23:43:50.710' AS DateTime), 1, N'5000 ', N'South Australia', N'', N'817178B9-369B-4A9F-B5D6-735D5063F566', N' Adelaide')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_student_details] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ON 

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (1, N'CHC33021 Certificate III in Individual Support (Ageing)', 1, CAST(N'2026-01-03T10:43:04.873' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (2, N'CHC33021 Certificate III in Individual Support (Disability)', 1, CAST(N'2026-01-03T10:43:28.500' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (3, N'CHC43015 Certificate IV in Ageing Support', 1, CAST(N'2026-01-03T10:43:40.843' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (4, N'CHC43121 Certificate IV in Disability Support', 1, CAST(N'2026-01-03T10:43:52.847' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (5, N'CHC32021 Certificate III in Community Services', 1, CAST(N'2026-01-03T10:44:10.783' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (6, N'CHC42021 Certificate IV in Community Services', 1, CAST(N'2026-01-03T10:44:24.520' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (7, N'CHC52021 Diploma of Community Services', 1, CAST(N'2026-01-03T10:44:37.910' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (8, N'CHC43315 Certificate IV in Mental Health', 1, CAST(N'2026-01-03T10:44:59.803' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (9, N'CHC53315 Diploma of Mental Health', 1, CAST(N'2026-01-03T10:45:22.133' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (10, N'HLT33021 Certificate III in Allied Health Assistance', 1, CAST(N'2026-01-03T10:45:36.087' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (11, N'HLT43021 Certificate IV in Allied Health Assistance', 1, CAST(N'2026-01-03T10:45:52.697' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (12, N'CHC43415 Certificate IV in Leisure and Health', 1, CAST(N'2026-01-03T10:46:06.810' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (13, N'HLT54121 Diploma of Nursing', 1, CAST(N'2026-01-03T10:46:19.167' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (14, N'HLT35021 Certificate III in Dental Assisting', 1, CAST(N'2026-01-03T10:46:36.060' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (15, N'HLT45021 Certificate IV in Dental Assisting', 1, CAST(N'2026-01-03T10:46:51.640' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (16, N'HLT45021 Certificate IV in Dental Assisting (Dental Radiography)', 1, CAST(N'2026-01-03T10:47:06.530' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (17, N'CHC30121 Certificate III in Early Childhood Education and Care', 1, CAST(N'2026-01-03T10:47:20.860' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (18, N'CHC50121 Diploma of Early Childhood Education & Care', 1, CAST(N'2026-01-03T10:47:35.720' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (19, N'HLT35021 Certificate III in Dental Assisting', 1, CAST(N'2026-01-03T10:47:49.847' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (20, N'SIT50422 Diploma of Hospitality Management', 1, CAST(N'2026-01-03T10:48:04.067' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (21, N'CPC30220 Certificate III in Carpentry', 1, CAST(N'2026-01-03T10:48:16.067' AS DateTime), N'SYSTEM')

INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] ([Id], [CourseName], [IsActive], [CreatedDate], [CreatedBy]) VALUES (22, N'CPC40120 Certificate IV in Building and Construction (Building)', 1, CAST(N'2026-01-03T10:48:29.567' AS DateTime), N'SYSTEM')

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_TrendingCourses_Master] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_university_details] ON 

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (6, N'123456', 1, N'23456E', N'PUNE UNIVERSITY', N'JAVA', N'123423456', N'BANER', N'PUNE', N'https://www.youtube.com/watch?v=ysz5S6PUM-U ', N'9730035743', N'swatigalande1096@gmail.com', N'SWATI GALANDE', N'QA', N'9730035743', N'swatigalande1096@gmail.com', N'20', N'Uploads/Sign/123456/5751e7ce-92dc-44dc-8baf-51248ed7a50e.png', N'Uploads/Logo/123456/059123bb-a116-4fea-8e63-9625ff289484.png', N'382DHUS', 1, CAST(N'2026-01-28T01:26:12.533' AS DateTime), N'4000 - Brisbane', N'Australian Capital Territory', NULL, NULL, 0)

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (11, N'234120', 1, N'23245E', N'NEW TEST UN', N'WRD', N'231487907', N'SATARA', N'PUNE', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'9730035743', N'galandeswati1996@gmail.com', N'SWATI ', N'QA', N'9730035743', N'galandeswati1996@gmail.com', N'11', N'Uploads/Sign/234120/fbb8dbed-fd2d-4f84-a694-e24472131af1.png', N'Uploads/Logo/234120/a63089f4-98af-4b9a-9657-729314ee139d.png', N'F6NVZ8S', 1, CAST(N'2026-01-28T08:56:36.383' AS DateTime), N'4000 - Spring Hill', N'South Australia', NULL, NULL, 1)

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (12, N'467000', 1, N'34256E', N'SATARA U', N'BCD', N'234556789', N'BANER', N'PUNE', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'9730035743', N'galandeswati123@gmail.com', N'SANKET', N'developer', N'9730035743', N'galandeswati123@gmail.com', N'16', N'Uploads/Sign/467000/c65829fe-f44d-47fe-a27e-08ee4f2479d7.png', N'Uploads/Logo/467000/6fdeaa32-b21c-4c76-866a-c07654aec681.png', N'NMOW2LI', 1, CAST(N'2026-01-28T08:58:37.117' AS DateTime), N'4000 - Brisbane City', N'South Australia', NULL, NULL, 1)

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (13, N'910000', 1, NULL, N'National College Australia', NULL, N'78097149598', N'Level 1 / 118 King William Street Adelaide ', N'Level 1 / 118 King William Street Adelaide ', N'www.nca.edu.au', N'0450028047', N'ujala.sinha@nca.edu.au', N'Ujala Sinha', N'Director ', N'0450028047', N'ujala.sinha@nca.edu.au', N'1', NULL, NULL, N'A9B53A', 1, CAST(N'2026-01-31T00:01:32.843' AS DateTime), N'5000 - Adelaide', N'South Australia', NULL, NULL, 1)

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (14, N'91001', 1, NULL, N'National College', NULL, N'78079125698', N'Unit no 401, Konar Business Park, 404, Road No. 16/Z, Ambica Nagar, Wagle Industrial Estate,', N'40000', N'https://www.auztraining.com.au/', N'8976612086', N'vivek@nca.edu.au', N'Vivek YoKrazy', N'HR', N'8976612086', N'vivek@nca.edu.au', N'1', NULL, N'Uploads/Logo/91001/31fea3a6-2d5f-457b-87cb-16f0aebf74e6.jpg', N'BCVGNKS', 1, CAST(N'2026-03-16T04:39:46.223' AS DateTime), N'4000 - Brisbane', N'Tasmania', NULL, NULL, 1)

INSERT [livedigi_courseshub].[tbl_university_details] ([Id], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (15, N'00009', 0, NULL, N'The One', NULL, N'00000000009', N'Testing', N'Testing One', N'testing.com', N'7208481209', N'ravighiware9@gmail.com', N'Ravi', N'Teacher', N'7208481209', N'ravighiware9@gmail.com', N'1,16', N'Uploads/Sign/00009/8727190f-fd80-44b8-878d-e048410714e1.png', N'Uploads/Logo/00009/17c356c0-948c-43b7-8889-13240aee7add.png', NULL, 0, CAST(N'2026-03-16T07:27:37.230' AS DateTime), N'4000 - Brisbane', N'South Australia', NULL, NULL, 1)

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_university_details] OFF

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_university_details_archive] ON 

INSERT [livedigi_courseshub].[tbl_university_details_archive] ([Id], [UnivercityId], [RTO_ID], [RTO_Status], [CRICOS_Code], [Legal_Name_Organisation], [Trading_Name], [ABN_ACN], [Business_Address], [Postal_Address], [Website], [Contact_Number], [General_Contact_Email], [Full_Name], [Position], [Direct_Phone], [Email], [Relevant_Courses], [Signature], [Logo], [Password], [IsActive], [CreateDate], [ZipCode], [State], [IsReject], [RejectionReason], [isUniversityActive]) VALUES (1, 8, N'342345', 1, N'43567E', N'MUMBAI UNIVERSITY', N'PYTHON', N'32456756789', N'ANDHERI', N'MUMBAI', N'https://www.youtube.com/watch?v=ysz5S6PUM-U', N'9730035743', N'swatigalande1996@gmail.com', N'SANKET AMBARE', N'DEVELOPER', N'9730035743', N'swatigalande1996@gmail.com', N'20', N'Uploads/Sign/342345/a56875a9-b883-4a2b-9da4-1f01f4148053.png', N'Uploads/Logo/342345/66bb48ed-b3c4-43ab-891b-9e57c1ae0457.png', N'JMTWC90', 1, CAST(N'2026-01-28T02:39:18.850' AS DateTime), N'4000 - Brisbane', N'Australian Capital Territory', NULL, NULL, 1)

SET IDENTITY_INSERT [livedigi_courseshub].[tbl_university_details_archive] OFF

