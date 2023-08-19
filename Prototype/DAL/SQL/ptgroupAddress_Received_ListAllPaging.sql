use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupAddress_Received_ListAllPaging]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupAddress_Received_ListAllPaging]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure select all record with paging for Address_Received table.
-- =============================================
create procedure [dbo].[ptgroupAddress_Received_ListAllPaging]

(
@pageIndex int,
@pageSize int,
@totalRow int output
)

as

set nocount on

DECLARE @UpperBand int, @LowerBand int

SELECT @totalRow = COUNT(*) FROM Address_Received

SET @LowerBand  = (@pageIndex - 1) * @PageSize
SET @UpperBand  = (@pageIndex * @PageSize)

SELECT  [Id],
	[CustomerId],
	[FullName],
	[Phone],
	[Email],
	[CityId],
	[Address],
	[Note],
	[CreatedDate]
 FROM(SELECT *, ROW_NUMBER() OVER(ORDER BY Id ASC) AS RowNumber FROM Address_Received WITH (NOLOCK)) AS temp
WHERE RowNumber > @LowerBand AND RowNumber <= @UpperBand
go
