use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentInfo_Update]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentInfo_Update]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure update a new record for PaymentInfo table.
-- =============================================
create procedure [dbo].[ptgroupPaymentInfo_Update]
(
	@Id int,
	@CustomerId int,
	@PaymentType int,
	@PaymentDetail nvarchar(max),
	@CreatedDate datetime
)

as

set nocount on

update [PaymentInfo]
set [CustomerId] = @CustomerId,
	[PaymentType] = @PaymentType,
	[PaymentDetail] = @PaymentDetail,
	[CreatedDate] = @CreatedDate
where [Id] = @Id
go
