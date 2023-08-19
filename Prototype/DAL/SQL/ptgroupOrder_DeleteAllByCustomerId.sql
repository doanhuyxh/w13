use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_DeleteAllByCustomerId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_DeleteAllByCustomerId]
go

create procedure [dbo].[ptgroupOrder_DeleteAllByCustomerId]
(
	@CustomerId int
)

as

set nocount on

delete from [Orders]
where [CustomerId] = @CustomerId
go
