use [MarketPlacePT]
go
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ptgroupOrder_ListAll]') and ObjectProperty(id, N'IsProcedure') = 1)
	drop procedure [dbo].[ptgroupOrder_ListAll]
go

create procedure [dbo].[ptgroupOrder_ListAll]

as

set nocount on

select  [Id],
	[CustomerId],
	[OrderDate],
	[Status]
from [Orders] WITH (NOLOCK)
go
