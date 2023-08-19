USE [PhucThanh_Ecomerce]
GO

/****** Object:  Table [dbo].[ProductTag]    Script Date: 4/18/2017 11:38:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductTag](
	[ProductID] [bigint] NOT NULL,
	[Keyword] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ProductTag] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[Keyword] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_DeleteByKeyword]
	@Keyword nvarchar(200)
AS
BEGIN
	DELETE FROM dbo.ProductTag WHERE Keyword = @Keyword
END

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_DeleteByProduct]
	@ProductID bigint
AS
BEGIN
	DELETE FROM dbo.ProductTag WHERE ProductID = @ProductID
END

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_Insert]
	@ProductID bigint,
	@Keyword nvarchar(200),
	@StatusCode int
AS
BEGIN
	IF(
		EXISTS(
			SELECT TOP 1 * FROM dbo.ProductTag WHERE ProductID = @ProductID AND Keyword = @Keyword
		)
	)
	BEGIN
		SET @StatusCode = - 1
	END
	ELSE
	BEGIN
		INSERT INTO dbo.ProductTag
		(
			ProductID,
			Keyword
		)
		VALUES
		(
			@ProductID,
			@Keyword
		)
		SET @StatusCode = 1
	END
END

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_ListAllPaging]
	@ProductID bigint,
	@Keyword nvarchar(200),
	@pageIndex int,
	@pageSize int,
	@totalRow int output
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @UpperBand int, @LowerBand int
	SET @LowerBand  = (@pageIndex - 1) * @PageSize
	SET @UpperBand  = (@pageIndex * @PageSize)

	SELECT @totalRow = COUNT(*) FROM dbo.ProductTag pt INNER JOIN dbo.Products p ON pt.ProductID = p.Id
					WHERE (@ProductID = 0 OR (@ProductID > 0 AND pt.ProductID = @ProductID))
					AND (@Keyword = '' OR (@Keyword <> '' AND pt.Keyword = @Keyword))

	SELECT * FROM
	(SELECT ROW_NUMBER() OVER(ORDER BY ProductID ASC) As RowNumber, pt.ProductID, pt.Keyword, p.ProductName
	FROM dbo.ProductTag pt INNER JOIN dbo.Products p ON pt.ProductID = p.Id
	WHERE (@ProductID = 0 OR (@ProductID > 0 AND pt.ProductID = @ProductID))
	AND (@Keyword = '' OR (@Keyword <> '' AND pt.Keyword = @Keyword))) temp
	WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
END

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_ListByKeyword]
	@Keyword nvarchar(200)
AS
BEGIN
	SELECT pt.*, p.ProductName
	FROM dbo.ProductTag pt INNER JOIN dbo.Products p ON pt.ProductID = p.Id
	WHERE pt.Keyword = @Keyword
END

GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		LongNDG
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ProductTag_ListByProduct]
	@ProductID bigint
AS
BEGIN
	SELECT ProductID, Keyword, p.ProductName 
	FROM dbo.ProductTag pt INNER JOIN dbo.Products p ON pt.ProductID = p.Id
	WHERE ProductID = @ProductID
END

GO



