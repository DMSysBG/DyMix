USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[S_COUNTRY]    Script Date: 28.7.2015 г. 17:44:19 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[S_COUNTRY](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[C_NAME] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_S_COUNTRY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

