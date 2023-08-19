use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentInfo_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentInfo_ListAll]
go

create procedure [dbo].[ptgroupPaymentInfo_ListAll]

as

set nocount on

select  [Id],
	[CustomerId],
	[PaymentType],
	[PaymentDetail],
	[CreatedDate]
from [PaymentInfo] WITH (NOLOCK)
go
