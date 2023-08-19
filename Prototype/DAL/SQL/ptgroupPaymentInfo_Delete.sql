use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentInfo_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentInfo_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure delete a new record for PaymentInfo table.
-- =============================================
create procedure [dbo].[ptgroupPaymentInfo_Delete]
(
	@Id int
)

as

set nocount on

delete from [PaymentInfo]
where [Id] = @Id
go
