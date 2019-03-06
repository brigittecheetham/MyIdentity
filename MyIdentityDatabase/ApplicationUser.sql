
CREATE TABLE [dbo].[ApplicationUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Title] [varchar](10) NULL,
	[Name] [nvarchar](100) NULL,
	[Surname] [nvarchar](100) NULL,
	[Cellphone] [varchar](10) NULL,
	[HomeAddress] [nvarchar](500) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
 CONSTRAINT [pkApplicationUser] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


