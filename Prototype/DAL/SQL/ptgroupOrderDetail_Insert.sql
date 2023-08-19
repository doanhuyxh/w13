use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrderDetail_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrderDetail_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure insert a new record for OrderDetails table.
-- =============================================
create procedure [dbo].[ptgroupOrderDetail_Insert]
(
	@OrderId int,
	@ProductId bigint,
	@UnitPrice money,
	@Quantity smallint,
	@Id int OUTPUT
)

as

set nocount on

insert into [OrderDetails]
(
	[OrderId],
	[ProductId],
	[UnitPrice],
	[Quantity]
)
values
(
	@OrderId,
	@ProductId,
	@UnitPrice,
	@Quantity
)

set @Id = scope_identity()
go
