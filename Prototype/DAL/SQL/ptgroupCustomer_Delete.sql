use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupCustomer_Delete]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupCustomer_Delete]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure delete a new record for Customers table.
-- =============================================
create procedure [dbo].[ptgroupCustomer_Delete]
(
	@Id int
)

as

set nocount on

delete from [Customers]
where [Id] = @Id
go
