USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[C_CARD]    Script Date: 19.8.2015 г. 23:41:57 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[C_CARD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[C_NUMBER] [nvarchar](50) NOT NULL,
	[VALID_FROM_DATE] [datetime] NOT NULL,
	[VALID_TO_DATE] [datetime] NOT NULL,
	[IS_BLOCKED] [tinyint] NOT NULL CONSTRAINT [DF_C_CARD_IS_ACTIVE]  DEFAULT ((0)),
	[CONTRACTOR_ID] [int] NULL,
	[DISCOUNT_GROUP_ID] [int] NULL,
 CONSTRAINT [PK_C_CARD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
