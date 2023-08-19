use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentTransaction_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentTransaction_ListAll]
go

create procedure [dbo].[ptgroupPaymentTransaction_ListAll]

as

set nocount on

select  [Id],
	[PaymentId],
	[OrderId],
	[CustomerId],
	[TransactionDate]
from [PaymentTransaction] WITH (NOLOCK)
go
