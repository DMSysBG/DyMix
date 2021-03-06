USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[CONTRACTOR]    Script Date: 28.7.2015 г. 17:37:03 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CONTRACTOR](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[C_NAME] [nvarchar](256) NOT NULL,
	[ID_CONTRACTOR] [nvarchar](20) NOT NULL,
	[S_TOWN_ID] [int] NOT NULL,
	[C_ADDRESS] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_CONTRACTOR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

