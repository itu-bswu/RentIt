/****** Object:  Table [dbo].[User]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[full_name] [nvarchar](100) NULL,
	[type] [smallint] NOT NULL,
	[token] [nvarchar](100) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Username] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[genre_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
(
	[genre_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [Unique_GenreName] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[movie_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NULL,
	[image_path] [nvarchar](100) NULL,
	[owner_id] [int] NOT NULL,
	[release_date] [datetime] NULL,
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[movie_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HasGenre]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HasGenre](
	[hasgenre_id] [int] IDENTITY(1,1) NOT NULL,
	[genre_id] [int] NOT NULL,
	[movie_id] [int] NOT NULL,
 CONSTRAINT [PK_HasGenre] PRIMARY KEY CLUSTERED 
(
	[hasgenre_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Edition]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Edition](
	[edition_id] [int] IDENTITY(1,1) NOT NULL,
	[movie_id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[file_path] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Edition] PRIMARY KEY CLUSTERED 
(
	[edition_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 05/07/2012 19:21:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[rental_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[edition_id] [int] NOT NULL,
	[time] [datetime] NOT NULL,
 CONSTRAINT [PK_Rental] PRIMARY KEY CLUSTERED 
(
	[rental_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_User_type]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_type]  DEFAULT ((1)) FOR [type]
GO
/****** Object:  ForeignKey [FK_Edition_Movie]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_Movie] FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movie] ([movie_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_Movie]
GO
/****** Object:  ForeignKey [FK_HasGenre_Genre]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[HasGenre]  WITH CHECK ADD  CONSTRAINT [FK_HasGenre_Genre] FOREIGN KEY([genre_id])
REFERENCES [dbo].[Genre] ([genre_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HasGenre] CHECK CONSTRAINT [FK_HasGenre_Genre]
GO
/****** Object:  ForeignKey [FK_HasGenre_Movie]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[HasGenre]  WITH CHECK ADD  CONSTRAINT [FK_HasGenre_Movie] FOREIGN KEY([movie_id])
REFERENCES [dbo].[Movie] ([movie_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HasGenre] CHECK CONSTRAINT [FK_HasGenre_Movie]
GO
/****** Object:  ForeignKey [FK_Movie_Owner]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_Owner] FOREIGN KEY([owner_id])
REFERENCES [dbo].[User] ([user_id])
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_Owner]
GO
/****** Object:  ForeignKey [FK_Rental_MovieEdition]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_MovieEdition] FOREIGN KEY([edition_id])
REFERENCES [dbo].[Edition] ([edition_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_MovieEdition]
GO
/****** Object:  ForeignKey [FK_Rental_User]    Script Date: 05/07/2012 19:21:38 ******/
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Rental_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Rental_User]
GO
