use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttributeValue_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttributeValue_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record with paging for ProductAttributeValue table.
-- =============================================
create procedure [dbo].[ptgroupProductAttributeValue_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM ProductAttributeValue

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[ProductId],
	[AttributeId],
	[AttributeValueTypeId],
	[AssociatedProductId],
	[Name],
	[ColorSquaresRgb],
	[ImageSquaresPictureId],
	[PriceAdjustment],
	[WeightAdjustment],
	[Cost],
	[Quantity],
	[IsPreSelected],
	[DisplayOrder],
	[PictureId]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM ProductAttributeValue WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
