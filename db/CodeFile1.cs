/*USE[EisenhoverMatrix]
GO

ALTER TABLE [dbo].[matrix] DROP CONSTRAINT[FK_matrix_matrix]
GO

*//****** Object:  Table [dbo].[matrix]    Script Date: 13.12.2021 22:30:23 ******//*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[matrix]') AND type in (N'U'))
DROP TABLE[dbo].[matrix]
GO

*//****** Object:  Table [dbo].[matrix]    Script Date: 13.12.2021 22:30:23 ******//*
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[matrix](

[id][int] IDENTITY(1, 1) NOT NULL,

[title] [varchar](50) NOT NULL,
CONSTRAINT[PK_matrix] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
) ON[PRIMARY]
GO

ALTER TABLE [dbo].[matrix]  WITH CHECK ADD  CONSTRAINT [FK_matrix_matrix] FOREIGN KEY([id])
REFERENCES[dbo].[matrix]([id])
GO

ALTER TABLE [dbo].[matrix] CHECK CONSTRAINT[FK_matrix_matrix]
GO


ALTER TABLE [dbo].[item] DROP CONSTRAINT[FK_item_matrix]
GO

*//****** Object:  Table [dbo].[item]    Script Date: 13.12.2021 22:30:58 ******//*
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[item]') AND type in (N'U'))
DROP TABLE[dbo].[item]
GO

*//****** Object:  Table [dbo].[item]    Script Date: 13.12.2021 22:30:58 ******//*
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[item](

[id][int] IDENTITY(1, 1) NOT NULL,

[title] [varchar](50) NOT NULL,

[deadline] [date] NOT NULL,

[is_important] [bit] NOT NULL,

[matrix_id] [int] NOT NULL,
CONSTRAINT[PK_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
) ON[PRIMARY]
GO

ALTER TABLE [dbo].[item]  WITH CHECK ADD  CONSTRAINT [FK_item_matrix] FOREIGN KEY([matrix_id])
REFERENCES[dbo].[matrix]([id])
GO

ALTER TABLE [dbo].[item] CHECK CONSTRAINT[FK_item_matrix]
GO*/
