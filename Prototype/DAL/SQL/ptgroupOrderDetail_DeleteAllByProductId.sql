use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderDetail_DeleteAllByProductId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderDetail_DeleteAllByProductId]
go

create procedure [dbo].[ptgroupOrderDetail_DeleteAllByProductId]
(
	@ProductId bigint
)

as

set nocount on

delete from [OrderDetails]
where [ProductId] = @ProductId
go
