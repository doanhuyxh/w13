use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProduct_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProduct_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record with paging for Products table.
-- =============================================
create procedure [dbo].[ptgroupProduct_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM Products

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[MakerId],
	[CateId],
	[ProductName],
	[ProductCode],
	[Description],
	[Images],
	[PictureId],
	[UnitPrice],
	[SellPrice],
	[Quantity],
	[Views],
	[Sort],
	[Published],
	[MetaKeyword],
	[MetaDescription],
	[CreatedDate],
	[LastmodifiedDate],
	[lat],
	[lng]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM Products WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
