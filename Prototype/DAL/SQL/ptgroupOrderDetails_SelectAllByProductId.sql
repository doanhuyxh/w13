use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderDetails_SelectAllByProductId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderDetails_SelectAllByProductId]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record in OrderDetails table.
-- =============================================
create procedure [dbo].[ptgroupOrderDetails_SelectAllByProductId]
(
	@ProductId bigint
)

as

set nocount on

select [Id],
	[OrderId],
	[ProductId],
	[UnitPrice],
	[Quantity]
from [OrderDetails]
where [ProductId] = @ProductId
go
