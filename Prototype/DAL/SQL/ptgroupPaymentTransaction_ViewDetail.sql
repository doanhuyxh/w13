use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentTransaction_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentTransaction_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure view detail a new record for PaymentTransaction table.
-- =============================================
create procedure [dbo].[ptgroupPaymentTransaction_ViewDetail]
(
	@Id bigint
)

as

set nocount on

select  [Id],
	[PaymentId],
	[OrderId],
	[CustomerId],
	[TransactionDate]
from [PaymentTransaction]
where [Id] = @Id
go
