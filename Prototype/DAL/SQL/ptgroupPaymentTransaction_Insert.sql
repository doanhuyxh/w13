use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentTransaction_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentTransaction_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure insert a new record for PaymentTransaction table.
-- =============================================
create procedure [dbo].[ptgroupPaymentTransaction_Insert]
(
	@PaymentId int,
	@OrderId int,
	@CustomerId int,
	@TransactionDate datetime,
	@Id bigint OUTPUT
)

as

set nocount on

insert into [PaymentTransaction]
(
	[PaymentId],
	[OrderId],
	[CustomerId],
	[TransactionDate]
)
values
(
	@PaymentId,
	@OrderId,
	@CustomerId,
	@TransactionDate
)

set @Id = scope_identity()
go
