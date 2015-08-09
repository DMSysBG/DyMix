USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[CONTRACTOR_TYPE]    Script Date: 28.7.2015 г. 18:16:45 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CONTRACTOR_TYPE](
	[ID] [int] NOT NULL,
	[CT_NAME] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CONTRACTOR_TYPE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
