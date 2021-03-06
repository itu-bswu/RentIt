/****** Object:  Table [dbo].[User]    Script Date: 05/07/2012 19:23:07 ******/
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (1, N'Smith', N'anmlqsjoiJOekOu8vIUGwvc52O29Nm4XWH1MLdfZzth8TMfjlfb2+vxInCpQyuUTgF9RGTw9fpVdNbVdT7Iamg==', N'smith@matrix.org', N'James Smith', 1, NULL)
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (2, N'Universal', N'c/PZ5+9Vr/2LlEsELUCB7mVClGqvQ8Z6Dkliwp3KFVKjPC/NUVd413bQJJhdKsty4VNWQa2sVt3fMQ84iFcpjw==', N'rentit@universalpictures.com', N'Universal Pictures', 2, NULL)
INSERT [dbo].[User] ([user_id], [username], [password], [email], [full_name], [type], [token]) VALUES (3, N'Anderson', N'HHRVDX8/9DgRsG9jyEoRpITeEX9rm3AcZFZhw2EQ8J0IMa7gRZxfEsr5OJKFzojPkblU7i7qmQv4eU0ceTsS8Q==', N'neo@matrix.org', N'Thomas Anderson', 3, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Genre]    Script Date: 05/07/2012 19:23:07 ******/
SET IDENTITY_INSERT [dbo].[Genre] ON
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (3, N'Action')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (5, N'Adventure')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (7, N'Comedy')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (1, N'Crime')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (4, N'Drama')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (10, N'Family')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (9, N'Fantasy')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (8, N'Romance')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (6, N'Sci-Fi')
INSERT [dbo].[Genre] ([genre_id], [name]) VALUES (2, N'Thriller')
SET IDENTITY_INSERT [dbo].[Genre] OFF
/****** Object:  Table [dbo].[Movie]    Script Date: 05/07/2012 19:23:07 ******/
SET IDENTITY_INSERT [dbo].[Movie] ON
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (1, N'Ocean''s Eleven', N'Danny Ocean and his eleven accomplices plan to rob three Las Vegas casinos simultaneously.', NULL, 2, CAST(0x0000916C00A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (2, N'Ocean''s Twelve', N'Daniel Ocean recruits one more team member so he can pull off three major European heists in this sequel to Ocean''s 11.', NULL, 2, CAST(0x000095B700A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (3, N'Ocean''s Thirteen', N'Danny Ocean rounds up the boys for a third heist, after casino owner Willy Bank double-crosses one of the original eleven, Reuben Tishkoff.', NULL, 2, CAST(0x0000993800A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (4, N'Batman Begins', N'Bruce Wayne loses his philanthropic parents to a senseless crime, and years later becomes the Batman to save the crime-ridden Gotham City on the verge of destruction by an ancient order.', NULL, 2, CAST(0x0000967400A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (5, N'The Matrix', N'A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.', NULL, 2, CAST(0x00008D9800A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (6, N'The Matrix Reloaded', N'Neo and the rebel leaders estimate that they have 72 hours until 250,000 probes discover Zion and destroy it and its inhabitants. During this, Neo must decide how he can save Trinity from a dark fate in his dreams.', NULL, 2, CAST(0x0000937200A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (7, N'The Matrix Revolutions', N'The human city of Zion defends itself against the massive invasion of the machines as Neo fights to end the war at another front while also opposing the rogue Agent Smith.', NULL, 2, CAST(0x0000941F00A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (8, N'The Mask', N'Bank clerk Stanley Ipkiss is transformed into a manic super-hero when he wears a mysterious mask.', NULL, 2, CAST(0x000086EE00A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (9, N'American Pie', N'Four teenage boys enter a pact to lose their virginity by prom night.', NULL, 2, CAST(0x00008DFC00A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (10, N'American Pie 2', N'The continuing bawdy adventures of a group of friends reuniting after their first year of college.', NULL, 2, CAST(0x000090F300A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (11, N'American Wedding', N'It''s the wedding of Jim and Michelle and the gathering of their families and friends, including Jim''s old friends from high school and Michelle''s little sister.', NULL, 2, CAST(0x000093A200A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (12, N'The Lord of the Rings: The Fellowship of the Ring', N'An innocent hobbit of The Shire journeys with eight companions to the fires of Mount Doom to destroy the One Ring and the dark lord Sauron forever.', NULL, 2, CAST(0x0000917100A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (13, N'The Lord of the Rings: The Two Towers', N'While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron''s new ally, Saruman, and his hordes of Isengard.', NULL, 2, CAST(0x000092D900A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (14, N'The Lord of the Rings: The Return of the King', N'Aragorn leads the World of Men against Sauron''s army to draw the dark lord''s gaze from Frodo and Sam who are on the doorstep of Mount Doom with the One Ring.', NULL, 2, CAST(0x0000945200A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (15, N'Die Hard', N'New York cop John McClane gives terrorists a dose of their own medicine as they hold hostages in an LA office building.', NULL, 2, CAST(0x00007E5100A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (16, N'Die Hard 2', N'John McClane is forced to battle mercenaries who seize control of an airport''s communications and threaten to cause plane crashes if their demands are not met.', NULL, 2, CAST(0x0000811E00A4CB80 AS DateTime))
INSERT [dbo].[Movie] ([movie_id], [title], [description], [image_path], [owner_id], [release_date]) VALUES (17, N'How the Grinch Stole Christmas', N'Big budget remake of the classic cartoon about a creature intent on stealing Christmas.', NULL, 2, CAST(0x00008FE400A4CB80 AS DateTime))
SET IDENTITY_INSERT [dbo].[Movie] OFF
/****** Object:  Table [dbo].[HasGenre]    Script Date: 05/07/2012 19:23:07 ******/
SET IDENTITY_INSERT [dbo].[HasGenre] ON
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (1, 1, 1)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (2, 2, 1)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (3, 1, 2)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (4, 2, 2)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (5, 1, 3)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (6, 2, 3)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (7, 3, 4)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (8, 1, 4)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (9, 4, 4)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (10, 3, 5)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (11, 5, 5)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (12, 6, 5)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (13, 3, 6)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (14, 5, 6)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (15, 6, 6)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (16, 3, 7)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (17, 5, 7)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (18, 6, 7)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (19, 3, 8)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (20, 7, 8)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (21, 1, 8)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (22, 7, 9)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (23, 8, 9)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (24, 7, 10)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (25, 8, 10)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (26, 7, 11)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (27, 8, 11)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (28, 3, 12)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (29, 5, 12)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (30, 9, 12)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (31, 3, 13)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (32, 5, 13)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (33, 9, 13)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (34, 3, 14)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (35, 5, 14)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (36, 9, 14)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (37, 3, 15)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (38, 2, 15)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (39, 3, 16)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (40, 2, 16)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (41, 7, 17)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (42, 10, 17)
INSERT [dbo].[HasGenre] ([hasgenre_id], [genre_id], [movie_id]) VALUES (43, 9, 17)
SET IDENTITY_INSERT [dbo].[HasGenre] OFF
/****** Object:  Table [dbo].[Edition]    Script Date: 05/07/2012 19:23:07 ******/
SET IDENTITY_INSERT [dbo].[Edition] ON
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (1, 1, N'SD', N'Oceans_Eleven_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (2, 1, N'HD 720p', N'Oceans_Eleven_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (3, 1, N'HD 1080p', N'Oceans_Eleven_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (4, 2, N'SD', N'Oceans_Twelve_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (5, 2, N'HD 720p', N'Oceans_Twelve_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (6, 2, N'HD 1080p', N'Oceans_Twelve_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (7, 3, N'SD', N'Oceans_Thirteen_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (8, 3, N'HD 720p', N'Oceans_Thirteen_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (9, 3, N'HD 1080p', N'Oceans_Thirteen_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (10, 4, N'SD', N'Batman_Begins_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (11, 4, N'HD 720p', N'Batman_Begins_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (12, 4, N'HD 1080p', N'Batman_Begins_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (13, 5, N'SD', N'The_Matrix_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (14, 5, N'HD 720p', N'The_Matrix_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (15, 5, N'HD 1080p', N'The_Matrix_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (16, 6, N'SD', N'Matrix_Reloaded_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (17, 6, N'HD 720p', N'Matrix_Reloaded_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (18, 6, N'HD 1080p', N'Matrix_Reloaded_1080p')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (19, 7, N'SD', N'Matrix_Revolutions_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (20, 7, N'HD 720p', N'Matrix_Revolutions_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (21, 7, N'HD 1080p', N'Matrix_Revolutions_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (22, 8, N'SD', N'The_Mask.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (23, 9, N'SD', N'American_Pie_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (24, 9, N'HD 720p', N'American_Pie_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (25, 9, N'HD 1080p', N'American_Pie_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (26, 10, N'SD', N'American_Pie_2_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (27, 10, N'HD 720p', N'American_Pie_2_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (28, 10, N'HD 1080p', N'American_Pie_2_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (29, 11, N'SD', N'American_Wedding_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (30, 11, N'HD 720p', N'American_Wedding_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (31, 11, N'HD 1080p', N'American_Wedding_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (32, 12, N'SD', N'LOTR_Fellowship_of_the_Ring_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (33, 12, N'HD 720p', N'LOTR_Fellowship_of_the_Ring_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (34, 12, N'HD 1080p', N'LOTR_Fellowship_of_the_Ring_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (35, 13, N'SD', N'LOTR_Two_Towers_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (36, 13, N'HD 720p', N'LOTR_Two_Towers_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (37, 13, N'HD 1080p', N'LOTR_Two_Towers_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (38, 14, N'SD', N'LOTR_Return_of_the_King_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (39, 14, N'HD 720p', N'LOTR_Return_of_the_King_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (40, 14, N'HD 1080p', N'LOTR_Return_of_the_King_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (41, 15, N'SD', N'Die_Hard_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (42, 15, N'HD 720p', N'Die_Hard_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (43, 15, N'HD 1080p', N'Die_Hard_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (44, 16, N'SD', N'Die_Hard_2_SD.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (45, 16, N'HD 720p', N'Die_Hard_2_720p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (46, 16, N'HD 1080p', N'Die_Hard_2_1080p.avi')
INSERT [dbo].[Edition] ([edition_id], [movie_id], [name], [file_path]) VALUES (47, 17, N'SD', N'The_Grinch.avi')
SET IDENTITY_INSERT [dbo].[Edition] OFF
/****** Object:  Table [dbo].[Rental]    Script Date: 05/07/2012 19:23:07 ******/
