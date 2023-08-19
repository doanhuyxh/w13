use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentTransaction_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentTransaction_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure update a new record for PaymentTransaction table.
-- =============================================
create procedure [dbo].[ptgroupPaymentTransaction_Update]
(
	@Id bigint,
	@PaymentId int,
	@OrderId int,
	@CustomerId int,
	@TransactionDate datetime
)

as

set nocount on

update [PaymentTransaction]
set [PaymentId] = @PaymentId,
	[OrderId] = @OrderId,
	[CustomerId] = @CustomerId,
	[TransactionDate] = @TransactionDate
where [Id] = @Id
go
