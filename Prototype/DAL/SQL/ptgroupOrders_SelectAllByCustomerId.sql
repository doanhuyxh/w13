use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrders_SelectAllByCustomerId]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrders_SelectAllByCustomerId]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure select all record in Orders table.
-- =============================================
create procedure [dbo].[ptgroupOrders_SelectAllByCustomerId]
(
	@CustomerId int
)

as

set nocount on

select [Id],
	[CustomerId],
	[OrderDate],
	[Status]
from [Orders]
where [CustomerId] = @CustomerId
go
