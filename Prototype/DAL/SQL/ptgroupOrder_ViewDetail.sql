use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_ViewDetail]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_ViewDetail]
go

-- =============================================
-- Author: Auto Generator
-- Create date: 12/14/2016
-- Description:	The procedure view detail a new record for Orders table.
-- =============================================
create procedure [dbo].[ptgroupOrder_ViewDetail]
(
	@Id int
)

as

set nocount on

select  [Id],
	[CustomerId],
	[OrderDate],
	[Status]
from [Orders]
where [Id] = @Id
go
