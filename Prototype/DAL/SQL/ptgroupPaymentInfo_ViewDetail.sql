use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentInfo_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentInfo_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure view detail a new record for PaymentInfo table.
-- =============================================
create procedure [dbo].[ptgroupPaymentInfo_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[CustomerId],
	[PaymentType],
	[PaymentDetail],
	[CreatedDate]
from [PaymentInfo]
where [Id] = @Id
go
