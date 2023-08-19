use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupProductAttribute_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupProductAttribute_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record with paging for ProductAttribute table.
-- =============================================
create procedure [dbo].[ptgroupProductAttribute_ListAllPaging]

(
@CateID int,
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM ProductAttribute WHERE (@CateID = 0 OR (@CateID <> 0 AND Id IN (SELECT AttributeID FROM CategoryAttribute WHERE CategoryID = @CateID)))

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[Attribute],
	[CreatedDate],
	[ModifiedDate]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM ProductAttribute WITH (NOLOCK)
 WHERE (@CateID = 0 OR (@CateID <> 0 AND Id IN (SELECT AttributeID FROM CategoryAttribute WHERE CategoryID = @CateID)))
 ) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
