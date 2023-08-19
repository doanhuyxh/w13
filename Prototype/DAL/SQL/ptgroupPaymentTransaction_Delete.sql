use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupPaymentTransaction_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupPaymentTransaction_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/22/2016
-- Description:	The procedure delete a new record for PaymentTransaction table.
-- =============================================
create procedure [dbo].[ptgroupPaymentTransaction_Delete]
(
	@Id bigint
)

as

set nocount on

delete from [PaymentTransaction]
where [Id] = @Id
go
