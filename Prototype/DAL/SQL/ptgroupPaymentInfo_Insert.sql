use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentInfo_Insert]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentInfo_Insert]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure insert a new record for PaymentInfo table.
-- =============================================
create procedure [dbo].[ptgroupPaymentInfo_Insert]
(
	@CustomerId int,
	@PaymentType int,
	@PaymentDetail nvarchar(max),
	@CreatedDate datetime,
	@Id int OUTPUT
)

as

set nocount on

insert into [PaymentInfo]
(
	[CustomerId],
	[PaymentType],
	[PaymentDetail],
	[CreatedDate]
)
values
(
	@CustomerId,
	@PaymentType,
	@PaymentDetail,
	@CreatedDate
)

set @Id = scope_identity()
go
