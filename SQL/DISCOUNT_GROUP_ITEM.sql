USE [db48acc9c4eec24cc6bb96a4d000858d2d]
GO

/****** Object:  Table [dbo].[DISCOUNT_GROUP_ITEM]    Script Date: 6.8.2015 г. 22:26:28 ч. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DISCOUNT_GROUP_ITEM](
	[DISCOUNT_GROUP_ID] [int] NOT NULL,
	[DISCOUNT_ID] [int] NOT NULL,
 CONSTRAINT [PK_DISCOUNT_GROUP_ITEM_1] PRIMARY KEY CLUSTERED 
(
	[DISCOUNT_GROUP_ID] ASC,
	[DISCOUNT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

