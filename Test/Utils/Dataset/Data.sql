GO
/****** Object:  Table [dbo].[Movie]    Script Date: 04/02/2012 01:09:00 ******/
/****** Object:  Table [dbo].[User]    Script Date: 04/02/2012 01:09:00 ******/
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (11, N'1', N'System.Byte[]', N'1', N'1', 0, N'')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (16, N'143rt', N'System.Byte[]', N'2312', N'123', 0, N'')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (34, N'qweqwe', N'System.Byte[]', N'1', N'1', 0, N'')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (35, N'sdf', N'System.Byte[]', N'11', N'11', 0, N'')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (39, N'123', N'System.Byte[]', N'123', N'123', 0, N'c076ea34ae4c18d0')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (50, N'testUser', N'test.dk', N'testUser@testing.dk', N'Test User', 0, N'testUserToken')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (51, N'testContentProvider', N'test.dk', N'testContentProvider@testing.dk', N'Test ContentProvider', 1, N'testContentProviderToken')
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (52, N'testAdmin', N'test.dk', N'testAdmin@testing.dk', N'Test Admin', 2, N'testAdminToken')
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Rental]    Script Date: 04/02/2012 01:09:00 ******/
