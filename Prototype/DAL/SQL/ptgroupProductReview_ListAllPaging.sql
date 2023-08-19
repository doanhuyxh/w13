use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductReview_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductReview_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record with paging for ProductReview table.
-- =============================================
create procedure [dbo].[ptgroupProductReview_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM ProductReview

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[CustomerId],
	[ProductId],
	[IsApproved],
	[Title],
	[ReviewText],
	[Rating],
	[CreatedOn]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM ProductReview WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
