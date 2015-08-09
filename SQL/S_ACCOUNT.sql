USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[S_ACCOUNT]    Script Date: 28.7.2015 г. 11:15:06 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[S_ACCOUNT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[A_EMAIL] [nvarchar](256) NOT NULL,
	[A_USERNAME] [nvarchar](50) NOT NULL,
	[A_PASSWORD] [nvarchar](32) NOT NULL,
	[IS_ACTIVE] [tinyint] NOT NULL CONSTRAINT [DF_S_ACCOUNT_IS_ACTIVE]  DEFAULT ((1)),
	[FIRST_NAME] [nvarchar](256) NULL,
	[LAST_NAME] [nvarchar](256) NULL,
	[LANGUAGE_CODE] [nvarchar](10) NULL,
	[LOGIN_SESSION_ID] [nvarchar](24) NULL,
	[LOGIN_LAST_DATE] [datetime] NULL,
 CONSTRAINT [PK_S_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
