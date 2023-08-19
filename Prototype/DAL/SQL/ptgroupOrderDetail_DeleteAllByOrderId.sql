use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderDetail_DeleteAllByOrderId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderDetail_DeleteAllByOrderId]
go

create procedure [dbo].[ptgroupOrderDetail_DeleteAllByOrderId]
(
	@OrderId int
)

as

set nocount on

delete from [OrderDetails]
where [OrderId] = @OrderId
go
