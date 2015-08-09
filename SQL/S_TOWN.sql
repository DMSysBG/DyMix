USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[S_TOWN]    Script Date: 28.7.2015 г. 17:45:05 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[S_TOWN](
	[ID] [int] NOT NULL,
	[S_COUNTRY_ID] [int] NULL,
	[S_REGION_ID] [int] NULL,
	[T_NAME] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

